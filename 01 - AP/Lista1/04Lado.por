programa {
  // Libs
  inclua biblioteca Matematica
  funcao inicio() {
    
    // Variáveis
    inteiro lado1
    inteiro lado2
    inteiro lado3
    inteiro resultado

    // Leitura e escrita
    escreva("digite o 1º lado: ")
    leia(lado1)
    escreva("digite o 2º lado: ")
    leia(lado2)
    escreva("digite o 3º lado: ")
    leia(lado3)


    // Calculo
    resultado = lado1 + lado2 + lado3
    // Resposta
    escreva("você tem um perimetro do triângulo de: " + resultado)
  }
}
