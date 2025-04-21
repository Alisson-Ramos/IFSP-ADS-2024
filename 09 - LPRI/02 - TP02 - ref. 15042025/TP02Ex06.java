// 6. Armazenar seis nomes em uma matriz de ordem 2x3. Apresentar os nomes na tela.
//Alisson Santos
// Davi Coelho

import java.util.Scanner;

public class TP02Ex06 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String[][] matriz = new String[2][3];

        System.out.println("Digite 6 nomes para a matriz 2x3:");

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                System.out.printf("Valor [%s][%s]: ", i, j);
                matriz[i][j] = scanner.next();
            }
        }

        System.out.println("\nMatriz 2x3:");
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                System.out.print(matriz[i][j] + "\t");
            }
            System.out.println();
        }

        scanner.close();
    }
}
