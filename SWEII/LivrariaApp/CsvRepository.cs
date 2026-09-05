using System;
using System.IO;
using System.Linq;

namespace LivrariaApp
{
    public static class CsvRepository
    {
        public static void SaveBook(Book book, string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Name;Price;Qty;Authors\n");
            }

            var authorData = string.Join("|", book.GetAuthors().Select(a => $"{a.GetName()},{a.GetEmail()},{a.GetGender()}"));
            var line = $"{book.GetName()};{book.GetPrice()};{book.GetQty()};{authorData}\n";
            File.AppendAllText(filePath, line);
        }
    }
}
