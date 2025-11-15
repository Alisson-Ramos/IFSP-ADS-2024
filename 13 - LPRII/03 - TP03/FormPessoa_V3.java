import javax.swing.*;
import java.awt.*;

public class FormPessoa_V3 extends JFrame {
    JTextField txtNumero, txtNome, txtIdade;
    JRadioButton m, f;
    ButtonGroup sexoGroup;
    Pessoa p;

    public FormPessoa_V3(){
        setLayout(new GridLayout(6,2));
        txtNumero = new JTextField();
        txtNumero.setEditable(false);
        txtNome = new JTextField();
        txtIdade = new JTextField();

        m = new JRadioButton("M");
        f = new JRadioButton("F");
        sexoGroup = new ButtonGroup();
        sexoGroup.add(m);
        sexoGroup.add(f);

        add(new JLabel("Número:"));
        add(txtNumero);
        add(new JLabel("Nome:"));
        add(txtNome);
        add(new JLabel("Sexo:"));
        JPanel sp = new JPanel();
        sp.add(m);
        sp.add(f);
        add(sp);
        add(new JLabel("Idade:"));
        add(txtIdade);

        JButton ok = new JButton("OK");
        JButton mostrar = new JButton("Mostrar");

        ok.addActionListener(e -> {
            char sx = m.isSelected() ? 'M' : 'F';
            p = new Pessoa(txtNome.getText(), sx, Integer.parseInt(txtIdade.getText()));
            txtNumero.setText("" + p.getKp());
        });

        mostrar.addActionListener(e -> {
            JOptionPane.showMessageDialog(this,
                "Nome: " + p.getNome() + "\nSexo: " + p.getSexo() + "\nIdade: " + p.getIdade());
        });

        add(ok);
        add(mostrar);

        setSize(300,250);
    }

    public static void main(String[] args){
        new FormPessoa_V3().setVisible(true);
    }
}
