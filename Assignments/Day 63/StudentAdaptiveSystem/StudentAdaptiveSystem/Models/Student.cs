using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAdaptiveSystem.Models
{
    internal class Student
    {
        public int StudentId { get; set; }  
        public string Name { get; set; }
        public int Grade { get; set; } 
        override public string ToString()
        {
            return $"StudentId: {StudentId}, Name: {Name}, Grade: {Grade}";
        }
    }
}
