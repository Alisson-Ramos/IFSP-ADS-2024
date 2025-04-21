//5. Armazenar seis valores em uma matriz de ordem 3x2. Apresentar os valores na tela.
//Alisson Santos
// Davi Coelho

import java.util.Scanner;

public class TP02Ex05 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int[][] matriz = new int[3][2];

        System.out.println("Digite 6 valores para a matriz 3x2:");

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 2; j++) {
                System.out.printf("Valor [%d][%d]: ", i, j);
                matriz[i][j] = scanner.nextInt();
            }
        }

        System.out.println("\nMatriz 2x3:");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 2; j++) {
                System.out.print(matriz[i][j] + "\t");
            }
            System.out.println();
        }

        scanner.close();
    }
}
