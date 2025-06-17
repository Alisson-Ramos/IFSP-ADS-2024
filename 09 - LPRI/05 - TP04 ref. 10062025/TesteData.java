import java.util.Scanner;

public class TesteData {
    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        Data data = null;

        int opcao;
        do {
            System.out.println("\n===== MENU TESTE CLASSE DATA =====");
            System.out.println("1 - Criar data com valores digitados");
            System.out.println("2 - Mostrar data formato dd/mm/aaaa");
            System.out.println("3 - Mostrar data formato dd/mesPorExtenso/aaaa");
            System.out.println("4 - Verificar se o ano é bissexto");
            System.out.println("5 - Mostrar dias transcorridos no ano");
            System.out.println("6 - Entrar novo dia");
            System.out.println("7 - Entrar novo mes");
            System.out.println("8 - Entrar novo ano");
            System.out.println("0 - Sair");
            System.out.print("Escolha uma opcao: ");
            opcao = sc.nextInt();

            switch(opcao) {
                case 1:
                    data = new Data();  // chama construtor que lê do teclado
                    break;
                case 2:
                    if(data != null) System.out.println("Data: " + data.mostra1());
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 3:
                    if(data != null) System.out.println("Data: " + data.mostra2());
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 4:
                    if(data != null) System.out.println("Ano bissexto? " + data.bissexto());
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 5:
                    if(data != null) System.out.println("Dias transcorridos: " + data.diasTranscorridos());
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 6:
                    if(data != null) data.entraDia();
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 7:
                    if(data != null) data.entraMes();
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 8:
                    if(data != null) data.entraAno();
                    else System.out.println("Data ainda nao criada.");
                    break;
                case 0:
                    System.out.println("Encerrando programa...");
                    break;
                default:
                    System.out.println("Opcao invalida.");
            }
        } while(opcao != 0);

        sc.close();
    }
}