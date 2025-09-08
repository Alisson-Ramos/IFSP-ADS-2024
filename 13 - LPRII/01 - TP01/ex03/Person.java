package ex03;

public class Person {
    private String name;
    private String address;

    public Person(String name, String address){
        this.address = address;
        this.name = name;
    }

    public String getName(){
        return name;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String toString(){
        return "Person[name=" + getName() + ",address=" + getAddress() + "]";
    }
}
