using System;
using System.Collections.Generic;

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    // Constructor to initialize Comment
    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    // Constructor to initialize Video
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    // Method to get the number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Method to display video details
    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of comments: {GetNumberOfComments()}\n");

        // Display comments
        foreach (var comment in Comments)
        {
            Console.WriteLine($"Comment by {comment.Name}: {comment.Text}");
        }
        Console.WriteLine(); // Add an empty line between videos
    }
}

class Program
{
    static void Main()
    {
        // Create some Comment objects
        Comment comment1 = new Comment("MinecraftvillagerB0b", "This mustve been a blast to make!");
        Comment comment2 = new Comment("Revolutionary_Toenail", "That ending blew my mind WHATTTT");
        Comment comment3 = new Comment("Deadpool007", "Is it Thursday yet??? b r u h");
        Comment comment4 = new Comment("DAVE123456", "LOL'd at 1:12dotnet");

        // Create Video objects
        Video video1 = new Video("Sam Riegel's Dungeons & Dragons One-shot", "Critical Role", 300);
        video1.Comments.Add(comment1);
        video1.Comments.Add(comment2);
        video1.Comments.Add(comment3);

        Video video2 = new Video("Top 10 Lore Secrets in Genshin Impact", "Ningnews", 600);
        video2.Comments.Add(comment2);
        video2.Comments.Add(comment4);

        Video video3 = new Video("Keep Your Hand on a Ferrari longest to own it Challenge!", "Mr.Beast", 450);
        video3.Comments.Add(comment1);
        video3.Comments.Add(comment3);

        // Create a list of videos
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Iterate through the videos and display their details
        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}