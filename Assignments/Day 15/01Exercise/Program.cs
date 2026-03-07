namespace _01Exercise
{
    class Height
    {
        public int Feet { get; set; }
        public double Inches { get; set; }

        public Height()
        {
            Feet = 0;
            Inches = 0;
        }

        public Height(int Feet, double Inches)
        {
            this.Feet = Feet;
            this.Inches = Inches;
        }

        public Height AddHeights(Height h1)
        {
            int f = this.Feet + h1.Feet;
            double inch = this.Inches + h1.Inches;

            f += (int)inch / 12;
            inch = inch % 12;

            Height h3 = new Height(f, inch);

            return h3;
        }

        public override string ToString()
        {
            return $"Height = {Feet} feet {Inches:F1} inches";
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Height person1 = new Height()
            {
                Feet = 5,
                Inches = 6.5,
            };

            Height person2 = new Height()
            {
                Feet = 7,
                Inches = 9,
            };

            Height person3 = new Height();
            person3 = person2.AddHeights(person1);

            Console.WriteLine(person1);
            Console.WriteLine(person2);
            Console.WriteLine(person3);
        }
    }
}
