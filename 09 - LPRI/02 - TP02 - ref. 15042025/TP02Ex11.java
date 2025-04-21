// 11. Entrar com uma matriz de ordem MxM, onde a ordem também será escolhida pelo usuário,
// sendo que no máximo será de ordem 10 e quadrática. Após a digitação dos elementos,
// calcular e exibir determinante da matriz.
// Alisson Santos
// Davi Coelho
import java.util.Scanner;

public class TP02Ex11 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int ordem;

        do {
            System.out.print("Digite a ordem da matriz quadrada (até 10): ");
            ordem = scanner.nextInt();
        } while (ordem < 1 || ordem > 10);

        double[][] matriz = new double[ordem][ordem];

        System.out.println("Digite os elementos da matriz:");
        for (int i = 0; i < ordem; i++) {
            for (int j = 0; j < ordem; j++) {
                System.out.printf("Elemento [%d][%d]: ", i, j);
                matriz[i][j] = scanner.nextDouble();
            }
        }

        System.out.println("\nMatriz digitada:");
        for (int i = 0; i < ordem; i++) {
            for (int j = 0; j < ordem; j++) {
                System.out.printf("%.2f\t", matriz[i][j]);
            }
            System.out.println();
        }

        double determinante = calcularDeterminante(matriz, ordem);
        System.out.printf("\nDeterminante da matriz: %.2f\n", determinante);

        scanner.close();
    }

    
    public static double calcularDeterminante(double[][] matriz, int ordem) {
        if (ordem == 1) {
            return matriz[0][0];
        }

        if (ordem == 2) {
            return matriz[0][0] * matriz[1][1] - matriz[0][1] * matriz[1][0];
        }

        double det = 0;
        for (int k = 0; k < ordem; k++) {
            double[][] submatriz = new double[ordem - 1][ordem - 1];

            for (int i = 1; i < ordem; i++) {
                int colunaSub = 0;
                for (int j = 0; j < ordem; j++) {
                    if (j == k) continue;
                    submatriz[i - 1][colunaSub] = matriz[i][j];
                    colunaSub++;
                }
            }

            det += Math.pow(-1, k) * matriz[0][k] * calcularDeterminante(submatriz, ordem - 1);
        }

        return det;
    }
}
