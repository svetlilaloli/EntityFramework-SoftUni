using BookShop.Data;
using BookShop.Models.Enums;
using BookShop.Initializer;
using System;
using System.Linq;
using System.Text;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using BookShopContext context = new BookShopContext();
            DbInitializer.ResetDatabase(context);

            //string command = Console.ReadLine();
            //Console.WriteLine(GetBooksByAgeRestriction(context, command));
            //Console.WriteLine(GetGoldenBooks(context));
            //Console.WriteLine(GetBooksByPrice(context));
            //Console.WriteLine(GetBooksNotReleasedIn(context, 2000));
            //Console.WriteLine(GetBooksByCategory(context, "horror mystery drama"));
            //Console.WriteLine(GetBooksReleasedBefore(context, "30-12-1989"));
            //Console.WriteLine(GetAuthorNamesEndingIn(context, "e"));
            //Console.WriteLine(GetBookTitlesContaining(context, "WOR"));
            //Console.WriteLine(GetBooksByAuthor(context, "po"));
            //Console.WriteLine(CountBooks(context, 40));
            //Console.WriteLine(CountCopiesByAuthor(context));
            //Console.WriteLine(GetTotalProfitByCategory(context));
            //Console.WriteLine(GetMostRecentBooks(context));
            //IncreasePrices(context);
            Console.WriteLine(RemoveBooks(context));
        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var restriction = Enum.Parse<AgeRestriction>(command, true);
            var books = context.Books.Where(b => b.AgeRestriction.Equals(restriction)).OrderBy(b => b.Title).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            int copiesCount = 5000;
            var books = context.Books.Where(b => b.Copies < copiesCount && b.EditionType == EditionType.Gold).OrderBy(b => b.BookId).ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();
        }
        public static string GetBooksByPrice(BookShopContext context)
        {
            int price = 40;
            var books = context.Books.Where(b => b.Price > price).OrderByDescending(b => b.Price).ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books.Where(b => b.ReleaseDate.Value.Year != year).OrderBy(b => b.BookId).ToList();
            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().Trim();
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var booksTitles = context.Books.Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                                     .OrderBy(b => b.Title)
                                     .Select(b => b.Title)
                                     .ToList();
            var sb = new StringBuilder();

            foreach (var title in booksTitles)
            {
                sb.AppendLine(title);
            }

            return sb.ToString().Trim();
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dateParsed = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            var books = context.Books.Where(b => b.ReleaseDate < dateParsed).OrderByDescending(b => b.ReleaseDate);
            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }

            return sb.ToString().Trim();
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors.Where(a => a.FirstName.EndsWith(input))
                                         .Select(a => a.FirstName + ' ' + a.LastName)
                                         .ToList();
            var sb = new StringBuilder();

            foreach (var author in authors.OrderBy(a => a))
            {
                sb.AppendLine(author);
            }

            return sb.ToString().Trim();
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var titles = context.Books.Where(b => b.Title.ToLower().Contains(input.ToLower()))
                                     .Select(a => a.Title)
                                     .ToList();
            var sb = new StringBuilder();

            foreach (var title in titles.OrderBy(t => t))
            {
                sb.AppendLine(title);
            }

            return sb.ToString().Trim();
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books.Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                                         .Select(b => new
                                         {
                                             b.BookId,
                                             b.Title,
                                             AuthorName = b.Author.FirstName + ' ' + b.Author.LastName,
                                         })
                                         .OrderBy(b => b.BookId)
                                         .ToList();
            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorName})");
            }

            return sb.ToString().Trim();
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int count = context.Books.Count(b => b.Title.Length > lengthCheck);
            return count;
        }
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorCopies = context.Authors.Select(a => new
            {
                AuthorName = a.FirstName + ' ' + a.LastName,
                CopiesCount = a.Books.Sum(b => b.Copies)
            }).OrderByDescending(c => c.CopiesCount).ToList();

            var sb = new StringBuilder();

            foreach (var author in authorCopies)
            {
                sb.AppendLine($"{author.AuthorName} - {author.CopiesCount}");
            }

            return sb.ToString().Trim();
        }
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitByCategory = context.Categories.Select(c => new
            {
                CategoryName = c.Name,
                Profit = c.CategoryBooks.Sum(c => c.Book.Price * c.Book.Copies)
            }).OrderByDescending(c => c.Profit).ThenBy(c => c.CategoryName).ToList();

            var sb = new StringBuilder();

            foreach (var category in profitByCategory)
            {
                sb.AppendLine($"{category.CategoryName} ${category.Profit:f2}");
            }

            return sb.ToString().Trim();
        }
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories.Select(c => new
            {
                CategoryName = c.Name,
                MostResentBooks = c.CategoryBooks.OrderByDescending(b => b.Book.ReleaseDate).Take(3).Select(b => new
                {
                    Title = b.Book.Title,
                    Year = b.Book.ReleaseDate.Value.Year
                }).ToList()
            }).OrderBy(c => c.CategoryName).ToList();

            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.CategoryName}");

                foreach (var book in category.MostResentBooks)
                {
                    sb.AppendLine($"{book.Title} ({book.Year})");
                }
            }

            return sb.ToString().Trim();
        }
        public static void IncreasePrices(BookShopContext context)
        {
            int year = 2010;
            var books = context.Books.Where(b => b.ReleaseDate.Value.Year < year).ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }
            context.SaveChanges();
        }
        public static int RemoveBooks(BookShopContext context)
        {
            int copies = 4200;
            var books = context.Books.Where(b => b.Copies < copies).ToList();

            foreach (var book in books)
            {
                context.Remove(book);
            }

            context.SaveChanges();

            return books.Count;
        }
    }
}
