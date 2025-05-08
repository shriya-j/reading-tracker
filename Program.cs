using System;
using System.Collections.Generic;

class Book
{
    public required string Title { get; set; }
    public int TotalPages { get; set; }
    public int PagesRead { get; set; }
    public bool IsCompleted => PagesRead >= TotalPages;
    public string Author { get; internal set; }
    public string DateStarted { get; internal set; }
    public string? DateCompleted { get; internal set; }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Started: {DateStarted}");
        if (DateCompleted != null)
            Console.WriteLine($"Completed: {DateCompleted}");
        Console.WriteLine($"Progress: {PagesRead}/{TotalPages} pages");
        Console.WriteLine($"Status: {(IsCompleted ? "Completed" : "In Progress")}");
        Console.WriteLine();
    }
}

class Program
{
    static List<Book> books = new List<Book>();

    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nReading Tracker Menu");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. View reading list");
            Console.WriteLine("3. Update progress");
            Console.WriteLine("4. Exit");
            Console.Write("\nSelect an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    ViewBooks();
                    break;
                case "3":
                    UpdateProgress();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void AddBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();

        Console.Write("Enter author: ");
        string author = Console.ReadLine();

        Console.Write("Enter date started (mm/dd/yyyy): ");
        string dateStarted = Console.ReadLine();

        Console.Write("Enter total number of pages: ");
        int totalPages = int.Parse(Console.ReadLine());

        Book book = new Book
        {
            Title = title,
            Author = author,
            DateStarted = dateStarted,
            DateCompleted = null,
            TotalPages = totalPages,
            PagesRead = 0
        };

        books.Add(book);
        Console.WriteLine("\nBook added to library!");
    }

    static void ViewBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books added yet.");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"\nBook #{i + 1}");
            books[i].Display();
        }
    }

    static void UpdateProgress()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books to update.");
            return;
        }

        ViewBooks();
        Console.Write("Enter book number to update: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= books.Count)
        {
            Console.WriteLine("Invalid book number.");
            return;
        }

        Console.Write("Enter new pages read: ");
        int newPages = int.Parse(Console.ReadLine());

        books[index].PagesRead = Math.Min(newPages, books[index].TotalPages);
        Console.WriteLine("Progress updated!");

        if (books[index].IsCompleted)
        {
            Console.Write("Enter date completed (mm/dd/yyyy): ");
            string dateCompleted = Console.ReadLine();

            books[index].DateCompleted = dateCompleted;
            Console.WriteLine("Congrats! You finished the book!");
        }
    }
}
