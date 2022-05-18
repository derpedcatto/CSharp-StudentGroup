using System;
using System.Collections;

namespace StudentGroup
{
    class Group : IEnumerable
    {
        public string name { get; set; }
        public string specialization { get; set; }
        public int number { get; set; }
        public int size { get; set; }
        Student[] arr;

        public Group()
        {
            Random rnd = new Random();
            this.name = "Group Name " + rnd.Next(10, 20);
            this.specialization = "Specialization Name " + rnd.Next(10, 20);
            this.number = rnd.Next(1, 20);
            this.size = 10;

            this.arr = new Student[this.size];
            for(int i = 0; i < this.size; i++)
            {
                this.arr[i] = new Student();
            }
        }
        public Group(int size)
        {
            Random rnd = new Random();
            this.name = "Group Name " + rnd.Next(10, 20);
            this.specialization = "Specialization Name " + rnd.Next(10, 20);
            this.number = rnd.Next(1, 20);
            this.size = size;

            this.arr = new Student[this.size];
            for (int i = 0; i < this.size; i++)
            {
                this.arr[i] = new Student();
            }
        }
        public Group(Student[] arr)
        {
            Random rnd = new Random();
            this.name = "Group Name " + rnd.Next(10, 20);
            this.specialization = "Specialization Name " + rnd.Next(10, 20);
            this.number = rnd.Next(1, 20);
            this.size = arr.Length;
            this.arr = arr;
        }
        public Group(Group other)
        {
            this.name = new string(other.name);
            this.specialization = new string(other.specialization);
            this.number = other.number;
            this.size = other.size;

            this.arr = new Student[other.size];
            for (int i = 0; i < this.size; i++)
            {
                arr[i] = new Student();
                arr[i].DeepCopy(other.arr[i]);
            }
        }
        public Student this[int index]
        {
            get { return arr[index]; }
            set { arr[index] = value; }
        }

        public void Print()
        {
            Console.WriteLine("Group " + number + ": " + name + "\n");
            Console.WriteLine("Specialization: " + specialization + "\n");
            foreach (Student i in arr)
            {
                Console.WriteLine("=========================");
                i.Print();
                Console.WriteLine("=========================\n\n");
            }
        }
        public void AddStudent()
        {
            size++;
            Array.Resize(ref arr, size);
            arr[size - 1] = new Student();
        }
        public void TransferStudent(int index, Group other)
        {
            other.size++;
            Array.Resize(ref other.arr, other.size);
            other.arr[other.size - 1].DeepCopy(arr[index]);

            if (index != size - 1)
            {
                for (int i = size - 1; i > index; i--)
                {
                    arr[i].DeepCopy(arr[i - 1]);
                }
            }

            size--;
            Array.Resize(ref arr, size);
        }
        public void ExpellBadCourse(double mark)
        {
            int[] _index = new int[size];
            int count = 0;

            /*Indexes of good students*/
            for (int i = 0; i < size; i++)
            {
                double avg = 0;
                for (int j = 0; j < arr[i].course.Length; j++)
                {
                    avg += arr[i].course[j];
                }
                if (avg / arr[i].course.Length > mark)
                {
                    count++;
                    _index[count - 1] = i;
                }
            }

            if (count == 0)
            {
                System.Console.WriteLine("All students are expelled! Adding new one...");

                size = 1;
                Array.Resize(ref arr, size);
                arr[0] = new Student();

                return;
            }

            int _tmpcount = 0;
            for (int i = 0; i < count; i++)
            {
                arr[i].DeepCopy(arr[_index[_tmpcount]]);
                _tmpcount++;
            }

            size = count;
            Array.Resize(ref arr, size);
        }
        public void ExpellWorstCourse()
        {
            double[] marks = new double[size];
            int worstindex = 0;

            /*Filling array with marks*/
            for (int i = 0; i < size; i++)
            {
                double avg = 0;
                for (int j = 0; j < arr[i].course.Length; j++)
                {
                    avg += arr[i].course[j];
                }
                marks[i] = avg;
            }

            /*Finding the worst student index*/
            for (int i = 0; i < size - 1; i++)
            {
                if (marks[i] > marks[i + 1])
                {
                    worstindex = i + 1;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(marks[i] + " ");
            }

            /*Deleting student*/
            for (int i = worstindex; i < size - 1; i++)
            {
                arr[i].DeepCopy(arr[i + 1]);
            }

            size--;

            Array.Resize(ref arr, size);
        }
        public void Sort(string option)
        {
            if (option == "Course")
            {
                Array.Sort(arr, new StudentCourseComparer());
            }
            else if (option == "Name")
            {
                Array.Sort(arr, new StudentNameComparer());
            }
            else if (option == "Age")
            {
                Array.Sort(arr, new StudentAgeComparer());
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public bool Search(string option, string value)
        {
            if (option == "FullName")
            {
                foreach(Student i in arr)
                {
                    string _name = i.name + " " +  i.surname + " " + i.middlename;

                    if (_name == value)
                    {
                        return true;
                    }
                }
            }
            else if (option == "Age")
            {
                foreach (Student i in arr)
                {
                    if (i.birthday.ToString() == value)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return arr.GetEnumerator();
        }
    }

    class GroupEnum : IEnumerator
    {
        public Student[] _arr;
        int pos = -1;

        public GroupEnum(Student[] list)
        {
            _arr = list;
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }
        public Student Current
        {
            get
            {
                try
                {
                    return _arr[pos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            pos++;
            return (pos < _arr.Length);
        }
        public void Reset()
        {
            pos = -1;
        }
    }
}

/*
Поля:
- ссылка на массив студентов
- количество человек в группе
- название
- специализация
- номер курса

Методы:
- конструктор по умолчанию на 10 студентов (с генерацией данных)
- конструктор с одним параметром типа int (задаётся произвольное количество студентов)
- конструктор с параметром типа Student[] (новая группа формируется на основании уже существующего массива студентов)
- конструктор с параметром типа Group (создаётся точная копия группы)
- показ всех студентов группы (сначала - название и специализация группы, 
затем - порядковые номера, фамилии в алфавитном порядке и имена студентов)
- добавление студента в группу
- редактирование данных о студенте
- редактирование данных о группе
- перевод студента из одной группы в другую
- отчисление всех не сдавших сессию студентов
- отчисление одного самого неуспевающего студента.
 */
