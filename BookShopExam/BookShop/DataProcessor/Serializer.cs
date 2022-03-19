namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context.Authors.ToList().Select(a => new
                                        {
                                            AuthorName = a.FirstName + ' ' + a.LastName,
                                            Books = a.AuthorsBooks.ToList().Select(b => new
                                                                {
                                                                    BookName = b.Book.Name,
                                                                    BookPrice = (decimal)b.Book.Price
                                                                })
                                                                .OrderByDescending(b => b.BookPrice)
                                        })
                                        .OrderByDescending(a => a.Books.Count())
                                        .ThenBy(a => a.AuthorName);
            
            var mappedAuthors = authors.Select(a => new { a.AuthorName, Books = a.Books.Select(b => new { b.BookName, BookPrice = b.BookPrice.ToString("0.00") })});
            
            return JsonConvert.SerializeObject(mappedAuthors, Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var oldestBooks = context.Books.ToList()
                                    .Where(b => b.PublishedOn < date && b.Genre.ToString() == "Science")
                                    .Select(b => new BookOutputDto
                                    {
                                        Name = b.Name,
                                        Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture),
                                        Pages = (int)b.Pages
                                    })
                                    .OrderByDescending(b => b.Pages)
                                    .ThenByDescending(b => b.Date)
                                    .Take(10)
                                    .ToList();

            var root = new XmlRootAttribute("Books");
            var serializer = new XmlSerializer(typeof(List<BookOutputDto>), root);
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            var sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, oldestBooks, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}