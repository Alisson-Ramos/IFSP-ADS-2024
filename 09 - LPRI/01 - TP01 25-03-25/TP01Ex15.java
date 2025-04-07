// 15. Entrar via teclado com o valor da cotação do dólar e uma certa quantidade de
// dólares. Calcular e exibir o valor correspondente em Reais (R$).
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex15 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite a cotação do dólar: ");
        double cotacao = scanner.nextDouble();
        if (cotacao <= 0) {
            while (cotacao <= 0) {
                System.out.println("A cotação do dólar deve ser maior que 0.");
                System.out.print("Digite a cotação do dólar: ");
                cotacao = scanner.nextDouble();
            }
        }
        System.out.print("Digite a quantidade de dólares: ");
        double dolares = scanner.nextDouble();
        if (dolares <= 0) {
            while (dolares <= 0) {
                System.out.println("A cotação do dólar deve ser maior que 0.");
                System.out.print("Digite a cotação do dólar: ");
                dolares = scanner.nextDouble();
            }
        }
        double reais = cotacao * dolares;
        System.out.println("O valor correspondente em Reais é: R$" + reais);
        scanner.close();
    }    
}
