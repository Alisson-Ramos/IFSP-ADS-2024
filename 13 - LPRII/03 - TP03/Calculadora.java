import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
// Construir o Form abaixo e possibilitar o cálculo das operações de
// divisão, multiplicação, subtração e adição.
    // OBS:
    // • O botão C = Clear e limpa a memória da calculadora e também zera o text field
    // de resultado.
    // • Colocar tratamento de erros (try, catch, finally)
public class Calculadora extends JFrame {
    JTextField txt;
    double num1, num2;
    String op;

    public Calculadora() {
        setLayout(new BorderLayout());
        txt = new JTextField();
        add(txt, BorderLayout.NORTH);
        JPanel p = new JPanel(new GridLayout(5,4));
        String[] b = {"7","8","9","/","4","5","6","*","1","2","3","-","0","C","=","+"};
        for(String s : b){
            JButton btn = new JButton(s);
            btn.addActionListener(e -> click(s));
            p.add(btn);
        }
        add(p);
        setSize(300,300);
    }

    void click(String s){
        if(s.equals("C")){
            txt.setText("");
            num1 = num2 = 0;
            op = null;
        } else if(s.equals("=")){
            num2 = Double.parseDouble(txt.getText());
            double r = 0;
            switch(op){
                case "+": r = num1 + num2; break;
                case "-": r = num1 - num2; break;
                case "*": r = num1 * num2; break;
                case "/": r = num1 / num2; break;
            }
            txt.setText("" + r);
        } else if("+-*/".contains(s)){
            num1 = Double.parseDouble(txt.getText());
            op = s;
            txt.setText("");
        } else {
            txt.setText(txt.getText() + s);
        }
    }

    public static void main(String[] args){
        new Calculadora().setVisible(true);
    }
}
