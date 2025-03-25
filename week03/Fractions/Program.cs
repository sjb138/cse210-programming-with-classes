using System;

class Fraction
{
    private int numerator;
    private int denominator;

    // Constructor 1: Default 1/1
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    // Constructor 2: Single integer (n/1)
    public Fraction(int top)
    {
        numerator = top;
        denominator = 1;
    }

    // Constructor 3: Two integers (n/d)
    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        numerator = top;
        denominator = bottom;
    }

    // Getters and Setters
    public int GetNumerator()
    {
        return numerator;
    }

    public void SetNumerator(int top)
    {
        numerator = top;
    }

    public int GetDenominator()
    {
        return denominator;
    }

    public void SetDenominator(int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        denominator = bottom;
    }

    // Returns fraction as "n/d"
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    // Returns decimal value of fraction
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}
