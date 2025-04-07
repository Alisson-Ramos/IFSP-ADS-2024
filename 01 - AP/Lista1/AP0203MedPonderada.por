programa {
  funcao inicio() {
    // Declaração de Variáveis
    real nota1
    real nota2
    real nota3
    real mediaPonderada
    
    // Leitura das Notas
    escreva("Digite a primeira nota: ")
    leia(nota1)
    escreva("Digite a segunda nota: ")
    leia(nota2)
    escreva("Digite a terceira nota: ")
    leia(nota3)
    
    // Cálculo da Média Ponderada
    mediaPonderada = (nota1 * 1) + (nota2 * 2) + (nota3 * 3) / 3    
    // Saída: Exibe a Média Ponderada
    escreva("A média ponderada é: " + mediaPonderada)
  }
}