// 10. Entrar com uma matriz de ordem MxM, onde a ordem também será escolhida pelo usuário,
// sendo que no máximo será de ordem 10 e quadrática. Após a digitação dos elementos,
// calcular e exibir a matriz inversa. Exibir as matrizes na tela, sob a forma matricial (linhas x
// colunas).
// Alisson Santos
// Davi Coelho

import java.util.Scanner;

public class TP02Ex10 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int ordem;

        do {
            System.out.print("Digite a ordem da matriz quadrada (até 10): ");
            ordem = scanner.nextInt();
        } while (ordem < 1 || ordem > 10);

        double[][] matriz = new double[ordem][ordem];
        double[][] inversa = new double[ordem][ordem];


        System.out.println("Digite os elementos da matriz:");
        for (int i = 0; i < ordem; i++) {
            for (int j = 0; j < ordem; j++) {
                System.out.printf("Elemento [%d][%d]: ", i, j);
                matriz[i][j] = scanner.nextDouble();
                inversa[i][j] = (i == j) ? 1.0 : 0.0;
            }
        }

        
        for (int i = 0; i < ordem; i++) {
            double pivo = matriz[i][i];
            if (pivo == 0) {
                System.out.println("Matriz não possui inversa (pivô zero).");
                scanner.close();
                return;
            }

            
            for (int j = 0; j < ordem; j++) {
                matriz[i][j] /= pivo;
                inversa[i][j] /= pivo;
            }

            
            for (int k = 0; k < ordem; k++) {
                if (k != i) {
                    double fator = matriz[k][i];
                    for (int j = 0; j < ordem; j++) {
                        matriz[k][j] -= fator * matriz[i][j];
                        inversa[k][j] -= fator * inversa[i][j];
                    }
                }
            }
        }


        System.out.println("\nMatriz original transformada (identidade):");
        for (int i = 0; i < ordem; i++) {
            for (int j = 0; j < ordem; j++) {
                System.out.printf("%.2f\t", matriz[i][j]);
            }
            System.out.println();
        }


        System.out.println("\nMatriz inversa:");
        for (int i = 0; i < ordem; i++) {
            for (int j = 0; j < ordem; j++) {
                System.out.printf("%.2f\t", inversa[i][j]);
            }
            System.out.println();
        }

        scanner.close();
    }
}
