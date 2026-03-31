using StudentAdaptiveSystem.Models;
using StudentAdaptiveSystem.Repositories;
using StudentAdaptiveSystem.Services;

namespace StudentAdaptiveSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Json File or List(2)");
            var repoOptions = int.Parse(Console.ReadLine());

            //var student = GetStudent();

            IStudentRepository repo = null;
            switch (repoOptions)
            {
                case 1:
                    repo = new JsonStudentRepository();
                    break;
                case 2:
                    repo = new ListStudentRepository();
                    break;
                default:
                    Console.WriteLine("invalid choice");
                    break;
            }

            StudentService service = new StudentService(repo);//interface ke through dependency injection kraya 


            while (true)
            {
                Console.WriteLine("1.ADD  2.GET  3.UPDATE  4.DELETE  5.STOP");
                int opt = int.Parse(Console.ReadLine());
                try
                {
                    switch (opt)
                    {
                        case 1:
                            var student = GetStudent();
                            service.AddStudent(student);
                            break;
                        case 2:
                            var students = service.GetStudents();
                            DisplayStudents(students);
                            break;
                        case 3:
                            var updStu = GetStudent();
                            service.UpdateStudent(updStu);
                            break;
                        case 4:
                            Console.WriteLine("enter id to delete");
                            int id = int.Parse(Console.ReadLine());
                            service.DeleteStudent(id);
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("invalid option");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            

        }
        static Student GetStudent()
        {
            
            //Student student = new Student
            //{
            //    StudentId = 1,
            //    Name = "sneha",
            //    Grade = 85
            //};
            //return student;
            Student student = new Student();
            Console.WriteLine("Enter Student Id:");
            student.StudentId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Student Name:");
            student.Name = Console.ReadLine();
            Console.WriteLine("Enter Student Grade:");
            student.Grade = int.Parse(Console.ReadLine());
            return student;
        }
        static void DisplayStudents(IEnumerable<Student> students)
        {
            foreach (var s in students)
            {
                Console.WriteLine(s);
            }
        }
    }
}
