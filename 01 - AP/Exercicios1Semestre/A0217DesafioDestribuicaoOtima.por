programa
{
    funcao inicio()
    {

      // Declaração de variaveis
      inteiro valor, notas100, notas50, notas20, notas10, notas5, notas2, notas1

      // Leitura e escrita
      escreva("Digite o valor do saque: R$ ")
      leia(valor)

      // Calculos
      notas100 = valor / 100
      valor = valor % 100
      //escreva(valor)

      notas50 = valor / 50
      valor = valor % 50
      //escreva(valor)

      notas20 = valor / 20
      valor = valor % 20
      //escreva(valor)

      notas10 = valor / 10
      valor = valor % 10
      //escreva(valor)

      notas5 = valor / 5
      valor = valor % 5
      //escreva(valor)

      notas2 = valor / 2
      valor = valor % 2
      //escreva(valor)

      notas1 = valor
      //escreva(notas1)

      se (notas100 > 0) {
          escreva(notas100, " nota(s) de R$100\n")
      }
      se (notas50 > 0) {
          escreva(notas50, " nota(s) de R$50\n")
      }
      se (notas20 > 0) {
          escreva(notas20, " nota(s) de R$20\n")
      }
      se (notas10 > 0) {
          escreva(notas10, " nota(s) de R$10\n")
      }
      se (notas5 > 0) {
          escreva(notas5, " nota(s) de R$5\n")
      }
      se (notas2 > 0) {
          escreva(notas2, " nota(s) de R$2\n")
      }
      se (notas1 > 0) {
          escreva(notas1, " nota(s) de R$1\n")
      }
    }
}
