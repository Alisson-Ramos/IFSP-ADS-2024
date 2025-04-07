programa
{
	funcao inicio ()
	{
		    
        real cotacao_dolar
		real quantidade_dolares
		real valor_em_reais
    
	    // Entrada de dados
	    escreva("Digite a cotação do dólar: ")
	    leia(cotacao_dolar)
	    
	    escreva("Digite a quantidade de dólares: ")
	    leia(quantidade_dolares)
	    
	    // Cálculo
	    valor_em_reais = cotacao_dolar * quantidade_dolares
	    
	    // Saída de dados
	    escreva("O valor correspondente em Reais é: R$" + valor_em_reais)
		
	}
}