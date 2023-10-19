using lab1db;
using System.Text.RegularExpressions;

namespace logic
{
    public class People
    {
        EntitiesBidirectionalList<Entity> data = new();
        DB db = new();
        public People()
        {
            data = db.Load();
        }
        public void Insert(Entity input)
        {
            data.Push(input);
            db.Save(data);
        }
        public void Update(Entity input, int index)
        {
            data[index] = input;
            db.Save(data);
        }
        public void Delete(int index)
        {
            data.Delete(index);
            db.Save(data);
        }
        public void Save() => db.Save(data);
        public int Length() => data.Length;
        public static void ValidateName(string? name)
        {
            if (name == null || !Regex.Match(name, @"^\p{L}{1,32}$", RegexOptions.IgnoreCase).Success) throw new ArgumentException();
        }
        public static void ValidateID(string? id)
        {
            if (id == null || !Regex.Match(id, @"^КВ\d{8}$", RegexOptions.IgnoreCase).Success) throw new ArgumentException();
        }
        public static void ValidateCourse(int? course)
        {
            if (course < 1 || course > 6) throw new ArgumentException();
        }
        public static void ValidateMark(int? mark)
        {
            if (mark < 0 || mark > 5) throw new ArgumentException();
        }
        public static void ValidateCountry(string? country)
        {
            if (country == null || !Regex.Match(country, @"^(\p{L}| |-){1,32}$", RegexOptions.IgnoreCase).Success) throw new ArgumentException();
        }
        public static void ValidateForeignPassportNumber(string? number)
        {
            if (number == null || !Regex.Match(number, @"^(\p{L}|-|\d){1,32}$", RegexOptions.IgnoreCase).Success) throw new ArgumentException();
        }
        public List<Tuple<int, Student>> Search()
        {
            List<Tuple<int, Student>> Entities = new();
            if (data.Length == 0) { return  Entities; }
            for (int i=0;i < data.Length;i++)
            {
                Entity cur = data[i];
                if (cur is Student)
                {
                    Student student = cur as Student;
                    if (student is not null)
                    {
                        if (student.ForeignPassportNumber != null && student.Course == 1 && student.GPA == 5) Entities.Add(Tuple.Create(i, student));
                    }
                }
            }
            return Entities;
        }
        public Entity this[int position]
        {
            get => data[position];
            set => data.SetValue(position, value);
        }
    }
}
