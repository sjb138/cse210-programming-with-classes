using System;

partial class Program
{
    static void Main()
    {
        // Ask for first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        // Ask for last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        // Return introduction James Bond Style
        Console.WriteLine($"\nYour name is {lastName}, {firstName} {lastName}.");
    }
}
