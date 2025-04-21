// 9. Entrar com uma matriz de ordem MxN, onde a ordem também será escolhida pelo usuário,
// sendo que no máximo 10x10. A matriz não precisa ser quadrática. Após a digitação dos
// elementos, calcular e exibir a matriz transposta.
// Alisson Santos
// Davi Coelho

import java.util.Scanner;

public class TP02Ex09 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        int linhas, colunas;

        do {
            System.out.print("Digite o número de linhas (máximo 10): ");
            linhas = scanner.nextInt();
        } while (linhas <= 0 || linhas > 10);

        do {
            System.out.print("Digite o número de colunas (máximo 10): ");
            colunas = scanner.nextInt();
        } while (colunas <= 0 || colunas > 10);

        int[][] matriz = new int[linhas][colunas];
        int[][] transposta = new int[colunas][linhas];

        System.out.println("\nDigite os valores da matriz:");
        for (int i = 0; i < linhas; i++) {
            for (int j = 0; j < colunas; j++) {
                System.out.printf("Valor [%d][%d]: ", i, j);
                matriz[i][j] = scanner.nextInt();
            }
        }

        for (int i = 0; i < linhas; i++) {
            for (int j = 0; j < colunas; j++) {
                transposta[j][i] = matriz[i][j];
            }
        }

        System.out.println("\nMatriz original:");
        for (int i = 0; i < linhas; i++) {
            for (int j = 0; j < colunas; j++) {
                System.out.print(matriz[i][j] + "\t");
            }
            System.out.println();
        }

        System.out.println("\nMatriz transposta:");
        for (int i = 0; i < colunas; i++) {
            for (int j = 0; j < linhas; j++) {
                System.out.print(transposta[i][j] + "\t");
            }
            System.out.println();
        }

        scanner.close();
    }
}
