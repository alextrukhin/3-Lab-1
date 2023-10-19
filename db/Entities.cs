using db;
using System.Text.Json.Serialization;

namespace lab1db
{
    public interface IEntity
    {
        public string LastName { get; set; }
        public string[] Methods { get; }
        public string ToString();
    }
    public class Entity : IEntity
    {
        string lastName;
        public string LastName { get => lastName; set => lastName = value; }
        [JsonConstructor]
        public Entity(string LastNameInput)
        {
            lastName = LastNameInput;
        }
        public virtual string[] Methods { get { return new string[] { "Nothing" }; } }
        public string Nothing()
        {
            return LastName + " does nothing";
        }
        public override string ToString() => lastName;
    }
    public class Student : Entity, IStudy
    {
        private string studentID;
        private int? gpa;
        private int? course;
        private string? country;
        private string? foreignPassportNumber;
        public int? Course { get => course; set => course = value; }
        public string StudentID { get => studentID; set => studentID = value; }
        public int? GPA { get => gpa; set => gpa = value; }
        public string? Country { get => country; set => country = value; }
        public string? ForeignPassportNumber { get => foreignPassportNumber; set => foreignPassportNumber = value; }
        public Student(string LastNameInput, string StudentIDInput) : base(LastNameInput)
        {
            studentID = StudentIDInput;
        }
        [JsonConstructor]
        public Student(int? Course, string StudentID, int? GPA, string? Country, string? ForeignPassportNumber, string LastName) : base(LastName)
        {
            (studentID, gpa, course, country, foreignPassportNumber) = (StudentID, GPA, Course, Country, ForeignPassportNumber);
        }
        public Student(string LastName, string StudentID, int? Course, int? GPA, string? Country, string? ForeignPassportNumber) :
            this(Course, StudentID, GPA, Country, ForeignPassportNumber, LastName)
        { }
        public string Study()
        {
            course = course == 6 ? 1 : course + 1;
            return LastName + " is now studing in " + course + " course";
        }
        public override string[] Methods { get { return base.Methods.Union(new string[] { "Study" }).ToArray(); } }
        public override string ToString() =>
            "Student - " + LastName +
            ", StudentID: " + studentID +
            ", Course: " + Course +
            ", GPA: " + GPA +
            ", Country: " + Country +
            ", ForeignPassportNumber: " + ForeignPassportNumber;
    }
    public class Tailor : Entity, IRepair
    {
        public Tailor(string LastName) : base(LastName)
        {
        }
        public string Repair()
        {
            return LastName + " repaired something for you!";
        }
        public override string[] Methods { get { return base.Methods.Union(new string[] { "Repair" }).ToArray(); } }
        public override string ToString() => "Tailor - " + LastName;
    }
    public class Singer : Entity, ISing
    {
        public Singer(string LastName) : base(LastName)
        {
        }
        public string Sing()
        {
            return LastName + " sings for you!";
        }
        public override string[] Methods { get { return base.Methods.Union(new string[] { "Sing" }).ToArray(); } }
        public override string ToString() => "Singer - " + LastName;
    }
}