programa {
  // Libs
  inclua biblioteca Matematica
  funcao inicio() {
    
    // Variáveis
    real raio
    real resultado

    // Leitura e escrita
    escreva("digite o raio da circunferência:")
    leia(raio)

    // Calculo
    resultado = Matematica.arredondar(Matematica.PI,2) * Matematica.potencia(raio, 2) 
    // Resposta
    escreva("você tem uma área de: " + Matematica.arredondar(resultado, 2))
    // Calculo
    resultado = (2 * Matematica.arredondar(Matematica.PI,2)) * raio
    // Resposta
    escreva("\nvocê tem um perimetro de: " + Matematica.arredondar(resultado, 2))
  }
}
