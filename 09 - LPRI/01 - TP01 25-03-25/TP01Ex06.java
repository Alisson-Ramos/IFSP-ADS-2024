// 6. Calcular e exibir a média aritmética de quatro valores quaisquer que serão digitados.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex06 {
    
    public static void main(String[] args) {
        Scanner scanner = new java.util.Scanner(System.in);
        double soma = 0;
        for (int i = 1; i <= 4; i++) {
            System.out.print("Digite o " + i + "º valor: ");
            double valor = scanner.nextDouble();
            soma += valor;
        }
        double media = soma / 4;
        System.out.println("A média aritmética dos valores é: " + media);
        scanner.close();
    }
}
