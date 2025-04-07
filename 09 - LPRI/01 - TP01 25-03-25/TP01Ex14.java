// 14. Calcular e exibir o volume livre de um ambiente que contém uma esfera de raio
// “r” inscrita em um cubo perfeito de aresta “a”. Os valores de “r “ e “a” serão
// digitados.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;
public class TP01Ex14 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor do raio da esfera: ");
        double raio = scanner.nextDouble();
        if (raio <= 0) {
            while (raio <= 0) {
                System.out.println("O valor do raio deve ser maior que 0.");
                System.out.print("Digite o valor do raio da esfera: ");
                raio = scanner.nextDouble();
            }
        }
        System.out.print("Digite o valor da aresta do cubo: ");
        double aresta = scanner.nextDouble();
        if (aresta <= 0) {
            while (aresta <= 0) {
                System.out.println("O valor da aresta deve ser maior que 0.");
                System.out.print("Digite o valor da aresta do cubo: ");
                aresta = scanner.nextDouble();
            }
        }
        double volumeEsfera = (4 * Math.PI * Math.pow(raio, 3)) / 3;
        double volumeCubo = Math.pow(aresta, 3);
        double volumeLivre = volumeCubo - volumeEsfera;
        System.out.println("O volume livre do ambiente é: " + volumeLivre);
        scanner.close();

    }
}
