namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = new List<GenreOutputDto>();

            foreach (var genreName in genreNames)
            {
                var genreDto = context.Genres.Include(g => g.Games)
                                             .ThenInclude(p => p.Purchases)
                                             .Include(g => g.Games)
                                             .ThenInclude(d => d.Developer)
                                             .Include(g => g.Games)
                                             .ThenInclude(gt => gt.GameTags)
                                             .ThenInclude(t => t.Tag)
                                             .ToList()
                                             .Where(g => g.Name == genreName && g.Games.Any(p => p.Purchases.Any()))
                                             .Select(g => new GenreOutputDto
                                             {
                                                 Id = g.Id,
                                                 Genre = g.Name,
                                                 Games = g.Games.Where(game => game.Purchases.Any())
                                                         .Select(game => new GameOutputDto
                                                         {
                                                             Id = game.Id,
                                                             Title = game.Name,
                                                             Developer = game.Developer.Name,
                                                             Tags = string.Join(", ", game.GameTags.Select(t => t.Tag.Name)),
                                                             Players = game.Purchases.Count
                                                         })
                                                         .OrderByDescending(game => game.Players)
                                                         .ThenBy(game => game.Id)
                                                         .ToArray(),
                                                 TotalPlayers = g.Games.Sum(game => game.Purchases.Count)
                                             })
                                             .ToList();

                genres.AddRange(genreDto);
            }

            genres = genres.OrderByDescending(g => g.TotalPlayers).ThenBy(g => g.Id).ToList();

            return JsonConvert.SerializeObject(genres, Formatting.Indented);
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var users = context.Users.Include(u => u.Cards)
                                     .ThenInclude(c => c.Purchases)
                                     .ThenInclude(p => p.Game)
                                     .ThenInclude(g => g.Genre)
                                     .ToList()
                                     .Where(u => u.Cards.Any(c => c.Purchases.Any(p => p.Type.ToString() == storeType)))
                                     .Select(u => new UserOutputDto
                                     {
                                         Username = u.Username,
                                         Purchases = u.Cards.SelectMany(c => c.Purchases)
                                                            .Where(p => p.Type.ToString() == storeType)
                                                            .Select(p => new PurchaseOutputDto
                                                            {
                                                                Card = p.Card.Number,
                                                                Cvc = p.Card.Cvc,
                                                                Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                                                Game = new PurchaseGameOutputDto
                                                                {
                                                                    Title = p.Game.Name,
                                                                    Genre = p.Game.Genre.Name,
                                                                    Price = p.Game.Price
                                                                }
                                                            })
                                                            .OrderBy(p => p.Date)
                                                            .ToArray(),
                                         TotalSpent = u.Cards.SelectMany(c => c.Purchases)
                                                             .Where(p => p.Type.ToString() == storeType)
                                                             .Sum(p => p.Game.Price)
                                     })
                                     .OrderByDescending(u => u.TotalSpent)
                                     .ThenBy(u => u.Username)
                                     .ToList();

            var root = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(List<UserOutputDto>), root);
            var namespaces = new XmlSerializerNamespaces();
            
            namespaces.Add(string.Empty, string.Empty);
            
            var sb = new StringBuilder();

            using(StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, users, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}