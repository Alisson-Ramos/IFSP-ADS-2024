programa
{
    funcao inicio()
    {
        real nota1_bimestre1, nota2_bimestre1
        real nota1_bimestre2, nota2_bimestre2
        real media_bimestre1, media_bimestre2, nota_semestral

        // Escrita e Leitura
        escreva("Digite a nota da primeira prova do 1º bimestre: ")
        leia(nota1_bimestre1)
        escreva("Digite a nota da segunda prova do 1º bimestre: ")
        leia(nota2_bimestre1)
      
        escreva("Digite a nota da primeira prova do 2º bimestre: ")
        leia(nota1_bimestre2)
        escreva("Digite a nota da segunda prova do 2º bimestre: ")
        leia(nota2_bimestre2)
        
        // Calcular a média de cada bimestre
        media_bimestre1 = (nota1_bimestre1 + nota2_bimestre1) / 2
        media_bimestre2 = (nota1_bimestre2 + nota2_bimestre2) / 2
        
        // Calcular a nota semestral
        nota_semestral = (media_bimestre1 + media_bimestre2) / 2
        
        // Exibir a nota semestral
        escreva("A nota semestral do aluno é: ", nota_semestral)
    }
}
