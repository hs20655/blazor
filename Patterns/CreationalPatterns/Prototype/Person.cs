using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Prototype
{
    public class Person
    {
        public int? Age;
        public DateTime? BirthDate;
        public string Name;
        public IdInfo IdInfo;

        public Person(int? age, DateTime? birthDate, string name, IdInfo idInfo)
        {
            this.Age = age;
            this.BirthDate = birthDate;
            this.Name = name;
            this.IdInfo = idInfo;
        }
        public Person ShallowCopy()
        {
            return (Person)this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person)this.MemberwiseClone();
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = String.Copy(Name);
            return clone;
        }
    }
}
