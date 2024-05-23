﻿using LANCommander.Client.Data;
using LANCommander.Client.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LANCommander.Client.Services
{
    public class LibraryService : BaseService
    {
        private readonly SDK.Client Client;
        private readonly CollectionService CollectionService;
        private readonly CompanyService CompanyService;
        private readonly EngineService EngineService;
        private readonly GameService GameService;
        private readonly GenreService GenreService;
        private readonly MultiplayerModeService MultiplayerModeService;
        private readonly RedistributableService RedistributableService;
        private readonly TagService TagService;

        public LibraryService(
            SDK.Client client,
            CollectionService collectionService,
            CompanyService companyService,
            EngineService engineService,
            GameService gameService,
            GenreService genreService,
            MultiplayerModeService multiplayerModeService,
            RedistributableService redistributableService,
            TagService tagService) : base()
        {
            Client = client;
            CollectionService = collectionService;
            CompanyService = companyService;
            EngineService = engineService;
            GameService = gameService;
            GenreService = genreService;
            MultiplayerModeService = multiplayerModeService;
            RedistributableService = redistributableService;
            TagService = tagService;
        }

        public async Task ImportAsync()
        {
            await ImportGamesAsync();
            await ImportRedistributables();
        }

        public async Task ImportGamesAsync()
        {
            var localGames = await GameService.Get();
            var remoteGames = await Client.Games.GetAsync();

            #region Import Collections
            var collections = await ImportFromModel<Collection, SDK.Models.Collection, CollectionService>(remoteGames.SelectMany(g => g.Collections).DistinctBy(c => c.Id), CollectionService, (collection, importCollection) =>
            {
                collection.Name = importCollection.Name;

                return collection;
            });
            #endregion

            #region Import Companies
            var importCompanies = new List<SDK.Models.Company>();

            importCompanies.AddRange(remoteGames.SelectMany(g => g.Developers));
            importCompanies.AddRange(remoteGames.SelectMany(g => g.Publishers));

            var companies = await ImportFromModel<Company, SDK.Models.Company, CompanyService>(importCompanies.DistinctBy(c => c.Id), CompanyService, (company, importCompany) =>
            {
                company.Name = importCompany.Name;

                return company;
            });
            #endregion

            #region Import Engines
            /*var engines = await ImportFromModel<Engine, SDK.Models.Engine, EngineService>(remoteGames.SelectMany(g => g.Engines).DistinctBy(e => e.Id), EngineService, (engine, importEngine) =>
            {
                engine.Name = importEngine.Name;

                return engine;
            });*/
            #endregion

            #region Import Genres
            var genres = await ImportFromModel<Genre, SDK.Models.Genre, GenreService>(remoteGames.SelectMany(g => g.Genres).DistinctBy(g => g.Id), GenreService, (genre, importGenre) =>
            {
                genre.Name = importGenre.Name;

                return genre;
            });
            #endregion

            #region Import Multiplayer Modes
            /*var multiplayerModes = await ImportFromModel<MultiplayerMode, SDK.Models.MultiplayerMode, MultiplayerModeService>(remoteGames.SelectMany(g => g.MultiplayerModes).DistinctBy(m => m.Id), MultiplayerModeService, (multiplayerMode, importMultiplayerMode) =>
            {
                multiplayerMode.Description = importMultiplayerMode.Description;
                multiplayerMode.MinPlayers = importMultiplayerMode.MinPlayers;
                multiplayerMode.MaxPlayers = importMultiplayerMode.MaxPlayers;
                multiplayerMode.NetworkProtocol = importMultiplayerMode.NetworkProtocol;
                multiplayerMode.Type = importMultiplayerMode.Type;
                multiplayerMode.Spectators = importMultiplayerMode.Spectators;

                return multiplayerMode;
            });*/
            #endregion

            #region Import Tags
            var tags = await ImportFromModel<Tag, SDK.Models.Tag, TagService>(remoteGames.SelectMany(g => g.Tags).DistinctBy(t => t.Id), TagService, (tag, importTag) =>
            {
                tag.Name = importTag.Name;

                return tag;
            });
            #endregion

            foreach (var remoteGame in remoteGames.OrderBy(g => (int)g.Type))
            {
                try
                {
                    var localGame = localGames.FirstOrDefault(g => g.Id == remoteGame.Id);

                    if (localGame == null)
                        localGame = new Data.Models.Game();

                    localGame.Title = remoteGame.Title;
                    localGame.SortTitle = remoteGame.SortTitle;
                    localGame.Description = remoteGame.Description;
                    localGame.Notes = remoteGame.Notes;
                    localGame.ReleasedOn = remoteGame.ReleasedOn;
                    localGame.Type = (Data.Enums.GameType)(int)remoteGame.Type;
                    localGame.BaseGameId = remoteGame.BaseGame?.Id;
                    localGame.Singleplayer = remoteGame.Singleplayer;

                    #region Update Game Collections
                    if (localGame.Collections == null)
                        localGame.Collections = collections.Where(c => remoteGame.Collections.Any(rc => rc.Id == c.Id)).ToList();
                    else
                    {
                        var collectionsToRemove = localGame.Collections.Where(c => !remoteGame.Collections.Any(rc => rc.Id == c.Id)).ToList();
                        var collectionsToAdd = collections.Where(c => remoteGame.Collections.Any(rc => rc.Id == c.Id) && !localGame.Collections.Any(lc => lc.Id == c.Id)).ToList();

                        foreach (var collection in collectionsToRemove)
                            localGame.Collections.Remove(collection);

                        foreach (var collection in collectionsToAdd)
                            localGame.Collections.Add(collection);
                    }
                    #endregion

                    #region Update Game Developers
                    if (localGame.Developers == null)
                        localGame.Developers = companies.Where(c => remoteGame.Developers.Any(rc => rc.Id == c.Id)).ToList();
                    else
                    {
                        var developersToRemove = localGame.Developers.Where(c => !remoteGame.Developers.Any(rc => rc.Id == c.Id)).ToList();
                        var developersToAdd = companies.Where(c => remoteGame.Developers.Any(rc => rc.Id == c.Id) && !localGame.Developers.Any(lc => lc.Id == c.Id)).ToList();

                        foreach (var developer in developersToRemove)
                            localGame.Developers.Remove(developer);

                        foreach (var developer in developersToAdd)
                            localGame.Developers.Add(developer);
                    }
                    #endregion

                    #region Update Game Publishers
                    if (localGame.Publishers == null)
                        localGame.Publishers = companies.Where(c => remoteGame.Publishers.Any(rc => rc.Id == c.Id)).ToList();
                    else
                    {
                        var publishersToRemove = localGame.Publishers.Where(c => !remoteGame.Publishers.Any(rc => rc.Id == c.Id)).ToList();
                        var publishersToAdd = companies.Where(c => remoteGame.Publishers.Any(rc => rc.Id == c.Id) && !localGame.Publishers.Any(lc => lc.Id == c.Id)).ToList();

                        foreach (var publisher in publishersToRemove)
                            localGame.Publishers.Remove(publisher);

                        foreach (var publisher in publishersToAdd)
                            localGame.Publishers.Add(publisher);
                    }
                    #endregion

                    #region Update Game Genres
                    if (localGame.Genres == null)
                        localGame.Genres = genres.Where(c => remoteGame.Genres.Any(rc => rc.Id == c.Id)).ToList();
                    else
                    {
                        var genresToRemove = localGame.Genres.Where(c => !remoteGame.Genres.Any(rc => rc.Id == c.Id)).ToList();
                        var genresToAdd = genres.Where(c => remoteGame.Genres.Any(rc => rc.Id == c.Id) && !localGame.Genres.Any(lc => lc.Id == c.Id)).ToList();

                        foreach (var genre in genresToRemove)
                            localGame.Genres.Remove(genre);

                        foreach (var genre in genresToAdd)
                            localGame.Genres.Add(genre);
                    }
                    #endregion

                    #region Update Game Tags
                    if (localGame.Tags == null)
                        localGame.Tags = tags.Where(c => remoteGame.Tags.Any(rc => rc.Id == c.Id)).ToList();
                    else
                    {
                        var tagsToRemove = localGame.Tags.Where(c => !remoteGame.Tags.Any(rc => rc.Id == c.Id)).ToList();
                        var tagsToAdd = tags.Where(c => remoteGame.Tags.Any(rc => rc.Id == c.Id) && !localGame.Tags.Any(lc => lc.Id == c.Id)).ToList();

                        foreach (var tag in tagsToRemove)
                            localGame.Tags.Remove(tag);

                        foreach (var tag in tagsToAdd)
                            localGame.Tags.Add(tag);
                    }
                    #endregion

                    // localGame.MultiplayerModes = multiplayerModes.Where(m => remoteGame.MultiplayerModes.Any(rm => rm.Id == m.Id)).ToList();

                    if (localGame.Id == Guid.Empty)
                    {
                        localGame.Id = remoteGame.Id;
                        localGame = await GameService.Add(localGame);
                    }
                    else
                        localGame = await GameService.Update(localGame);
                }
                catch (Exception ex)
                {

                }
            }

            // Potentially delete any games that no longer exist on the server or have been revoked
            foreach (var localGame in localGames)
            {
                var remoteGame = remoteGames.FirstOrDefault(g => g.Id == localGame.Id);

                if (remoteGame == null && !localGame.Installed)
                {
                    await GameService.Delete(localGame);
                }
            }
        }

        public async Task ImportRedistributables()
        {

        }

        // Could use something like automapper, but that's slow.
        public async Task<IEnumerable<T>> ImportFromModel<T, U, V>(IEnumerable<U> importModels, V service, Func<T, U, T> additionalMapping)
            where T : BaseModel
            where U : SDK.Models.KeyedModel
            where V : BaseDatabaseService<T>
        {
            // This could be handled better... DI?
            var models = await service.Get();

            foreach (var importModel in importModels)
            {
                try
                {
                    var model = models.FirstOrDefault(m => m.Id == importModel.Id);

                    if (model == null)
                        model = (T)Activator.CreateInstance(typeof(T));

                    model = additionalMapping.Invoke(model, importModel);

                    if (model.Id == Guid.Empty)
                    {
                        model.Id = importModel.Id;

                        model = await service.Add(model);
                    }
                    else
                        model = await service.Update(model);
                }
                catch (Exception ex)
                {

                }
            }

            foreach (var model in models.Where(m => !importModels.Any(im => im.Id == m.Id)))
            {
                await service.Delete(model);
            }

            // Too slow?
            return await service.Get();
        }

        public async Task<IEnumerable<Collection>> ImportCollections(IEnumerable<SDK.Models.Collection> importCollections)
        {
            var collections = await CollectionService.Get();

            foreach (var importCollection in importCollections)
            {
                try
                {
                    var collection = collections.FirstOrDefault(g => g.Id == importCollection.Id);

                    if (collection == null)
                        collection = new Collection();

                    collection.Name = importCollection.Name;

                    if (collection.Id == Guid.Empty)
                    {
                        collection.Id = importCollection.Id;

                        collection = await CollectionService.Add(collection);
                    }
                    else
                        collection = await CollectionService.Update(collection);
                }
                catch (Exception ex)
                {
                    // Exception handling
                }
            }

            foreach (var collection in collections.Where(g => !importCollections.Any(ig => ig.Id == g.Id)))
            {
                await CollectionService.Delete(collection);
            }

            // Too slow?
            return await CollectionService.Get();
        }

        public async Task<IEnumerable<Genre>> ImportGenres(IEnumerable<SDK.Models.Genre> importGenres)
        {
            var collections = await GenreService.Get();

            foreach (var importGenre in importGenres)
            {
                try
                {
                    var collection = collections.FirstOrDefault(g => g.Id == importGenre.Id);

                    if (collection == null)
                        collection = new Genre();

                    collection.Name = importGenre.Name;

                    if (collection.Id == Guid.Empty)
                    {
                        collection.Id = importGenre.Id;

                        collection = await GenreService.Add(collection);
                    }
                    else
                        collection = await GenreService.Update(collection);
                }
                catch (Exception ex)
                {
                    // Exception handling
                }
            }

            foreach (var collection in collections.Where(g => !importGenres.Any(ig => ig.Id == g.Id)))
            {
                await GenreService.Delete(collection);
            }

            // Too slow?
            return await GenreService.Get();
        }
    }
}
