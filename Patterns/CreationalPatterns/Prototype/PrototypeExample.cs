using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CreationalPatterns.Prototype
{
    public class PrototypeExample
    {
        private Person person1; //Prototype for person 2 and 3
        private Person person2;
        private Person person3;

        public PrototypeExample(int? age, DateTime? dateBirthday, string name, int? personId)
        {
            // create prototype
            person1 = new Person(age, dateBirthday, name, new IdInfo(personId));
            //Clone person1 Shallow Copy (will not clone inner class property like IdInfo)
            person2 = person1.ShallowCopy();
            //Clone peron 1 Deep Copy (Will clone all members)
            person3 = person1.DeepCopy();
        }

        public List<Person> GetPrototypeAndClones()
        {

            return new List<Person> { person1, person2, person3 };
        }
        public void ModifyPrototype(int? age, DateTime? dateBirthday, string name, int? personId)
        {
            //Modification in prototype
            person1.Age = age;
            person1.BirthDate = dateBirthday;
            person1.Name = name;
            person1.IdInfo.IdNumber = personId;
        }
    }
}
