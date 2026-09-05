using System;
using System.Linq;

namespace LivrariaApp
{
    public class Book
    {
        private string name;
        private Author[] authors;
        private double price;
        private int qty = 0;

        public Book(string name, Author[] authors, double price)
        {
            this.name = name;
            this.authors = authors;
            this.price = price;
        }

        public Book(string name, Author[] authors, double price, int qty)
        {
            this.name = name;
            this.authors = authors;
            this.price = price;
            this.qty = qty;
        }

        public string GetName() => name;
        public Author[] GetAuthors() => authors;
        public double GetPrice() => price;
        public void SetPrice(double price) => this.price = price;
        public int GetQty() => qty;
        public void SetQty(int qty) => this.qty = qty;

        public override string ToString()
        {
            string authorsString = string.Join(",", authors.Select(a => a.ToString()));
            return $"Book[name={name},authors={{{authorsString}}},price={price},qty={qty}]";
        }

        public string GetAuthorNames()
        {
            return string.Join(",", authors.Select(a => a.GetName()));
        }
    }
}
