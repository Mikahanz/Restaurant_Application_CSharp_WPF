using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Application_CSharp_WPF
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public int Salary { get; set; }

        public List<PhoneNumbers> PhoneNumbers { get; set; }


        public List<Student> generateStudents()
        {
            List<Student> students = new List<Student>();


            students.Add(new Student()
            {
                ID = 1,
                FirstName = "Michael",
                LastName = "Hanzel",
                Gender = Gender.Male,
                Salary = 2000,
                PhoneNumbers = new List<PhoneNumbers>()
                { new PhoneNumbers()
                  {
                      StudentID = 1,
                      PhoneNo = "514-699-6969"
                  },
                  new PhoneNumbers()
                  {
                      StudentID = 1,
                      PhoneNo = "514-688-5874"
                  }
                                                                                                                                                     }
            });
            students.Add(new Student()
            {
                ID = 2,
                FirstName = "Peter",
                LastName = "Kazu",
                Gender = Gender.Male,
                Salary = 3000,
                PhoneNumbers = new List<PhoneNumbers>()
                                                                                                                                                     { new PhoneNumbers() {StudentID = 2, PhoneNo = "514-699-4785" },
                                                                                                                                                        new PhoneNumbers() {StudentID = 2, PhoneNo = "514-688-7447"}
                                                                                                                                                     }
            });
            students.Add(new Student()
            {
                ID = 3,
                FirstName = "Boris",
                LastName = "Viau",
                Gender = Gender.Male,
                Salary = 1500,
                PhoneNumbers = new List<PhoneNumbers>()
                                                                                                                                                     { new PhoneNumbers() {StudentID = 3, PhoneNo = "514-699-7157" },
                                                                                                                                                        new PhoneNumbers() {StudentID = 3, PhoneNo = "514-688-7575"}
                                                                                                                                                     }
            });
            students.Add(new Student()
            {
                ID = 4,
                FirstName = "Via",
                LastName = "Angel",
                Gender = Gender.Female,
                Salary = 4000,
                PhoneNumbers = new List<PhoneNumbers>()
                                                                                                                                                     { new PhoneNumbers() {StudentID = 4, PhoneNo = "514-699-6969" },
                                                                                                                                                        new PhoneNumbers() {StudentID = 4, PhoneNo = "514-688-75758"}
                                                                                                                                                     }
            });
            students.Add(new Student()
            {
                ID = 5,
                FirstName = "Jesus",
                LastName = "Maria",
                Gender = Gender.Female,
                Salary = 2500,
                PhoneNumbers = new List<PhoneNumbers>()
                                                                                                                                                     { new PhoneNumbers() {StudentID = 5, PhoneNo = "514-699-7854" },
                                                                                                                                                        new PhoneNumbers() {StudentID = 5, PhoneNo = "514-688-5863"}
                                                                                                                                                     }
            });



            return students;
        }



    }

    public class PhoneNumbers
    {
        public int StudentID { get; set; }
        public string PhoneNo { get; set; }


        public List<PhoneNumbers> generatePhoneNumbers()
        {
            List<PhoneNumbers> phoneNumbers = new List<PhoneNumbers>();
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 1, PhoneNo = "514-699-6661" });
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 2, PhoneNo = "514-699-5896" });
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 5, PhoneNo = "514-699-5841" });
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 1, PhoneNo = "514-699-5891" });
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 2, PhoneNo = "514-699-5632" });
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 3, PhoneNo = "514-699-8523" });
            phoneNumbers.Add(new PhoneNumbers() { StudentID = 2, PhoneNo = "514-699-8549" });

            return phoneNumbers;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
