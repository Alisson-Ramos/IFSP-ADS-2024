import java.util.Scanner;

public class ConsultaAgendada {
    private Data data = new Data();
    private Hora hora = new Hora();
    private String nomePaciente;
    private static int quantidade;
    private String nomeMedico;

    private static final Scanner scanner = new Scanner(System.in);

    public ConsultaAgendada() {
        data.setAno();
        data.setMes();
        data.setDia();

        hora.setHor();
        hora.setMin();
        hora.setSeg();

        setNomePaciente();
        setNomeMedico();

        quantidade++;
    }

    public ConsultaAgendada(int h, int mi, int s, int d, int m, int a, String p, String me) {
        hora.setHor(h);
        hora.setMin(mi);
        hora.setSeg(s);

        data.setAno(a);
        data.setMes(m);
        data.setDia(d);

        setNomePaciente(p);
        setNomeMedico(me);

        quantidade++;
    }

    public ConsultaAgendada(Data d, Hora h, String p, String m) {
        this.data = d;
        this.hora = h;
        this.nomePaciente = p;
        this.nomeMedico = m;
        quantidade++;
    }

    public void setNomePaciente() {
        System.out.print("Por favor, insira o nome do paciente! ");
        String p = scanner.nextLine();
        nomePaciente = p;
    }

    public void setNomeMedico() {
        System.out.print("Por favor, insira o nome do Médico! ");
        String m = scanner.nextLine();
        nomeMedico = m;
    }

    public void setNomePaciente(String p) {
        nomePaciente = p;
    }

    public void setNomeMedico(String m) {
        nomeMedico = m;
    }

    public void setData() {

        data.setAno();
        data.setMes();
        data.setDia();
    }

    public void setHora() {
        hora.setHor();
        hora.setMin();
        hora.setSeg();
    }

    public void setData(int a, int b, int c) {
        data.setAno(a);
        data.setMes(b);
        data.setDia(c);
    }

    public void setHora(int a, int b, int c) {
        hora.setHor(a);
        hora.setMin(b);
        hora.setSeg(c);
    }

    public int getAmostra() {
        return quantidade;
    }

    public String getData() {
        return data.getMostra1();
    }

    public String getHora() {
        return hora.getHora1();
    }

    public String getNomePaciente() {
        return nomePaciente;
    }

    public String getNomeMedico() {
        return nomeMedico;
    }

    public class Main {
    public static void main(String[] args) {
        ConsultaAgendada p1 = new ConsultaAgendada(14, 30, 0, 10, 7, 2025, "João da Silva", "Dra. Camila");

        System.out.println("=== Consulta P1 ===");
        exibirConsulta(p1);

        ConsultaAgendada p2 = new ConsultaAgendada();

        System.out.println("=== Consulta P2 ===");
        exibirConsulta(p2);

        System.out.println("=== Alterando dados de P1 ===");
        p1.setData();
        p1.setHora();
        p1.setNomePaciente();
        p1.setNomeMedico();

        System.out.println("=== Consulta P1 (após alterações) ===");
        exibirConsulta(p1);

        System.out.println("Quantidade total de consultas agendadas: " + p1.getAmostra());
    }

    public static void exibirConsulta(ConsultaAgendada consulta) {
        System.out.println("Data: " + consulta.getData());
        System.out.println("Hora: " + consulta.getHora());
        System.out.println("Paciente: " + consulta.getNomePaciente());
        System.out.println("Médico: " + consulta.getNomeMedico());
        System.out.println();
    }
}


}
