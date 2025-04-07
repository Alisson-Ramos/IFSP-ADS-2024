// 2. Calcular e exibir a área de um quadrado, a partir do valor de sua aresta que será digitado.
// Alisson Santos
// Fernando Gomes

import java.util.Scanner;

public class TP01Ex02 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor da aresta: ");
        double aresta = scanner.nextDouble();
       
        if (aresta <= 0) {
            while (aresta <= 0) {
                System.out.println("O valor da aresta deve ser maior que 0.");
                System.out.print("Digite o valor da aresta: ");
                aresta = scanner.nextDouble();
            }
        }
        
        double area = aresta * aresta;

        System.out.println("A área do quadrado é: " + area);

        scanner.close();     
    }
}
