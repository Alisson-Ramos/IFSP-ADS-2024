// 13. Calcular e exibir a velocidade final (em km/h) de um automóvel, a partir dos
// valores da velocidade inicial (em m/s), da aceleração (m/s2) e do tempo de percurso
// (em s) que serão digitados.
// Alisson Santos
// Fernando Gomes
import java.util.Scanner;
public class TP01Ex13 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor da velocidade inicial (em m/s): ");
        double velocidadeInicial = scanner.nextDouble();
        if (velocidadeInicial < 0) {
            while (velocidadeInicial < 0) {
                System.out.println("O valor da velocidade inicial deve ser maior ou igual a 0.");
                System.out.print("Digite o valor da velocidade inicial (em m/s): ");
                velocidadeInicial = scanner.nextDouble();
            }
        }
        System.out.print("Digite o valor da aceleração (em m/s²): ");
        double aceleracao = scanner.nextDouble();
        if (aceleracao < 0) {
            while (aceleracao < 0) {
                System.out.println("O valor da aceleração deve ser maior ou igual a 0.");
                System.out.print("Digite o valor da aceleração (em m/s²): ");
                aceleracao = scanner.nextDouble();
            }
        }
        System.out.print("Digite o valor do tempo de percurso (em s): ");
        double tempo = scanner.nextDouble();
        if (tempo < 0) {
            while (tempo < 0) {
                System.out.println("O valor do tempo de percurso deve ser maior ou igual a 0.");
                System.out.print("Digite o valor do tempo de percurso (em s): ");
                tempo = scanner.nextDouble();
            }
        }
        double velocidadeFinal = velocidadeInicial + (aceleracao * tempo);
        System.out.println("A velocidade final do automóvel é: " + velocidadeFinal + " km/h");

        scanner.close();
    }
}
