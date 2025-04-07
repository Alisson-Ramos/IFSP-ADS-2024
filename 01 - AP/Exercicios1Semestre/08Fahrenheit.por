programa {
  inclua biblioteca Matematica
  funcao inicio() {
    // Variáveis
    real fahrenheit
    real celsius

    // Leitura e escrita
    escreva("digite o a temperatura em F: ")
    leia(fahrenheit)
    // Calculo
    celsius = 0.55 * (fahrenheit - 32)
    // Resposta
    escreva("a sua temperatura em celsius é: " + Matematica.arredondar(celsius,2) + " C°")
  }
}