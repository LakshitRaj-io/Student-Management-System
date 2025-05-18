using System;
using System.Collections.Generic;

namespace LibraryManagementApp
{
    // Book class with non-nullable properties initialized
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }

    class Program
    {
        // List to store books
        static List<Book> books = new List<Book>();

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        ViewBooks();
                        break;
                    case "3":
                        SearchBook();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Enter Book Title: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            Console.Write("Enter Author Name: ");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Author cannot be empty.");
                return;
            }

            Book newBook = new Book(title, author);
            books.Add(newBook);
            Console.WriteLine($"Book '{title}' by {author} added successfully.");
        }

        static void ViewBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\nList of Books:");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title} by {book.Author}");
            }
        }

        static void SearchBook()
        {
            Console.Write("Enter book title to search: ");
            string? searchTitle = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchTitle))
            {
                Console.WriteLine("Search term cannot be empty.");
                return;
            }

            var foundBooks = books.FindAll(b => b.Title.IndexOf(searchTitle, StringComparison.OrdinalIgnoreCase) >= 0);

            if (foundBooks.Count == 0)
            {
                Console.WriteLine("No books found with that title.");
            }
            else
            {
                Console.WriteLine("\nSearch Results:");
                foreach (var book in foundBooks)
                {
                    Console.WriteLine($"- {book.Title} by {book.Author}");
                }
            }
        }
    }
}
