// 5. Calcular e exibir o volume de uma esfera a partir do valor de seu diâmetro que será digitado.
// Alisson Santos
// Fernando Gomes
import java.util.Scanner;

public class TP01Ex05 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor do diâmetro da esfera: ");
        double diametro = scanner.nextDouble();
        if (diametro <= 0){
            while (diametro <= 0) {
                System.out.println("O valor do diâmetro deve ser maior que 0.");
                System.out.print("Digite o valor do diâmetro da esfera: ");
                diametro = scanner.nextDouble();
            }
        }
        double raio = diametro / 2;
        double volume = (4 * Math.PI * Math.pow(raio, 3)) / 3;
        System.out.println("O volume da esfera é: " + volume);
        scanner.close();
    }

}
