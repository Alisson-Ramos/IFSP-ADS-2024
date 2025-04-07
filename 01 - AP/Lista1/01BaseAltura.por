programa {
  funcao inicio() {
      // Variáveis
      inteiro base
      inteiro altura
      inteiro resultado

      // Leitura e escrita
      escreva("digite a base:")
      leia(base)

      escreva("digite a altura:")
      leia(altura)

      // Calculo
      resultado = base * altura
      // Resposta
      escreva("você tem uma área de: " + resultado)
      // Calculo
      resultado = 2*(base + altura)
      // Resposta
      escreva("\nvocê tem um perimetro de: " + resultado)
  }
}
