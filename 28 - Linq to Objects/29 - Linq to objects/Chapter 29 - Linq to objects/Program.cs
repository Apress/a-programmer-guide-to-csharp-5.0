using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public static class Helpers
{
    public static double AverageV1(List<Assignment> values)
    {
        double sum = 0;
        foreach (Assignment assignment in values)
        {
            sum += assignment.Score;
        }

        return sum / values.Count;
    }

    public static double AverageV2(List<Assignment> values, Func<Assignment, int> selector)
    {
        double sum = 0;
        foreach (Assignment value in values)
        {
            sum += selector(value);
        }

        return sum / values.Count;
    }

    public static double AverageV3<T>(List<T> values, Func<T, int> selector)
    {
        double sum = 0;
        foreach (T value in values)
        {
            sum += selector(value);
        }

        return sum / values.Count;
    }

    public static double AverageV4<T>(this IEnumerable<T> values, Func<T, int> selector)
    {
        double sum = 0;
        int count = 0;
        foreach (T value in values)
        {
            sum += selector(value);
            count++;
        }

        return sum / count;
    }

    public static IEnumerable<T> OrderByMany<T>(this IEnumerable<T> values, params Func<T, double>[] selector)
    {
        return null;
    }

    public static double AverageDropTheLowestScore<T>(this IEnumerable<T> values, Func<T, double> selector)
    {
        double minValue = Double.MaxValue;

        double sum = 0;
        int count = 0;
        foreach (T value in values)
        {
            double valueToAdd = selector(value);
            if (valueToAdd < minValue)
            {
                minValue = valueToAdd;
            }

            sum += valueToAdd;
            count++;
        }
        sum -= minValue;
        count--;

        return sum / count;
    }

    private class EveryOtherEnumerator<T> : IEnumerable<T>
    {
        IEnumerable<T> m_baseEnumerable;

        public EveryOtherEnumerator(IEnumerable<T> baseEnurable)
        {
            m_baseEnumerable = baseEnurable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            int count = 0;
            foreach (T value in m_baseEnumerable)
            {
                if (count % 2 == 0)
                {
                    yield return value;
                }
                count++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public static IEnumerable<T> EveryOther<T>(this IEnumerable<T> values)
    {
        return new EveryOtherEnumerator<T>(values);
    }
}

public class Assignment
{
    public string Name { get; set; }
    public int Score { get; set; }
}


class Student
{
    public Student()
    {
        Assignments = new List<Assignment>();
    }

    public string Name { get; set; }
    public List<Assignment> Assignments { get; set; }

    public double GetAverageScore()
    {
        //return Helpers.AverageV1(Assignments);
        //return Helpers.AverageV2(Assignments, a => a.Score);
        //return Helpers.AverageV3(Assignments, a => a.Score);

        //double average = Assignments.Average(a => a.Score);

        Assignments.Where(a => a.Score != 0).Count();

        Assignments.Count(a => a.Score != 0);

        double average = (from assignment in Assignments
                          select assignment.Score).Average();


        return average;

    }

    public int CountOfAssignementsWithNonZeroScores()
    {
        int count = 0;
        foreach (Assignment assignment in Assignments)
        {
            if (assignment.Score == 0)
            {
                count++;
            }
        }

        return count;
    }
}

class NameAndGrade
{
    public NameAndGrade(string name, double average)
    {
        Name = name;
        Average = average;
    }

    public string Name { get; set; }
    public double Average { get; set; }
}

class Grader
{
    List<Student> _students = new List<Student>();

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    public void SortStudentsByAverageScore()
    {
        _students.OrderBy(s => s.Assignments.Average(a => a.Score)).Select(s => s.Name);


    }

    public void GetStudentsAndScores()
    {
        _students.Select(s => new NameAndGrade(s.Name, s.GetAverageScore()));

        var x = _students.Select(s => new
        {
            Name = s.Name,
            Average = s.GetAverageScore()
        });

        Console.WriteLine(x);

        var y = from s in _students
                select new { Name = s.Name, Average = s.GetAverageScore() };
    }


    public void GetStudentsAndScoresForShortNamedStudents()
    {
        _students.Select(s => new NameAndGrade(s.Name, s.GetAverageScore()));

        var x = _students
                    .Where(s => s.Name.Length < 5)
                    .Select(s => new
                    {
                        Name = s.Name,
                        Average = s.GetAverageScore()
                    });

        Console.WriteLine(x);

        var y = from s in _students
                where s.Name.Length < 5
                select new { Name = s.Name, Average = s.GetAverageScore() };


        _students.Where(s => s.Name.Length > 5).First();
        _students.First(s => s.Name.Length > 5);

        var nameAndAverages = _students.Select(
            s => new
            {
                Name = s.Name,
                Average = s.Assignments.Average(a => a.Score)
            });


        _students
            .OrderBy(s => s.Assignments.Count())
            .ThenBy(s => s.Assignments.Average(a => a.Score));


    }

#if fred
    from c in customers
group c by c.Country into g
select new { Country = g.Key, CustCount = g.Count() }
is translated into
from g in
	from c in customers
	group c by c.Country
select new { Country = g.Key, CustCount = g.Count() }
the final translation of which is
customers.
GroupBy(c => c.Country).
Select(g => new { Country = g.Key, CustCount = g.Count() })


#endif

    public void GetClassAverage()
    {
        var a1 = _students.Average(s => s.GetAverageScore());

        var a2_ = _students.Select(s => s.Assignments);

        var a3 = _students.SelectMany(s => s.Assignments).Average(a => a.Score);
    }

    public void PrintAverages()
    {
        foreach (Student student in _students)
        {
            Console.WriteLine("Name: {0}", student.Name);

            Console.WriteLine("Average: {0}", student.GetAverageScore());
        }
    }
}



class StudentEmail
{
    public string Name { get; set; }
    public string EmailAddress { get; set; }
}

class Demographics
{
    public Demographics(string name, int age, int zipCode, int salary)
    {
        Name = name;
        Age = age;
        ZipCode = zipCode;
        Salary = salary;
    }

    public string Name { get; set; }
    public int Age { get; set; }
    public int ZipCode { get; set; }
    public int Salary { get; set; }

    public override string ToString()
    {
        return String.Join(", ", Name, Age, ZipCode, Salary);
    }
}

class Program
{
    static void GroupByTest()
    {
        List<Demographics> data = new List<Demographics>();

        data.Add(new Demographics("Fred", 55, 98008, 55000));
        data.Add(new Demographics("Barney", 58, 98052, 125000));
        data.Add(new Demographics("Wilma", 38, 98008, 250000));
        data.Add(new Demographics("Dino", 12, 98001, 12000));
        data.Add(new Demographics("George", 55, 98001, 80000));
        data.Add(new Demographics("Elroy", 8, 98008, 8000));
        data.Add(new Demographics("Judy", 16, 98008, 18000));
        data.Add(new Demographics("Jane", 48, 98008, 251000));

        var x = data.GroupBy(d => d.ZipCode);

        foreach (var group in x)
        {
            Console.WriteLine(group.Key);
            foreach (var item in group)
            {
                Console.Write("    ");
                Console.WriteLine(item);
            }
        }

        var y = data
                    .GroupBy(d => d.ZipCode)
                    .Select(d => new
                    {
                        ZipCode = d.Key,
                        AverageSalary = d.Average(d2 => d2.Salary)
                    });

        foreach (var yy in y)
        {
            Console.WriteLine(yy.ZipCode + " " + yy.AverageSalary);
        }

    }


    static void JoinExample()
    {
        List<Student> students = new List<Student>();

        students.Add(new Student { Name = "John" });
        students[0].Assignments.Add(new Assignment { Score = 10 });
        students[0].Assignments.Add(new Assignment { Score = 20 });
        students[0].Assignments.Add(new Assignment { Score = 30 });

        students.Add(new Student { Name = "Bob" });
        students[1].Assignments.Add(new Assignment { Score = 10 });
        students[1].Assignments.Add(new Assignment { Score = 24 });
        students[1].Assignments.Add(new Assignment { Score = 11 });

        students.Add(new Student { Name = "Sally" });
        students[2].Assignments.Add(new Assignment { Score = 18 });
        students[2].Assignments.Add(new Assignment { Score = 2 });
        students[2].Assignments.Add(new Assignment { Score = 34 });


        List<StudentEmail> studentEmails = new List<StudentEmail>();

        studentEmails.Add(new StudentEmail { Name = "John", EmailAddress = "John@school.org" });
        studentEmails.Add(new StudentEmail { Name = "Bob", EmailAddress = "Robert@school.org" });
        studentEmails.Add(new StudentEmail { Name = "Tony", EmailAddress = "Anthony@school.org" });

        var joinedItems = students.Join(studentEmails,
            s => s.Name,
            e => e.Name,
            (s, e) => new
            {
                Name = s.Name,
                EmailAddress = e.EmailAddress,
                Average = s.GetAverageScore()
            });

        Console.WriteLine("Name,Average");
        foreach (Student student in students)
        {
            Console.WriteLine("{0},{1}", student.Name, student.GetAverageScore());
        }

        Console.WriteLine("Name,Email address");
        foreach (StudentEmail studentEmail in studentEmails)
        {
            Console.WriteLine("{0},{1}", studentEmail.Name, studentEmail.EmailAddress);
        }

        Console.WriteLine("Name,Average,Email address");
        foreach (var joinedItem in joinedItems)
        {
            Console.WriteLine("{0},{1},{2}",
                joinedItem.Name, joinedItem.Average, joinedItem.EmailAddress);
        }
    }

    static void Main(string[] args)
    {
        JoinExample();
        return;

        //GroupByTest();
        //return;

        var names = typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Select(m => m.Name)
            .Distinct()
            .OrderBy(m => m);

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        Grader grader = new Grader();

        Student student = new Student();
        student.Name = "Jane";
        student.Assignments.Add(new Assignment { Name = "Chapter 1", Score = 10 });
        student.Assignments.Add(new Assignment { Name = "Chapter 2", Score = 30 });
        student.Assignments.Add(new Assignment { Name = "Chapter 3", Score = 55 });

        grader.AddStudent(student);

        grader.PrintAverages();
        grader.GetStudentsAndScores();
        grader.GetClassAverage();

        // student: string name, List<

        List<object> objects = new List<object>();
        objects.Add("A");
        objects.Add("B");
        objects.Add("C");

        var sequenceOfString = objects.Cast<string>();

        List<Student> students = new List<Student>();

        Dictionary<string, Student> studentDictionary =
            students.ToDictionary(s => s.Name);

        int[] list1 = { 1, 3, 5, 7, 9 };
        int[] list2 = { 1, 2, 3, 5, 8, 13 };

        var unionOfLists = list1.Union(list2);
        foreach (int i in unionOfLists)
        {
            Console.WriteLine(i);
        }

        var sorted = students.OrderBy(s => s.Assignments.Count()).ThenBy(s => s.GetAverageScore());

        List<StudentEmail> studentEmails = new List<StudentEmail>();

        var joined = students.Join(studentEmails,
            s => s.Name,
            e => e.Name,
            (s, e) => new { Name = s.Name, EmailAddress = e.EmailAddress, Average = s.GetAverageScore() }
                );
        students
            .OrderBy(s => s.Assignments.Count())
            .ThenBy(s => s.Assignments.Average(a => a.Score));


        students
            .OrderByMany(
                s => s.Assignments.Count(),
                s => s.Assignments.Average(a => a.Score));


        var x = students
                    .Where(s => s.Name.Length < 5)
                    .Select(s => new
                    {
                        Name = s.Name,
                        Average = s.Assignments.Average(a => a.Score)
                    });

        var y = from s in students
                where s.Name.Length < 5
                select new
                {
                    Name = s.Name,
                    Average = s.Assignments.Average(a => a.Score)
                };

        int[] values = { 1, 4, 9, 16, 25, 36, 49, 64, 81, 100 };

        var everyOtherValue = values.EveryOther();
        foreach (int value in everyOtherValue)
        {
            Console.WriteLine(value);
        }


    }



}







