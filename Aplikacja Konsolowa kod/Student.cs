using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekt_lab;

namespace projekt_lab
{
    public class Student : Uzytkownik
    {
        public Student(string login, string haslo)
            : base(login, haslo, "Student")
        {
        }
    }
}