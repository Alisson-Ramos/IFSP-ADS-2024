programa
{
    funcao inicio()
    {
        real velocidade_m_s
		real velocidade_km_h
        
        escreva("Digite a velocidade em m/s: ")
        leia(velocidade_m_s)
        
        velocidade_km_h = velocidade_m_s * 3.6
        
        escreva("A velocidade em km/h é: ", velocidade_km_h)
    }
}
