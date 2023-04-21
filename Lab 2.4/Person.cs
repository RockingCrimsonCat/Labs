using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_2._4
{
    class Person
    {
        protected int Age { get; set; }
        protected string Name { get; set; }
        protected string SurName { get; set; }
        protected string FullName { get; set; }
        protected string FullInfo { get; set; }
        public override string ToString()
        {
            FullName = $"{Name} {SurName}";
            return FullName;
        }
        public override int GetHashCode()
        {
            return FullInfo.GetHashCode();
        }
        public override bool Equals(object? obj)
        {

            if (obj is Person person)
            {
                return FullInfo.GetHashCode() == person.FullInfo.GetHashCode();
            }
            return false;

        }
        public string Attendance(bool info)
        {
            if (info)
                return "In the class.";
            else
                return "Out of the class.";
        }
        public string GetInfo(string name, string surName, int age, bool attendance)
        {
            Name = name;
            SurName = surName;
            FullName = ToString();
            int hashCode = FullName.GetHashCode();
            Age = age;
            FullInfo = $"{FullName}. Age: {Age}\n{Attendance(attendance)}\n";
            return FullInfo;
        }
    }
    class Teacher : Person
    {

        public string CheckHomework(bool info)
        {
            if (!info)
                return "Homework was not checked.";
            else
                return "Homework was checked.";
        }
    }
        class Student : Person
        {

            public string InfoStudent(string name,string surName, int age, int course, bool attendance, string checkedHomework)
            {
                return $"Course: {course}\n{GetInfo(name,surName,age,attendance)}{checkedHomework}\n";
            }
        }
    }

