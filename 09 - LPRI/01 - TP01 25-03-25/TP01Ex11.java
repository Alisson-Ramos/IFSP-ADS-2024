// 11. A partir do diâmetro de um círculo que será digitado, calcular e exibir sua área.
// Alisson Santos
// Fernando Gomes
import java.util.Scanner;

public class TP01Ex11 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Digite o valor do diâmetro do círculo: ");
        double diametro = scanner.nextDouble();
        if (diametro <= 0){
            while (diametro <= 0) {
                System.out.println("O valor do diâmetro deve ser maior que 0.");
                System.out.print("Digite o valor do diâmetro do círculo: ");
                diametro = scanner.nextDouble();
            }
        }
        double raio = diametro / 2;
        double area = Math.PI * Math.pow(raio, 2);
        System.out.println("A área do círculo é: " + area);
        scanner.close();
    }    
}
