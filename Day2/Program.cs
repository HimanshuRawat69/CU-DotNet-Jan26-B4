using System;

class Program
{
    static void Main()
    {
        // Question 1
        int daysPresent = 68;
        int totalDays = 90;

        double attPercent = (daysPresent * 100.0) / totalDays;
        int attDisplay = (int)Math.Round(attPercent);

        Console.WriteLine("Attendance Percentage: " + attDisplay);


        // Question 2
        int m1 = 78;
        int m2 = 82;
        int m3 = 91;

        double avg = (m1 + m2 + m3) / 3.0;
        Console.WriteLine("Average Marks: " + Math.Round(avg, 2));

        int scholarshipAvg = (int)Math.Round(avg);
        Console.WriteLine("Scholarship Marks: " + scholarshipAvg);


        // Question 3
        decimal fine = 2.50m;
        int late = 4;

        decimal totalFine = fine * late;
        double logFine = (double)totalFine;

        Console.WriteLine("Total Fine: " + totalFine);


        // Question 4
        decimal bal = 100000m;
        float rateFromApi = 7.5f;

        decimal monthlyRate = (decimal)rateFromApi / 100;
        decimal interest = bal * monthlyRate / 12;

        bal = bal + interest;
        Console.WriteLine("Updated Balance: " + bal);


        // Question 5
        double cartValue = 1999.99;
        decimal finalCart = (decimal)cartValue;

        decimal tax = finalCart * 0.18m;
        Console.WriteLine("Final Amount: " + (finalCart + tax));


        // Question 6
        short reading = 320;
        double temp = reading / 10.0;

        int displayTemp = (int)Math.Round(temp);
        Console.WriteLine("Temperature: " + displayTemp + "°C");


        // Question 7
        double score = 86.4;
        byte grade;

        if (score >= 90)
            grade = 10;
        else if (score >= 80)
            grade = 9;
        else
            grade = 8;

        Console.WriteLine("Grade: " + grade);


        // Question 8
        long usedBytes = 5368709120;
        double gb = usedBytes / (1024.0 * 1024 * 1024);

        int roundedGb = (int)Math.Round(gb);
        Console.WriteLine("Data Used: " + roundedGb + " GB");


        // Question 9
        int stock = 45000;
        ushort maxStock = 50000;

        if (stock <= maxStock)
            Console.WriteLine("Stock is within limit");
        else
            Console.WriteLine("Stock exceeded");


        // Question 10
        int basic = 40000;
        double allow = 12500.75;
        double deduct = 3200.50;

        decimal net =
            basic +
            (decimal)allow -
            (decimal)deduct;

        Console.WriteLine("Net Salary: " + net);
    }
}

