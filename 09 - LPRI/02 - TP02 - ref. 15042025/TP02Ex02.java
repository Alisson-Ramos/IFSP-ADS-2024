import java.util.Scanner;

public class TP02Ex02 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int[] valores = new int[10];
        int soma = 0;
        int maior = 0;

        for (int i = 0; i < 10; i++) {
            int valor;
            do {
                System.out.print("Digite um valor positivo (" + (i + 1) + "/10): ");
                valor = scanner.nextInt();
                if (valor < 0) {
                    System.out.println("Erro: o valor deve ser positivo.");
                }
            } while (valor < 0);

            valores[i] = valor;
            soma += valor;
            if (valor > maior) {
                maior = valor;
            }
        }

        double media = (double) soma / 10;

        System.out.println("\nMaior valor: " + maior);
        System.out.println("Soma dos valores: " + soma);
        System.out.println("Média aritmética: " + media);

        scanner.close();
    }
}
