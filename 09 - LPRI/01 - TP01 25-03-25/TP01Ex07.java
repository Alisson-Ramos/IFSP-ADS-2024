// 7. Calcular e exibir a média geométrica de dois valores quaisquer que serão digitados.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex07 {
    
    public static void main(String[] args) {
        Scanner scanner = new java.util.Scanner(System.in);
        System.out.print("Digite o 1º valor: ");
        double valor1 = scanner.nextDouble();
        System.out.print("Digite o 2º valor: ");
        double valor2 = scanner.nextDouble();
        double media = Math.sqrt(valor1 * valor2);
        System.out.println("A média geométrica dos valores é: " + media);
        scanner.close();
    }

}
