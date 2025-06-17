import java.util.Scanner;

public class Data {
    private int dia;
    private int mes;
    private int ano;
    private Scanner sc = new Scanner(System.in);

    public Data() {
        entraDia();
        entraMes();
        entraAno();
    }

    public Data(int d, int m, int a) {
        entraDia(d);
        entraMes(m);
        entraAno(a);
    }

    public void entraDia(int d) {
        if (d >= 1 && d <= diasNoMes(mes, ano)) {
            dia = d;
        } else {
            System.err.println("Dia inválido.");
        }
    }

    public void entraMes(int m) {
        if (m >= 1 && m <= 12) {
            mes = m;
        } else {
            System.err.println("Mês inválido.");
        }
    }

    public void entraAno(int a) {
        if (a >= 1) {
            ano = a;
        } else {
            System.err.println("Ano inválido.");
        }
    }

    public void entraDia() {
        int d;
        do {
            System.out.print("Digite o dia: ");
            d = sc.nextInt();
        } while (d < 1 || d > 31);
        dia = d;
    }

    public void entraMes() {
        int m;
        do {
            System.out.print("Digite o mês: ");
            m = sc.nextInt();
        } while (m < 1 || m > 12);
        mes = m;
    }

    public void entraAno() {
        int a;
        do {
            System.out.print("Digite o ano: ");
            a = sc.nextInt();
        } while (a < 1);
        ano = a;
    }

    public int retDia() {
        return dia;
    }

    public int retMes() {
        return mes;
    }

    public int retAno() {
        return ano;
    }

    public String mostra1() {
        return String.format("%02d/%02d/%04d", dia, mes, ano);
    }

    public String mostra2() {
        String[] meses = {"janeiro", "fevereiro", "marco", "abril", "maio", "junho", "julho",
                          "agosto", "setembro", "outubro", "novembro", "dezembro"};
        return String.format("%02d/%s/%04d", dia, meses[mes - 1], ano);
    }

    public boolean bissexto() {
        return (ano % 4 == 0 && ano % 100 != 0) || (ano % 400 == 0);
    }

    public int diasTranscorridos() {
        int total = 0;
        for (int i = 1; i < mes; i++) {
            total += diasNoMes(i, ano);
        }
        total += dia;
        return total;
    }

    public void apresentaDataAtual() {
        System.out.println("Funcionalidade de data atual desabilitada. Use lógica própria ou bibliotecas.");
    }

    private int diasNoMes(int m, int a) {
        switch (m) {
            case 1: case 3: case 5: case 7:
            case 8: case 10: case 12:
                return 31;
            case 4: case 6: case 9: case 11:
                return 30;
            case 2:
                return bissexto() ? 29 : 28;
            default:
                return 0;
        }
    }
}