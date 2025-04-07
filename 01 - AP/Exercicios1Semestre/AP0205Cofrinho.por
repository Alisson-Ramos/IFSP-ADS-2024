programa {
  funcao inicio() {
    // Declaração de Variáveis
    inteiro qtd1Centavo, qtd5Centavos, qtd10Centavos, qtd25Centavos, qtd50Centavos, qtd1Real
    real totalEconomizado
    
    // Leitura e escrita
    escreva("Digite a quantidade de moedas de 1 centavo: ")
    leia(qtd1Centavo)
    escreva("Digite a quantidade de moedas de 5 centavos: ")
    leia(qtd5Centavos)
    escreva("Digite a quantidade de moedas de 10 centavos: ")
    leia(qtd10Centavos)
    escreva("Digite a quantidade de moedas de 25 centavos: ")
    leia(qtd25Centavos)
    escreva("Digite a quantidade de moedas de 50 centavos: ")
    leia(qtd50Centavos)
    escreva("Digite a quantidade de moedas de 1 real: ")
    leia(qtd1Real)
    
    // Cálculo
    totalEconomizado = qtd1Centavo * 0.01 + qtd5Centavos * 0.05 + qtd10Centavos * 0.10 + qtd25Centavos * 0.25 + qtd50Centavos * 0.50 + qtd1Real * 1.00
    
    // Saída
    escreva("Total economizado: R$ " + totalEconomizado)
  }
}