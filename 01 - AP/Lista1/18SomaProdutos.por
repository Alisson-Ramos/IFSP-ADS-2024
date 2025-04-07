programa
{
    funcao inicio()
    {
        real produto1, produto2
        real pagamento
		real troco

        escreva("Digite o valor do produto 1: ")
        leia(produto1)
        escreva("Digite o valor do produto 2: ")
        leia(produto2)
		produto1 = produto1 + produto2
        escreva("Digite o valor do produto 3: ")
        leia(produto2)
		produto1 = produto1 + produto2
        escreva("Digite o valor do produto 4: ")
        leia(produto2)
		produto1 = produto1 + produto2
        escreva("Digite o valor do produto 5: ")
        leia(produto2)
		produto1 = produto1 + produto2

        escreva("O total da compra é: ", produto1, "\n")
        escreva("Digite o valor do pagamento: ")
        leia(pagamento)

        se (pagamento < produto1) {
            escreva("O valor pago é insuficiente.")
        } senao {
            troco = pagamento - produto1
            escreva("O troco é: ", troco)
        }
    }
}
