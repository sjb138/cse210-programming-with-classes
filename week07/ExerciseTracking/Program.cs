using System;

public abstract class Activity
{
    protected DateTime date;
    protected int duration; // Duration in minutes

    // Constructor to initialize date and duration
    public Activity(DateTime date, int duration)
    {
        this.date = date;
        this.duration = duration;
    }

    // Abstract methods to be overridden in derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Virtual method to get the summary of the activity
    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} {this.GetType().Name} ({duration} min): " +
               $"Distance {GetDistance()} miles, Speed {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

public class Running : Activity
{
    private double distance; // Distance in miles

    public Running(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        this.distance = distance;
    }

    // Override methods
    public override double GetDistance() => distance;
    public override double GetSpeed() => (distance / duration) * 60; // Speed = Distance / Time * 60
    public override double GetPace() => duration / distance; // Pace = Time / Distance
}

public class Cycling : Activity
{
    private double speed; // Speed in mph

    public Cycling(DateTime date, int duration, double speed)
        : base(date, duration)
    {
        this.speed = speed;
    }

    // Override methods
    public override double GetDistance() => (speed * duration) / 60; // Distance = Speed * Time / 60
    public override double GetSpeed() => speed;
    public override double GetPace() => 60 / speed; // Pace = 60 / Speed
}

public class Swimming : Activity
{
    private int laps; // Number of laps swum

    public Swimming(DateTime date, int duration, int laps)
        : base(date, duration)
    {
        this.laps = laps;
    }

    // Override methods
    public override double GetDistance() => laps * 50 / 1000.0; // Distance in kilometers (50m per lap)
    public override double GetSpeed() => (GetDistance() / duration) * 60; // Speed = Distance / Time * 60
    public override double GetPace() => duration / GetDistance(); // Pace = Time / Distance
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create instances of each activity type
        Activity running = new Running(new DateTime(2022, 11, 3), 30, 3.0);
        Activity cycling = new Cycling(new DateTime(2022, 11, 3), 45, 12.0);
        Activity swimming = new Swimming(new DateTime(2022, 11, 3), 60, 40);

        // Create a list to hold activities
        Activity[] activities = { running, cycling, swimming };

        // Display summaries for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
