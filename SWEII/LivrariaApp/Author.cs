using System;

namespace LivrariaApp
{
    public class Author
    {
        private string name;
        private string email;
        private char gender;

        public Author(string name, string email, char gender)
        {
            this.name = name;
            this.email = email;
            this.gender = gender;
        }

        public string GetName() => name;
        public string GetEmail() => email;
        public char GetGender() => gender;

        public override string ToString()
        {
            return $"Author[name={name},email={email},gender={gender}]";
        }
    }
}
