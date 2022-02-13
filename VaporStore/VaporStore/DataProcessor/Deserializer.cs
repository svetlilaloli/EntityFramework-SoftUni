namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public static class Deserializer
	{
		private const string ErrorMessage = "Invalid Data";
		private const string SuccessfullyImportedGame = "Added {0} ({1}) with {2} tags";
		private const string SuccessfullyImportedUser = "Imported {0} with {1} cards";
		private const string SuccessfullyImportedPurchase = "Imported {0} for {1}";
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var gamesDto = JsonConvert.DeserializeObject<IEnumerable<GameInputDto>>(jsonString);
			var sb = new StringBuilder();
			
			List<Developer> developers = new List<Developer>();
			List<Genre> genres = new List<Genre>();
			List<Tag> tags = new List<Tag>();
			List<Game> mappedGames = new List<Game>();

            foreach (var gameDto in gamesDto)
            {
                if (!IsValid(gameDto))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				bool isReleaseDateValid = DateTime.TryParseExact(gameDto.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

                if (!isReleaseDateValid)
                {
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Game currentGame = new Game
				{
					Name = gameDto.Name,
					Price = gameDto.Price,
					ReleaseDate = releaseDate
				};

				var developer = developers.FirstOrDefault(d => d.Name == gameDto.Developer);
				if (developer == null)
                {
					developer = new Developer { Name = gameDto.Developer};
					developers.Add(developer);
                }
                
				currentGame.Developer = developer;
                
				var genre = genres.FirstOrDefault(g => g.Name == gameDto.Genre);
				if (genre == null)
                {
					genre = new Genre { Name = gameDto.Genre };
					genres.Add(genre);
                }

				currentGame.Genre = genre;

                foreach (var inputTag in gameDto.Tags)
                {
					var tag = tags.FirstOrDefault(t => t.Name == inputTag);
					if (tag == null)
                    {
						tag = new Tag { Name = inputTag };
						tags.Add(tag);
                    }
					currentGame.GameTags.Add(new GameTag { Tag = tag });
                }

				mappedGames.Add(Mapper.Instance.Map<Game>(currentGame));
				sb.AppendLine(string.Format(SuccessfullyImportedGame, currentGame.Name, currentGame.Genre.Name, currentGame.GameTags.Count));
            }

			context.Games.AddRange(mappedGames);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var usersDto = JsonConvert.DeserializeObject<IEnumerable<UserCardsInputDto>>(jsonString);
			var sb = new StringBuilder();
			var mappedUsers = new List<User>();

			foreach (var userDto in usersDto)
            {
				var isCardValid = true;

                if (!IsValid(userDto))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }
                foreach (var cardDto in userDto.Cards)
                {
					var isCardTypeValid = Enum.TryParse(typeof(CardType), cardDto.Type, true, out _);
					if (!isCardTypeValid)
                    {
						sb.AppendLine(ErrorMessage);
						isCardValid = false;
						break;
                    }
				}
				if (isCardValid)
                {
					sb.AppendLine(string.Format(SuccessfullyImportedUser, userDto.Username, userDto.Cards.Length));
					mappedUsers.Add(Mapper.Instance.Map<User>(userDto));
                }
            }

			context.Users.AddRange(mappedUsers);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var root = new XmlRootAttribute("Purchases");
			var serializer = new XmlSerializer(typeof(PurchaseInputDto[]), root);
			var purchasesDto = (PurchaseInputDto[])serializer.Deserialize(new StringReader(xmlString));
			var sb = new StringBuilder();
			var mappedPurchases = new List<Purchase>();

            foreach (var purchaseDto in purchasesDto)
            {
                if (!IsValid(purchaseDto))
                {
					sb.AppendLine(ErrorMessage);
					continue;
                }

				var game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Title);
				if (game == null)
                {
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var isValidPurchaseType = Enum.TryParse(purchaseDto.Type, true, out PurchaseType type);
				if (!isValidPurchaseType)
                {
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);
				if (card == null)
                {
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var isDateValid = DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
				if (!isDateValid)
                {
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Purchase currentPurchase = new Purchase
				{
					Type = type,
					ProductKey = purchaseDto.Key,
					Date = date,
					Card = card,
					Game = game
				};

				mappedPurchases.Add(Mapper.Instance.Map<Purchase>(currentPurchase));
				sb.AppendLine(string.Format(SuccessfullyImportedPurchase, game.Name, card.User.Username));
			}

			context.Purchases.AddRange(mappedPurchases);
			context.SaveChanges();

			return sb.ToString().TrimEnd();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}