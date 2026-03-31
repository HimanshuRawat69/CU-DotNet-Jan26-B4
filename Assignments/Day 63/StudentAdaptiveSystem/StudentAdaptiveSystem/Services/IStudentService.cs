using StudentAdaptiveSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdaptiveSystem.Services
{
    internal interface IStudentService
    {
        void AddStudent(Student student);
        void DeleteStudent(int studId);
        void UpdateStudent(Student student);    

        public IEnumerable<Student> GetStudents();
    }
}
