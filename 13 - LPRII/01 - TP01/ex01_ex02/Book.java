package ex01_ex02;
public class Book {
    private String name;
    private Author authors[];
    private double price;
    private int qty = 0;

    Book(String name, Author[] authors, double price){
        this.name = name;
        this.authors = authors;
        this.price = price;
    }

    Book(String name, Author[] authors, double price, int qty){
        this.name = name;
        this.authors = authors;
        this.price = price;
        this.qty = qty;
    }

    public String getName(){
        return name;
    }

    public Author[] getAuthors(){
        return authors;
    }

    public double getPrice() {
        return price;
    }

    public int getQty() {
        return qty;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public void setQty(int qty) {
        this.qty = qty;
    }

    public String toString(){
        String toReturn = "Book[name=" + getName() + ",authors={";

        for(int i = 0; i < (authors.length - 1); i++){
            toReturn += authors[i].toString() + "]";
        }

        toReturn += "},price=" + getPrice() + ",qty=" + getQty() + "]";

        return toReturn;
    }

    public String getAuthorNames(){
        String toReturn = "";
        
        for(int i = 0; i < (authors.length - 1); i++){
            toReturn += authors[i].getName();
            if(i < (authors.length - 2)){
                toReturn += ", ";
            }
        }

        return toReturn;
    }
}

