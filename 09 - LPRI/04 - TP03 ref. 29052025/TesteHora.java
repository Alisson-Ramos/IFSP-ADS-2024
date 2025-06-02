import java.util.Scanner;

public class TesteHora{
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Hora hora = null;

        int opcao;
        do {
            System.out.println("\n===== MENU TESTE CLASSE HORA =====");
            System.out.println("1 - Criar hora com valores digitados");
            System.out.println("2 - Mostrar hora no formato 24h");
            System.out.println("3 - Mostrar hora no formato 12h");
            System.out.println("4 - Mostrar total de segundos");
            System.out.println("5 - Alterar hora");
            System.out.println("6 - Alterar minuto");
            System.out.println("7 - Alterar segundo");
            System.out.println("0 - Sair");
            System.out.print("Escolha uma opção: ");
            opcao = sc.nextInt();

            switch (opcao) {
                case 1:
                    System.out.print("Digite a hora (0-23): ");
                    int h = sc.nextInt();
                    System.out.print("Digite o minuto (0-59): ");
                    int m = sc.nextInt();
                    System.out.print("Digite o segundo (0-59): ");
                    int s = sc.nextInt();
                    hora = new Hora(h, m, s);
                    break;

                case 2:
                    if (hora != null) {
                        System.out.println("Hora (24h): " + hora.getHora1());
                    } else {
                        System.out.println("Hora ainda não criada.");
                    }
                    break;

                case 3:
                    if (hora != null) {
                        System.out.println("Hora (12h): " + hora.getHora2());
                    } else {
                        System.out.println("Hora ainda não criada.");
                    }
                    break;

                case 4:
                    if (hora != null) {
                        System.out.println("Total de segundos: " + hora.getSegundos());
                    } else {
                        System.out.println("Hora ainda não criada.");
                    }
                    break;

                case 5:
                    if (hora != null) {
                        System.out.print("Nova hora (0-23): ");
                        hora.setHor(sc.nextInt());
                    } else {
                        System.out.println("Hora ainda não criada.");
                    }
                    break;

                case 6:
                    if (hora != null) {
                        System.out.print("Novo minuto (0-59): ");
                        hora.setMin(sc.nextInt());
                    } else {
                        System.out.println("Hora ainda não criada.");
                    }
                    break;

                case 7:
                    if (hora != null) {
                        System.out.print("Novo segundo (0-59): ");
                        hora.setSeg(sc.nextInt());
                    } else {
                        System.out.println("Hora ainda não criada.");
                    }
                    break;

                case 0:
                    System.out.println("Encerrando o programa...");
                    break;

                default:
                    System.out.println("Opção inválida! Tente novamente.");
            }
        } while (opcao != 0);

        sc.close();
    }

}