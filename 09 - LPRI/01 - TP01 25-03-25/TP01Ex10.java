// 10. Entrar via teclado com o valor de uma temperatura em graus Celsius, calcular e
// exibir sua temperatura equivalente em Fahrenheit.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;
public class TP01Ex10 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite a temperatura em graus Celsius: ");
        double celsius = scanner.nextDouble();
        double fahrenheit = (celsius * 9 / 5) + 32;
        System.out.println("A temperatura em Fahrenheit é: " + fahrenheit);
        scanner.close();
    }
}
