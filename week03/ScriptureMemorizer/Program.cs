class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int? _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        return _endVerse == null ? $"{_book} {_chapter}:{_startVerse}" : $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}


class Word
{
    private string _text;
    private bool _isHidden;

    public bool IsHidden => _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        if (!_isHidden)
        {
            _isHidden = true;
        }
    }

    public override string ToString()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}



using System;
using System.Collections.Generic;
using System.Linq;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(string book, int chapter, int verse, string text)
    {
        _reference = new Reference(book, chapter, verse);
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public Scripture(string book, int chapter, int startVerse, int endVerse, string text)
    {
        _reference = new Reference(book, chapter, startVerse, endVerse);
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideWords(int count)
    {
        List<Word> visibleWords = _words.Where(w => !w.IsHidden).ToList();
        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(_reference);
        Console.WriteLine(string.Join(" ", _words));
    }

    public bool IsFullyHidden()
    {
        return _words.All(w => w.IsHidden);
    }
}



using System;

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture("John", 3, 16, 
            "For God so loved the world that he gave his only begotten Son " +
            "that whosoever believeth in him should not perish but have everlasting life.");

        while (true)
        {
            scripture.Display();
            Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit.");
            string input = Console.ReadLine();
            
            if (input.ToLower() == "quit")
                break;
            
            scripture.HideWords(2);
            
            if (scripture.IsFullyHidden())
            {
                scripture.Display();
                Console.WriteLine("\nAll words are hidden! Press Enter to exit.");
                Console.ReadLine();
                break;
            }
        }
    }
}
