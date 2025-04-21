// 7. Entrar via teclado com doze valores e armazená-los em uma matriz de ordem 3x4. Após a
// digitação dos valores solicitar uma constante multiplicativa, que deverá multiplicar cada
// valor matriz e armazenar o resultado na própria matriz, nas posições correspondentes.
// Alisson Santos
// Davi Coelho

import java.util.Scanner;

public class TP02Ex07 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int[][] matriz = new int[3][4];

        System.out.println("Digite 12 valores para preencher a matriz 3x4:");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                System.out.printf("Valor [%d][%d]: ", i, j);
                matriz[i][j] = scanner.nextInt();
            }
        }

        System.out.print("Digite a constante multiplicativa: ");
        int constante = scanner.nextInt();

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                matriz[i][j] *= constante;
            }
        }

        System.out.println("\nMatriz multiplicada:");
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 4; j++) {
                System.out.print(matriz[i][j] + "\t");
            }
            System.out.println();
        }

        scanner.close();
    }
}
