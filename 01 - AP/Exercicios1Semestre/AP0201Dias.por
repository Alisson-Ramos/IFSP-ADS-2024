programa {
  funcao inicio() {
    // Declaração de Var
    cadeia nome
    inteiro idade
    inteiro dias

    // Leitura e escrita
    escreva("Digite seu nome: ")
    leia(nome)
    escreva("Sua idade: ")
    leia(idade)

    // Calculo
    dias = idade * 365

    // Saida
    escreva(nome + " Você já viveu " + dias + " dias")
  }
}
