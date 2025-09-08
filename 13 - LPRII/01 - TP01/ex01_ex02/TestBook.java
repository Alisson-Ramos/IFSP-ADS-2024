package ex01_ex02;
public class TestBook {
    public static void main(String[] args) {
        Author[] authors = new Author[3];
        authors[0] = new Author("Autor 01", "autor01@somewhere.com.br", 'm');
        authors[1] = new Author("Autor 02", "autor02@somewhere.com.br", 'f');

        Book testeBook = new Book("Java for Dummy", authors, 19.99, 99);

        System.out.println(testeBook.toString());
        System.out.println(testeBook.getAuthorNames());
        System.out.println(testeBook.getName());
        System.out.println(testeBook.getPrice());
        System.out.println(testeBook.getQty());
        Author[] autores = testeBook.getAuthors();

        for(int i = 0; i < (autores.length - 1) ; i++){
            System.out.println(autores[i].toString());
        }
    }
}
