using System;
using System.Collections.Generic;
using System.Linq;

class Scripture
{
    private Reference Reference;
    private List<Word> Words;
    private Random random = new Random();

    public Scripture(string book, int chapter, int verse, string text)
    {
        Reference = new Reference(book, chapter, verse);
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public Scripture(string book, int chapter, int startVerse, int endVerse, string text)
    {
        Reference = new Reference(book, chapter, startVerse, endVerse);
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideWords(int count)
    {
        List<Word> visibleWords = Words.Where(w => !w.IsHidden).ToList();
        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(Reference);
        Console.WriteLine(string.Join(" ", Words));
    }

    public bool IsFullyHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}

class Reference
{
    private string Book;
    private int Chapter;
    private int StartVerse;
    private int? EndVerse;

    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = verse;
        EndVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {Chapter}:{StartVerse}" : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

class Word
{
    private string Text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture("John", 3, 16, "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life.");
        
        while (true)
        {
            scripture.Display();
            Console.WriteLine("Press Enter to hide more words, or type 'quit' to exit.");
            string input = Console.ReadLine();
            
            if (input.ToLower() == "quit")
                break;
            
            scripture.HideWords(2);
            
            if (scripture.IsFullyHidden())
            {
                scripture.Display();
                Console.WriteLine("All words are hidden! Press Enter to exit.");
                Console.ReadLine();
                break;
            }
        }
    }
}
