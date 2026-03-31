using StudentAdaptiveSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdaptiveSystem.Repositories
{
    internal class ListStudentRepository : IStudentRepository
    {
        private List<Student> students = new List<Student>();
        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public IEnumerable<Student> GetStudent()
        {
            return students;
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Grade = student.Grade;
            }
        }
        public void DeleteStudent(int id)
        {
            students.RemoveAll(s => s.StudentId == id);
        }
    }
}
