﻿using JetBrains.Annotations;
using LANCommander.Launcher.Data;
using LANCommander.Launcher.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LANCommander.Launcher.Services
{
    public class PlaySessionService : BaseDatabaseService<PlaySession>
    {

        public PlaySessionService(DatabaseContext dbContext, SDK.Client client, ILogger<CollectionService> logger) : base(dbContext, client, logger) { }

        public async Task<PlaySession> GetLatestSession(Guid gameId, Guid userId)
        {
            return await Get(ps => ps.GameId == gameId && ps.UserId == userId).OrderByDescending(ps => ps.End).FirstOrDefaultAsync();
        }

        public async Task StartSession(Guid gameId, Guid userId)
        {
            try
            {
                var existingSession = Get(ps => ps.GameId == gameId && ps.UserId == userId && ps.End == null).FirstOrDefault();

                if (existingSession != null)
                    await Delete(existingSession);

                var session = new PlaySession()
                {
                    GameId = gameId,
                    UserId = userId,
                    Start = DateTime.UtcNow
                };

                await Add(session);

                await Client.Games.StartPlaySessionAsync(gameId);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "An unknown error occurred while trying to start session recording for game with ID {GameId}", gameId);
            }
        }

        public async Task EndSession(Guid gameId, Guid userId)
        {
            try
            {
                var existingSession = Get(ps => ps.GameId == gameId && ps.UserId == userId && ps.End == null).FirstOrDefault();

                if (existingSession != null)
                {
                    existingSession.End = DateTime.UtcNow;

                    await Update(existingSession);
                }
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "An unknown error occurred while trying to end session recording for game with ID {GameId}", gameId);
            }
            finally
            {
                await Client.Games.EndPlaySessionAsync(gameId);
            }
        }
    }
}