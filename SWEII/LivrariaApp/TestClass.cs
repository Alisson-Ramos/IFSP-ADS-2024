using System;

namespace LivrariaApp
{
    public class TestClass
    {
        public static void RunTests()
        {
            Console.WriteLine("=== Iniciando Testes da Classe Book e Author ===");
            
            // 1. Instância de um livro com mais de um autor
            Author author1 = new Author("J.R.R. Tolkien", "tolkien@example.com", 'M');
            Author author2 = new Author("Christopher Tolkien", "chris@example.com", 'M');
            
            Author[] authors = new Author[] { author1, author2 };
            Book myBook = new Book("O Silmarillion", authors, 59.90, 10);

            // 2. Demonstrar o uso de TODOS os métodos da classe Book
            Console.WriteLine($"getName(): {myBook.GetName()}");
            
            Console.WriteLine("getAuthors():");
            foreach(var a in myBook.GetAuthors())
            {
                Console.WriteLine($" - {a.ToString()}");
            }
            
            Console.WriteLine($"getPrice(): {myBook.GetPrice()}");
            
            Console.WriteLine("setPrice(65.00)...");
            myBook.SetPrice(65.00);
            Console.WriteLine($"getPrice() após alteração: {myBook.GetPrice()}");
            
            Console.WriteLine($"getQty(): {myBook.GetQty()}");
            
            Console.WriteLine("setQty(15)...");
            myBook.SetQty(15);
            Console.WriteLine($"getQty() após alteração: {myBook.GetQty()}");
            
            Console.WriteLine($"getAuthorNames(): {myBook.GetAuthorNames()}");
            
            Console.WriteLine($"toString(): {myBook.ToString()}");
            
            // Demonstrando a conexão com repositório CSV
            Console.WriteLine("\n=== Salvando no Repositório CSV ===");
            string filePath = "books.csv";
            CsvRepository.SaveBook(myBook, filePath);
            Console.WriteLine("Livro salvo com sucesso em books.csv!");
        }
    }
}
