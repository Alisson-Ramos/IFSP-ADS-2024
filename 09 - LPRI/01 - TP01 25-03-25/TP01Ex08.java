// 8. Sabendo que uma milha marítima equivale a um mil, oitocentos e cinquenta e
// dois metros e que um quilômetro possui mil metros, fazer um programa para
// converter milhas marítimas em quilômetros.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex08 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite a quantidade de milhas marítimas: ");
        double milhas = scanner.nextDouble();
        if (milhas <= 0) {
            while (milhas <= 0) {
                System.out.println("A quantidade de milhas marítimas deve ser maior que 0.");
                System.out.print("Digite a quantidade de milhas marítimas: ");
                milhas = scanner.nextDouble();
            }
        }
        double quilometros = milhas * 1852 / 1000;
        System.out.println(milhas + " milhas marítimas equivalem a " + quilometros + " quilômetros.");
        scanner.close();
    }

}
