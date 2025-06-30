import java.util.Scanner;
import java.util.Date;
import java.util.Locale;
import java.text.DateFormat;

public class Data {
    private int dia;
    private int mes;
    private int ano;
    private static final Scanner sc = new Scanner(System.in);

    public Data() {
        setDia();
        setMes();
        setAno();
    }

    public Data(int d, int m, int a) {
        setMes(m);
        setDia(d);
        setAno(a);
    }

    public void setDia(int d) {
        if (d >= 1 && d <= getDiasNoMes(mes, ano)) {
            dia = d;
        } else {
            System.err.println("Dia inválido.");
        }
    }

    public void setMes(int m) {
        if (m >= 1 && m <= 12) {
            mes = m;
        } else {
            System.err.println("Mês inválido.");
        }
    }

    public void setAno(int a) {
        if (a >= 1) {
            ano = a;
        } else {
            System.err.println("Ano inválido.");
        }
    }

    public void setDia() {
        int d;
        do {
            System.out.print("Digite o dia: ");
            d = sc.nextInt();
        } while (d < 1 || d > 31);
        dia = d;
    }

    public void setMes() {
        int m;
        do {
            System.out.print("Digite o mês: ");
            m = sc.nextInt();
        } while (m < 1 || m > 12);
        mes = m;
    }

    public void setAno() {
        int a;
        do {
            System.out.print("Digite o ano: ");
            a = sc.nextInt();
        } while (a < 1);
        ano = a;
    }

    public int getDia() {
        return dia;
    }

    public int getMes() {
        return mes;
    }

    public int getAno() {
        return ano;
    }

    public String getMostra1() {
        return String.format("%02d/%02d/%04d", dia, mes, ano);
    }

    public String getMostra2() {
        String[] meses = {"janeiro", "fevereiro", "marco", "abril", "maio", "junho", "julho",
                          "agosto", "setembro", "outubro", "novembro", "dezembro"};
        return String.format("%02d/%s/%04d", dia, meses[mes - 1], ano);
    }

    public boolean getBissexto() {
        return (ano % 4 == 0 && ano % 100 != 0) || (ano % 400 == 0);
    }

    public int getDiasTranscorridos() {
        int total = 0;
        for (int i = 1; i < mes; i++) {
            total += getDiasNoMes(i, ano);
        }
        total += dia;
        return total;
    }

    public void getApresentaDataAtual() {
        Date hoje = new Date();
        DateFormat df = DateFormat.getDateInstance(DateFormat.FULL, new Locale("pt", "BR"));
        System.out.println(df.format(hoje));
    }

    private int getDiasNoMes(int m, int a) {
        switch (m) {
            case 1: case 3: case 5: case 7:
            case 8: case 10: case 12:
                return 31;
            case 4: case 6: case 9: case 11:
                return 30;
            case 2:
                return getBissexto() ? 29 : 28;
            default:
                return 0;
        }
    }
}