// 3. Entrar via teclado com “N” valores quaisquer. O valor “N” (que representa a quantidade de
// números) será digitado, deverá ser positivo, porém menor que vinte. Caso a quantidade não
// satisfaça a restrição, enviar mensagem de erro e solicitar o valor novamente. Após a
// digitação dos “N” valores, exibir:
// a. O maior valor;
// b. O menor valor;
// c. A soma dos valores;
// d. A média aritmética dos valores;
// e. A porcentagem de valores que são positivos;
// f. A porcentagem de valores negativos;
// Após exibir os dados, perguntar ao usuário de deseja ou não uma nova execução do
// programa. Consistir a resposta no sentido de aceitar somente “S” ou “N” e encerrar o
// programa em função dessa resposta.
//Alisson Santos 
//Davi Coelho

import java.util.Scanner;

public class TP02Ex03 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String continuar;

        do {
            int n;

          
            do {
                System.out.print("Digite a quantidade de valores (entre 1 e 19): ");
                n = scanner.nextInt();
                if (n <= 0 || n >= 20) {
                    System.out.println("Erro: o valor deve estar entre 1 e 19.");
                }
            } while (n <= 0 || n >= 20);

            int[] valores = new int[n];
            int soma = 0;
            int maior = -1;
            int menor = Integer.MAX_VALUE;
            int positivos = 0;
            int negativos = 0;

           
            for (int i = 0; i < n; i++) {
                System.out.print("Digite o " + (i + 1) + "º valor: ");
                valores[i] = scanner.nextInt();

                soma += valores[i];
                if (valores[i] > maior) maior = valores[i];
                if (valores[i] < menor) menor = valores[i];

                if (valores[i] > 0) positivos++;
                else if (valores[i] < 0) negativos++;
            }

            double media = (double) soma / n;
            double percPositivos = (double) positivos / n * 100;
            double percNegativos = (double) negativos / n * 100;

            System.out.println("\n--- Resultados ---");
            System.out.println("Maior valor: " + maior);
            System.out.println("Menor valor: " + menor);
            System.out.println("Soma: " + soma);
            System.out.printf("Média: %.2f%n", media);
            System.out.printf("%% Positivos: %.2f%%%n", percPositivos);
            System.out.printf("%% Negativos: %.2f%%%n", percNegativos);

            do {
                System.out.print("\nDeseja executar novamente? (S/N): ");
                continuar = scanner.next().toUpperCase();
                if (!continuar.equals("S") && !continuar.equals("N")) {
                    System.out.println("Erro: responda apenas com S ou N.");
                }
            } while (!continuar.equals("S") && !continuar.equals("N"));

        } while (continuar.equals("S"));

        System.out.println("Programa encerrado.");
        scanner.close();
    }
}
