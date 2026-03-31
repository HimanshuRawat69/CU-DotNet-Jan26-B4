using StudentAdaptiveSystem.Models;
using StudentAdaptiveSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdaptiveSystem.Services
{
    internal class StudentService :IStudentService
    {
        private IStudentRepository _repo { get; set; }
        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }
        public void AddStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.Name))
            {
                throw new ArgumentException("Name should be provided");
            }
            if (student.Grade < 0 || student.Grade > 100)
            {
                throw new ArgumentException("Grade should be in range.");
            }
            _repo.AddStudent(student);
        }

        public void DeleteStudent(int studId)
        {
            _repo.DeleteStudent(studId);
        }
        public void UpdateStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.Name))
            {
                throw new ArgumentException("Name should be provided");
            }
            if (student.Grade < 0 || student.Grade > 100)
            {
                throw new ArgumentException("Grade should be in range.");
            }
            _repo.UpdateStudent(student);
        }
        public IEnumerable<Student> GetStudents()
        {
            return _repo.GetStudent();
        }

    }
}
