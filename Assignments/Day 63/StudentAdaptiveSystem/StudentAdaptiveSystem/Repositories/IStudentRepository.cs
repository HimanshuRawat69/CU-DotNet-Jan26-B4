using StudentAdaptiveSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdaptiveSystem.Repositories
{
    internal interface IStudentRepository
    {
        void AddStudent(Student student);
        public IEnumerable<Student> GetStudent();
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
