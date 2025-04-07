//12. Calcular e exibir o volume de um cone a partir dos valores da altura e do raio da
//base que serão digitados.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;
public class TP01Ex12 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor da altura do cone: ");
        double altura = scanner.nextDouble();
        if (altura <= 0) {
            while (altura <= 0) {
                System.out.println("O valor da altura deve ser maior que 0.");
                System.out.print("Digite o valor da altura do cone: ");
                altura = scanner.nextDouble();
            }
        }
        System.out.print("Digite o valor do raio da base do cone: ");
        double raio = scanner.nextDouble();
        if (raio <= 0) {
            while (raio <= 0) {
                System.out.println("O valor do raio da base deve ser maior que 0.");
                System.out.print("Digite o valor do raio da base do cone: ");
                raio = scanner.nextDouble();
            }
        }
        double volume = (Math.PI * Math.pow(raio, 2) * altura) / 3;
        System.out.println("O volume do cone é: " + volume);
        scanner.close();
    }
}
