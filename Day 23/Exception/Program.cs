namespace ExceptionProject
{
    internal class Program
    {
        class InvalidStudentAgeException : Exception
        {
            public InvalidStudentAgeException(string message) : base(message) { }
        }
        class InvalidStudentNameException : Exception
        {
            public InvalidStudentNameException(string message) : base(message) { }
        }
        internal class Randomfun
        {
            static void Main(string[] args)
            {
                ValidateStudent();
            }
            static void ValidateStudent()
            {
                try
                {
                    Console.Write("Enter Student Name: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name) || int.TryParse(name, out _))
                    {
                        throw new InvalidStudentNameException("Please enter valid name.");
                    }

                    int age;
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter Student Age: ");
                            age = int.Parse(Console.ReadLine());

                            if (age < 18 || age > 60)
                            {
                                throw new InvalidStudentAgeException("Age must be between 18 and 60");
                            }
                            break;
                        }
                        catch (InvalidStudentAgeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    Console.WriteLine("Student enrollment successful");
                }
                catch (Exception ex)
                {
                    Exception wrappedException = new Exception("Student enrollment failed.", ex);

                    Console.WriteLine("Exception Details");
                    Console.WriteLine("Message: " + wrappedException.Message);
                    Console.WriteLine("Inner Exception: " + wrappedException.InnerException.Message);
                }
                finally
                {
                    Console.WriteLine("Operation Completed");
                }
            }
        }
    }
}