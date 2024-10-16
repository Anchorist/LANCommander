﻿using LANCommander.Server.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LANCommander.Server.Data
{
    public class Repository<T> : IDisposable where T : class, IBaseModel
    {
        private readonly DbContext Context;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public Repository(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            HttpContextAccessor = httpContextAccessor;
        }

        private DbSet<T> DbSet
        {
            get { return Context.Set<T>(); }
        }

        private DbSet<User> UserDbSet
        {
            get { return Context.Set<User>(); }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsQueryable().Where(predicate);
        }

        public async Task<T> Find(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Get(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> Add(T entity)
        {
            entity.CreatedBy = GetCurrentUser();
            entity.UpdatedBy = GetCurrentUser();
            entity.CreatedOn = DateTime.Now;
            entity.UpdatedOn = DateTime.Now;

            await Context.AddAsync(entity);

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            var existing = await Find(entity.Id);

            Context.Entry(existing).CurrentValues.SetValues(entity);

            entity.UpdatedBy = GetCurrentUser();
            entity.UpdatedOn = DateTime.Now;

            Context.Update(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        private User GetUser(string username)
        {
            return UserDbSet.FirstOrDefault(u => u.UserName == username);
        }

        private User GetCurrentUser()
        {
            if (HttpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var user = GetUser(HttpContextAccessor.HttpContext.User.Identity.Name);

                if (user == null)
                    return null;
                else
                    return user;
            }
            else
                return null;
        }
    }
}
