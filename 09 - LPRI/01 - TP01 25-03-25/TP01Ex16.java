//16. Entrar via teclado com o valor de um ângulo em graus, calcular e exibir as
//seguintes funções trigonométricas: seno, cosseno, tangente e secante deste ângulo.
//Lembre-se que uma função trigonométrica trabalha em radianos.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex16 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Digite o valor do ângulo em graus: ");
        double angulo = scanner.nextDouble();
        double radiano = Math.toRadians(angulo);
        System.out.println("Seno: " + Math.sin(radiano));
        System.out.println("Cosseno: " + Math.cos(radiano));                      
        System.out.println("Tangente: " + Math.tan(radiano));
        System.out.println("Secante: " + 1/Math.cos(radiano));
        scanner.close();  

    }
}
