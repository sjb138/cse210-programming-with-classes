using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string input;
        bool isRunning = true;
        Pokemon playerPokemon = null;
        List<Goal> goals = new List<Goal>();
        int score = 0;

        Console.WriteLine("Welcome to Eternal Quest!");

        Console.WriteLine("Choose your starter Pokemon:");
        Console.WriteLine("1. Bulbasaur");
        Console.WriteLine("2. Squirtle");
        Console.WriteLine("3. Charmander");

        input = Console.ReadLine();
        if (input == "1")
        {
            playerPokemon = new Pokemon("Bulbasaur", 1, "Ivysaur", "Venusaur");
        }
        else if (input == "2")
        {
            playerPokemon = new Pokemon("Squirtle", 1, "Wartortle", "Blastoise");
        }
        else if (input == "3")
        {
            playerPokemon = new Pokemon("Charmander", 1, "Charmeleon", "Charizard");
        }

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Show Score");
            Console.WriteLine("7. View Pokemon Stats");
            Console.WriteLine("8. Quit");
            Console.Write("Choose an option: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateGoal(goals);
                    break;
                case "2":
                    ListGoals(goals);
                    break;
                case "3":
                    RecordEvent(goals);
                    break;
                case "4":
                    SaveGoals(goals);
                    break;
                case "5":
                    LoadGoals(goals);
                    break;
                case "6":
                    ShowScore(score);
                    break;
                case "7":
                    ViewPokemonStats(playerPokemon);
                    break;
                case "8":
                    isRunning = false;
                    break;
            }
        }
    }

    static void CreateGoal(List<Goal> goals)
    {
        Console.Clear();
        Console.WriteLine("Choose the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string input = Console.ReadLine();

        if (input == "1")
        {
            CreateSimpleGoal(goals);
        }
        else if (input == "2")
        {
            CreateEternalGoal(goals);
        }
        else if (input == "3")
        {
            CreateChecklistGoal(goals);
        }
    }

    static void CreateSimpleGoal(List<Goal> goals)
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points for goal: ");
        int points = int.Parse(Console.ReadLine());
        goals.Add(new SimpleGoal(name, description, points));
    }

    static void CreateEternalGoal(List<Goal> goals)
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points for goal: ");
        int points = int.Parse(Console.ReadLine());
        goals.Add(new EternalGoal(name, description, points));
    }

    static void CreateChecklistGoal(List<Goal> goals)
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points for goal: ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("How many steps in the checklist? ");
        int steps = int.Parse(Console.ReadLine());
        goals.Add(new ChecklistGoal(name, description, points, steps));
    }

    static void ListGoals(List<Goal> goals)
    {
        Console.Clear();
        Console.WriteLine("Your Goals:");
        foreach (Goal goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
        Console.WriteLine();
    }

    static void RecordEvent(List<Goal> goals)
    {
        Console.Clear();
        Console.WriteLine("Choose a goal to record an event for:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetailsString()}");
        }
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordEvent();
        }
    }

    static void SaveGoals(List<Goal> goals)
    {
        Console.WriteLine("Saving goals...");
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.ToSaveString());
            }
        }
    }

    static void LoadGoals(List<Goal> goals)
    {
        Console.WriteLine("Loading goals...");
        goals.Clear();
        using (StreamReader reader = new StreamReader("goals.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Goal goal = Goal.LoadGoal(line);
                goals.Add(goal);
            }
        }
    }

    static void ShowScore(int score)
    {
        Console.Clear();
        Console.WriteLine($"Your score is: {score}");
        Console.WriteLine();
    }

    static void ViewPokemonStats(Pokemon pokemon)
    {
        Console.Clear();
        Console.WriteLine($"Pokemon: {pokemon.Name} - Level {pokemon.Level}");
        Console.WriteLine($"XP: {pokemon.XP}/{pokemon.GetNextLevelXP()}");
        Console.WriteLine($"Stage: {pokemon.Stage}");
        Console.WriteLine();
    }
}

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public abstract void RecordEvent();
    public abstract string GetDetailsString();
    public abstract string ToSaveString();

    public static Goal LoadGoal(string savedData)
    {
        string[] parts = savedData.Split(',');
        string type = parts[0];

        if (type == "SimpleGoal")
        {
            return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
        }
        else if (type == "EternalGoal")
        {
            return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
        }
        else if (type == "ChecklistGoal")
        {
            return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
        }
        return null;
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            Console.WriteLine($"You completed the simple goal: {Name}");
        }
    }

    public override string GetDetailsString()
    {
        return $"{Name} ({Description}) - Points: {Points} - Completed: {IsCompleted}";
    }

    public override string ToSaveString()
    {
        return $"SimpleGoal,{Name},{Description},{Points},{IsCompleted}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        if (!IsCompleted)
        {
            Console.WriteLine($"You earned points for the eternal goal: {Name}");
        }
    }

    public override string GetDetailsString()
    {
        return $"{Name} ({Description}) - Points: {Points} - Completed: {IsCompleted}";
    }

    public override string ToSaveString()
    {
        return $"EternalGoal,{Name},{Description},{Points}";
    }
}

class ChecklistGoal : Goal
{
    public int Steps { get; set; }
    public int Progress { get; set; }

    public ChecklistGoal(string name, string description, int points, int steps)
        : base(name, description, points)
    {
        Steps = steps;
        Progress = 0;
    }

    public override void RecordEvent()
    {
        if (Progress < Steps)
        {
            Progress++;
            Console.WriteLine($"Progress for checklist goal: {Name}. Step {Progress}/{Steps}");
            if (Progress == Steps)
            {
                IsCompleted = true;
                Console.WriteLine($"You completed the checklist goal: {Name}");
            }
        }
    }

    public override string GetDetailsString()
    {
        return $"{Name} ({Description}) - Points: {Points} - Progress: {Progress}/{Steps} - Completed: {IsCompleted}";
    }

    public override string ToSaveString()
    {
        return $"ChecklistGoal,{Name},{Description},{Points},{Steps},{Progress}";
    }
}

class Pokemon
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int XP { get; set; }
    public string Stage { get; set; }
    public string Stage2 { get; set; }
    public string Stage3 { get; set; }

    public Pokemon(string name, int level, string stage2, string stage3)
    {
        Name = name;
        Level = level;
        XP = 0;
        Stage = name;
        Stage2 = stage2;
        Stage3 = stage3;
    }

    public void AddXP(int xp)
    {
        XP += xp;
        if (XP >= GetNextLevelXP())
        {
            Level++;
            XP = 0;
            Evolve();
        }
    }

    public int GetNextLevelXP()
    {
        return Level * 100;
    }

    private void Evolve()
    {
        if (Level == 14)
        {
            Stage = Stage2;
            Console.WriteLine($"Your {Name} evolved into {Stage2}!");
        }
        if (Level == 32)
        {
            Stage = Stage3;
            Console.WriteLine($"Your {Name} evolved into {Stage3}!");
        }
    }
}
