using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Attendance
{
    public string StudentName { get; }
    public List<bool> Presence { get; } 

    public Attendance(string name, IEnumerable<char> marks)
    {
        StudentName = name;

        var list = marks
            .Select(m => m == '1' || m == 'Y' || m == 'y')
            .ToList();

        if (list.Count != 30)
            throw new ArgumentException($"Attendance data for {name} must have exactly 30 entries.");

        Presence = list;
    }

    public int Streak()
    {
        int maxStreak = 0, current = 0;
        foreach (var day in Presence)
        {
            if (day)
            {
                current++;
                maxStreak = Math.Max(maxStreak, current);
            }
            else
            {
                current = 0;
            }
        }
        return maxStreak;
    }

    public double AbsenceRate()
    {
        int absentDays = Presence.Count(p => !p);
        return absentDays / 30.0 * 100;
    }
}


namespace _27__Classroom_Attendance_Analyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var students = new List<Attendance>
        {
            new Attendance("Alice", "111111111111111111111111111111"), 
            new Attendance("Bob",   "111110000011111000001111100000"),
            new Attendance("Cara",  "000000000011111111111111111111"), 
            new Attendance("Dan",   "101010101010101010101010101010")  
        };

            double absenceThreshold = 20.0; 
            int minStreakRequired = 5;      

            Console.WriteLine("Attendance Report:");
            Console.WriteLine("------------------");

            foreach (var student in students)
            {
                double absenceRate = student.AbsenceRate();
                int streak = student.Streak();

                bool isDefaulter = absenceRate > absenceThreshold || streak < minStreakRequired;

                Console.WriteLine($"{student.StudentName,-5} | Streak: {streak,2} days | Absence Rate: {absenceRate,5:F1}% | {(isDefaulter ? "DEF" : "OK")}");
            }
        }
    }
}




