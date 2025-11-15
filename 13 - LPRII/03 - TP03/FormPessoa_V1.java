import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class FormPessoa_V1 extends JFrame {
    JTextField txtNumero, txtNome, txtSexo, txtIdade;
    Pessoa p;

    public FormPessoa_V1(){
        setLayout(new GridLayout(5,2));
        txtNumero = new JTextField();
        txtNumero.setEditable(false);
        txtNome = new JTextField();
        txtSexo = new JTextField();
        txtIdade = new JTextField();

        add(new JLabel("Número:"));
        add(txtNumero);
        add(new JLabel("Nome:"));
        add(txtNome);
        add(new JLabel("Sexo:"));
        add(txtSexo);
        add(new JLabel("Idade:"));
        add(txtIdade);

        JButton ok = new JButton("OK");
        JButton mostrar = new JButton("Mostrar");

        ok.addActionListener(e -> {
            p = new Pessoa(txtNome.getText(), txtSexo.getText().charAt(0), Integer.parseInt(txtIdade.getText()));
            txtNumero.setText("" + p.getKp());
        });

        mostrar.addActionListener(e -> {
            JOptionPane.showMessageDialog(this,
                "Nome: " + p.getNome() + "\nSexo: " + p.getSexo() + "\nIdade: " + p.getIdade());
        });

        add(ok);
        add(mostrar);

        setSize(300,200);
    }

    public static void main(String[] args){
        new FormPessoa_V1().setVisible(true);
    }
}
