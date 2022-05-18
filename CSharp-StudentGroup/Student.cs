using System;

namespace StudentGroup
{
    class Student : IComparable<Student>
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string middlename { get; set; }
        public string address { get; set; }
        public string phonenum { get; set; }
        public DateOnly birthday { get; set; }
        public int[] pass { get; set; }
        public int[] course { get; set; }
        public int[] exam { get; set; }
        Random rnd = new Random();

        public Student(string name)
        {
            
            this.name = name;
            this.surname = "B " + rnd.Next(9);
            this.middlename = "C " + rnd.Next(9);
            this.address = "Address " + rnd.Next(9);
            this.phonenum = "1234567890";
            this.birthday = new DateOnly(2002, 12, 27);
            pass = new int[7];
            course = new int[7];
            exam = new int[7];
            for (int i = 0; i < 7; i++)
            {
                pass[i] = rnd.Next(1, 12);
                course[i] = rnd.Next(1, 12);
                exam[i] = rnd.Next(1, 12);
            }
        }
        public Student(string name, string surname, string middlename)
        {
            this.name = name;
            this.surname = surname;
            this.middlename = middlename;
            this.address = "Address " + rnd.Next(9);
            this.phonenum = "1234567890";
            this.birthday = new DateOnly(2002, 12, 27);
            pass = new int[7];
            course = new int[7];
            exam = new int[7];
            for (int i = 0; i < 7; i++)
            {
                pass[i] = rnd.Next(1, 12);
                course[i] = rnd.Next(1, 12);
                exam[i] = rnd.Next(1, 12);
            }
        }
        public Student(string name, string surname, string middlename, int year, int month, int day, string address, string phonenum)
        {
            this.name = name;
            this.surname = surname;
            this.middlename = middlename;
            this.address = address;
            this.phonenum = phonenum;
            this.birthday = new DateOnly(year, month, day);
            pass = new int[7];
            course = new int[7];
            exam = new int[7];
            for (int i = 0; i < 7; i++)
            {
                pass[i] = rnd.Next(1, 12);
                course[i] = rnd.Next(1, 12);
                exam[i] = rnd.Next(1, 12);
            }
        }
        public Student()
        {
            this.name = "A " + rnd.Next(9);
            this.surname = "B " + rnd.Next(9);
            this.middlename = "C " + rnd.Next(9);
            this.address = "Address " + rnd.Next(9);
            this.phonenum = "1234567890";
            this.birthday = new DateOnly(2002, 12, 27);
            pass = new int[7];
            course = new int[7];
            exam = new int[7];
            for (int i = 0; i < 7; i++)
            {
                pass[i] = rnd.Next(1, 12);
                course[i] = rnd.Next(1, 12);
                exam[i] = rnd.Next(1, 12);
            }
        }

        public void Print()
        {
            Console.WriteLine("Full name: " + name + " " + surname + " " + middlename);
            Console.WriteLine("Date of birth: " + birthday);
            Console.WriteLine("Address: " + address);
            Console.WriteLine("Phone number: " + phonenum);

            Console.Write("Pass: ");
            for (int i = 0; i < pass.Length; i++)
            {
                Console.Write(pass[i] + " ");
            }
            Console.WriteLine("");

            Console.Write("Course: ");
            for (int i = 0; i < course.Length; i++)
            {
                Console.Write(course[i] + " ");
            }
            Console.WriteLine("");

            Console.Write("Exam: ");
            for (int i = 0; i < exam.Length; i++)
            {
                Console.Write(exam[i] + " ");
            }
            Console.WriteLine("");
        }
        public void DeepCopy(Student other)
        {
            name = other.name;
            surname = other.surname;
            middlename = other.middlename;
            address = other.address;
            phonenum = other.phonenum;
            birthday = other.birthday;
            for (int i = 0; i < pass.Length; i++)
            {
                pass[i] = other.pass[i];
                course[i] = other.course[i];
                exam[i] = other.exam[i];
            }
        }

        public int CompareTo(Student other)
        {
            double avg1 = 0;
            double avg2 = 0;
            for (int i = 0; i < course.Length; i++)
            {
                avg1 += course[i];
                avg2 += other.course[i];
            }

            if (avg1 < avg2)
            {
                return -1;
            }
            else if (avg1 > avg2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    class StudentCourseComparer : IComparer<Student>
    {
        public int Compare(Student a, Student b)
        {
            return a.CompareTo(b);
        }
    }
    class StudentNameComparer : IComparer<Student>
    {
        public int Compare(Student a, Student b)
        {
            if (String.Compare(a.name, b.name) == 1)
            {
                return 1;
            }
            else if (String.Compare(a.name, b.name) == -1)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
    class StudentAgeComparer : IComparer<Student>
    {
        public int Compare(Student a, Student b)
        {
            if (a.birthday > b.birthday)
            {
                return 1;
            }
            else if (a.birthday < b.birthday)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}

/*
Поля:
- ФИО
- дата рождения
- домашний адрес
- номер телефона
- зачёты[]
- курсовые работы[]
- экзамены[]

Методы:
- 3 конструктора с параметрами
- геттеры и сеттеры для всех полей
- показ всех данных о студенте.
 */