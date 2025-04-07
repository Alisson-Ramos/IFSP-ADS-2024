// 9. Calcular e exibir a tensão de um determinado circuito eletrônico a partir dos
// valores da resistência e corrente elétrica que serão digitados. Utilize a lei de Ohm.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex09 {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor da resistência: ");
        double resistencia = scanner.nextDouble();
        if (resistencia <= 0) {
            while (resistencia <= 0) {
                System.out.println("O valor da resistência deve ser maior que 0.");
                System.out.print("Digite o valor da resistência: ");
                resistencia = scanner.nextDouble();
            }
        }
        System.out.print("Digite o valor da corrente elétrica: ");
        double corrente = scanner.nextDouble();
        if (corrente <= 0) {
            while (corrente <= 0) {
                System.out.println("O valor da corrente elétrica deve ser maior que 0.");
                System.out.print("Digite o valor da corrente elétrica: ");
                corrente = scanner.nextDouble();
            }
        }
        double tensao = resistencia * corrente;
        System.out.println("A tensão do circuito é: " + tensao);
        scanner.close();
    }
}