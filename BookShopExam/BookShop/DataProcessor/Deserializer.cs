namespace BookShop.DataProcessor
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using System;
    using BookShop.Data.Models.Enums;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var root = new XmlRootAttribute("Books");
            var serializer = new XmlSerializer(typeof(List<BookInputDto>), root);
            var booksDto = (List<BookInputDto>)serializer.Deserialize(new StringReader(xmlString));
            var sb = new StringBuilder();
            var mappedBooks = new List<Book>();

            foreach (var bookDto in booksDto)
            {
                if (!IsValid(bookDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isValidGenre = Enum.TryParse(bookDto.Genre, out Genre genre) && Enum.IsDefined(typeof(Genre), genre);
                if (!isValidGenre)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isValidDate = DateTime.TryParseExact(bookDto.PublishedOn, Constants.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime publishedOn);
                if (!isValidDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Book book = new Book
                {
                    Name = bookDto.Name,
                    Genre = genre,
                    Price = bookDto.Price,
                    Pages = bookDto.Pages,
                    PublishedOn = publishedOn
                };

                mappedBooks.Add(book);
                sb.AppendLine(string.Format(SuccessfullyImportedBook, bookDto.Name, bookDto.Price));
            }

            context.Books.AddRange(mappedBooks);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var authorsDto = JsonConvert.DeserializeObject<List<AuthorInputDto>>(jsonString);
            var sb = new StringBuilder();
            var mappedAuthors = new List<Author>();
            var emails = new List<string>();
            var booksId = context.Books.Select(b => b.Id).ToList();

            foreach (var authorDto in authorsDto)
            {
                if (!IsValid(authorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (emails.Contains(authorDto.Email))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var validBooksId = new HashSet<int>();
                foreach (var bookDto in authorDto.Books)
                {
                    if (bookDto.Id == null)
                    {
                        continue;
                    }

                    int id = int.Parse(bookDto.Id);
                    if (!booksId.Contains(id))
                    {
                        continue;
                    }
                    validBooksId.Add(id);
                }

                if (validBooksId.Count < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Author author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName,
                    Email = authorDto.Email,
                    Phone = authorDto.Phone,
                    AuthorsBooks = validBooksId.Select(id => new AuthorBook
                    {
                        BookId = id
                    })
                    .ToList()
                };
                
                emails.Add(authorDto.Email);
                mappedAuthors.Add(author);
                sb.AppendLine(string.Format(SuccessfullyImportedAuthor, string.Concat(author.FirstName, " ", author.LastName), author.AuthorsBooks.Count));
            }

            context.Authors.AddRange(mappedAuthors);
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