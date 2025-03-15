using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int grade = int.Parse(Console.ReadLine());
        
        string letter;
        string sign = "";
        
        // Determine letter grade
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        
        // Determine sign for grades (except A+ and F+/F- cases)
        int lastDigit = grade % 10;
        if (letter != "A" && letter != "F")
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        
        Console.WriteLine($"Your grade is: {letter}{sign}");
        
        // Determine pass or fail
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! Good Job!");
        }
        else
        {
            Console.WriteLine("Try Again...Better luck next time!");
        }
    }
}
