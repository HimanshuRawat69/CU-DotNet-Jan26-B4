namespace StudentsDictionary
{
    class Studentt
    {
        public int StudID { get; set; }
        public string SName { get; set; }
        public Studentt(int studID, string sName)
        {
            StudID = studID;
            SName = sName;
        }

        public override bool Equals(object? obj)
        {
            Studentt s = obj as Studentt;//(Studentt)obj
            return this.StudID == s.StudID && this.SName == s.SName;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(StudID, SName);
        }
    }
    class Operation
    {
        static Dictionary<Studentt, int> d = new Dictionary<Studentt, int>();
        public static void AddStudent(Studentt stud, int marks)
        {
            if (!d.ContainsKey(stud))
            {
                d.Add(stud, marks);
            }
            else
            {
                if (d[stud] < marks)
                {
                    d[stud] = marks;
                }
            }
        }
        public static void Display()
        {
            foreach (var item in d)
            {
                Console.WriteLine(item.Key.StudID + " " + item.Key.SName + " " + item.Value);
            }
        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            Studentt s1 = new Studentt(1, "Sneha");
            Operation.AddStudent(s1, 77);
            Studentt s2 = new Studentt(2, "Angel");
            Studentt s3 = new Studentt(3, "Chirag");
            Operation.AddStudent(s3, 80);
            Studentt s4 = new Studentt(3, "Chirag");
            Operation.AddStudent(s4, 90);
            Operation.Display();
        }
    }
}
