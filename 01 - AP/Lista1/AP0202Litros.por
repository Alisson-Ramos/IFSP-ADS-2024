programa {
  funcao inicio() {
    // Declaração de Var
    real pagamento
    real ppl
    real litros

    // Leitura e escrita
    escreva("Digite o valor a ser pago: ")
    leia(pagamento)
    escreva("Digite o valor do preço por litro: ")
    leia(ppl)
    // Calculo
    litros = pagamento / ppl

    // Saida
    escreva("você receberá: " + litros + " litros")
  }
}
