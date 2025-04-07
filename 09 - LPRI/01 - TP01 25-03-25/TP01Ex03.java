// 3. Calcular e exibir a área de um quadrado a partir do valor de sua diagonal que será digitado.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex03 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Digite o valor da diagonal: ");
        double diagonal = scanner.nextDouble();
        if (diagonal <= 0){
            while (diagonal <= 0) {
                System.out.println("O valor da diagonal deve ser maior que 0.");
                System.out.print("Digite o valor da diagonal: ");
                diagonal = scanner.nextDouble();
            }
        }

        double area = (diagonal * diagonal) / 2;

        System.out.println("A área do quadrado é: " + area);

        scanner.close();
    }
}