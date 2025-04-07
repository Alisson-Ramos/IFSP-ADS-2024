//17. Entrar via teclado com dois valores quaisquer “X” e “Y”. Calcular e exibir o
//cálculo XY (“X” elevado a “Y”). Pesquisar as funções Exp e Ln.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex17 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Digite o valor de X: ");
        double x = scanner.nextDouble();
        System.out.println("Digite o valor de Y: ");    
        double y = scanner.nextDouble();
        System.out.println("X^Y: " + Math.pow(x, y));
        System.out.println("Exp(X): " + Math.exp(x));
        System.out.println("Ln(X): " + Math.log(x));
        scanner.close();
    }
}
