namespace acessos.mvc.Model
{
    public class Log
    {
        public DateTime DataAcesso { get; set; }
        public Usuario Usuario { get; set; }
        public bool TipoAcesso { get; set; } // (true=Autorizado, false=Negado)
    }
}
