programa {
  funcao inicio() {
    // Declaração de Variáveis
    real salarioInicial
    real salarioComAumento
    real salarioFinal
    
    // Leitura e escrita
    escreva("Digite o salário do funcionário: ")
    leia(salarioInicial)
    
    // Cálculo
    salarioComAumento = salarioInicial * 1.15

    salarioFinal = salarioComAumento * 0.92
    
    // Saída
    escreva("Salário inicial: R$ "+ salarioInicial+ "\n")
    escreva("Salário com aumento: R$ "+ salarioComAumento+ "\n")
    escreva("Salário final (após impostos): R$ "+ salarioFinal)
  }
}
