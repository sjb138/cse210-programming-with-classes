using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

abstract class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void ShowStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---");
        Console.WriteLine($"{_description}");
        Console.Write("\nEnter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    public void ShowEndingMessage()
    {
        Console.WriteLine("\nWell done!");
        ShowSpinner(2);
        Console.WriteLine($"\nYou completed {_name} for {_duration} seconds.");
        ShowSpinner(3);
    }

    public void ShowSpinner(int seconds)
    {
        DateTime end = DateTime.Now.AddSeconds(seconds);
        while (DateTime.Now < end)
        {
            foreach (char c in "|/-\\")
            {
                Console.Write(c);
                Thread.Sleep(200);
                Console.Write("\b");
            }
        }
    }

    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public abstract void Run();
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

    public override void Run()
    {
        ShowStartingMessage();
        int elapsed = 0;
        while (elapsed < _duration)
        {
            Console.Write("\nBreathe in... ");
            ShowCountdown(3);
            Console.Write("\nBreathe out... ");
            ShowCountdown(3);
            elapsed += 6;
        }
        ShowEndingMessage();
    }
}

class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience?",
        "What did you learn about yourself?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times when you showed strength and resilience.") { }

    public override void Run()
    {
        ShowStartingMessage();

        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine($"\n--- Prompt ---\n{prompt}\n");
        ShowSpinner(3);

        int elapsed = 0;
        while (elapsed < _duration)
        {
            string question = _questions[rand.Next(_questions.Count)];
            Console.WriteLine($"\n{question}");
            ShowSpinner(5);
            elapsed += 5;
        }

        ShowEndingMessage();
    }
}

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

    public override void Run()
    {
        ShowStartingMessage();

        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine($"\n--- Prompt ---\n{prompt}\n");
        Console.WriteLine("Begin listing items in 3 seconds...");
        ShowCountdown(3);
        Console.WriteLine("Start listing. Press Enter after each item. Leave blank to stop early.");

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                string item = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(item)) break;
                items.Add(item);
            }
        }

        Console.WriteLine($"\nYou listed {items.Count} items.");
        ShowEndingMessage();
    }
}

class JournalEntryActivity : Activity
{
    public JournalEntryActivity() : base(
        "Journal Entry Activity",
        "This activity will let you write a journal entry and reflect on your day.") { }

    public override void Run()
    {
        ShowStartingMessage();

        Console.Write("Enter the date for this journal entry (e.g., 2025-04-15): ");
        string date = Console.ReadLine();

        string filename = $"{date}_journal.txt";

        if (File.Exists(filename))
        {
            Console.WriteLine("\nA journal entry already exists for that date. Here it is:\n");
            Console.WriteLine(File.ReadAllText(filename));
            Console.WriteLine("\nWould you like to add to it or overwrite it? (a/o): ");
            string choice = Console.ReadLine().ToLower();

            if (choice == "o")
            {
                File.Delete(filename);
            }
        }

        Console.WriteLine("\nWrite your journal entry below. Press Enter on an empty line to finish.\n");

        using (StreamWriter writer = new StreamWriter(filename, append: true))
        {
            string line;
            do
            {
                line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                    writer.WriteLine(line);
            } while (!string.IsNullOrWhiteSpace(line));
        }

        Console.WriteLine($"\nJournal entry saved for {date}.");
        ShowEndingMessage();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string choice = "";
        while (choice != "5")
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflecting activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Start journal entry activity");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            Activity activity = null;

            if (choice == "1")
                activity = new BreathingActivity();
            else if (choice == "2")
                activity = new ReflectionActivity();
            else if (choice == "3")
                activity = new ListingActivity();
            else if (choice == "4")
                activity = new JournalEntryActivity();

            if (activity != null)
            {
                activity.Run();
                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("Goodbye!");
    }
}
