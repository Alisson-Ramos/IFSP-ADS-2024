package ex01_ex02;


public class Testeauthor {
    public static void main(String[] args) {
        Author autor = new Author("Wellington Tuler", "tulermoraes@yahoo.com",'m');

        System.out.println(autor.toString());
        autor.setEmail("tulerzinho@gmail.com");


        System.out.println("DADOS AUTHOR:");
        System.out.println(autor.getName());
        System.out.println(autor.getGender());
        System.out.println(autor.getEmail());
        System.out.println(autor.toString());
    }
}
