// 2. Crie a estrutura de classes indicada abaixo
// Pessoa
// From default
// # kp: static int
// # nome: String
// # sexo: char
// # idade: int
// + Pessoa()
// + Pessoa(String, char, int)
// + setKp()
// + setNome(String)
// + setSexo(char)
// + setIdade(int)
// + getKp() : int
// + getNome() : String
// + getSexo() : char
// + getIdade() : int

// Classe Pessoa
// • A propriedade “kp” é estática e deve nos informar quantas pessoas distintas
// foram “setadas”, deve funcionar como um contador;
// Para digitar os dados relativos a uma pessoa crie o FormPessoa conforme layout:
// O campo “Numero” não deve estar acessível à digitação;
// Os campos “Nome”, “Sexo” e “Idade” são de preenchimento obrigatório.
// O campo “Sexo” só deve aceitar “M” ou “F”.
// Quando se pressionar o botão “OK” os dados digitados deverão ser transferidos
// ao objeto “UmaPessoa”, uma instância da classe Pessoa.
// Quando se pressionar o botão “Mostrar” descarregar os dados do objeto
// “UmaPessoa” inclusive o contador “kp”.
// Versão 02
// Refaça o exercício anterior, porém agora o sexo não deve ser digitado através de
// um TextField e sim escolhido através de um componente do tipo “JComboBox”.
// Versão 03
// Refaça o exercício anterior, porém agora usando um componente do tipo
// “JRadioButton”.

public class Pessoa {
    static int kp = 0;
    String nome;
    char sexo;
    int idade;

    public Pessoa() {
        kp++;
    }

    public Pessoa(String nome, char sexo, int idade) {
        this.nome = nome;
        this.sexo = sexo;
        this.idade = idade;
        kp++;
    }

    public int getKp() { return kp; }
    public String getNome() { return nome; }
    public char getSexo() { return sexo; }
    public int getIdade() { return idade; }

    public void setNome(String n) { nome = n; }
    public void setSexo(char s) { sexo = s; }
    public void setIdade(int i) { idade = i; }
}
