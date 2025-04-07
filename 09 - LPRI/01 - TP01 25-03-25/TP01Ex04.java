// 4. A partir dos valores da base e altura de um triângulo, calcular e exibir sua área.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex04 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor da base: ");
        double base = scanner.nextDouble();
        if (base <= 0){
            while (base <= 0) {
                System.out.println("O valor da base deve ser maior que 0.");
                System.out.print("Digite o valor da base: ");
                base = scanner.nextDouble();
            }
        }
        System.out.print("Digite o valor da altura: ");
        double altura = scanner.nextDouble();
        if (altura <= 0){
            while (altura <= 0) {
                System.out.println("O valor da altura deve ser maior que 0.");
                System.out.print("Digite o valor da altura: ");
                altura = scanner.nextDouble();
            }
        }
        double area = (base * altura) / 2;
        System.out.println("A área do triângulo é: " + area);
        scanner.close();
    }    
}
