using System.Linq;
using System.Linq.Expressions;

namespace acessos.mvc.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Ambiente> Ambientes { get; set; } 

        public Usuario() { }

        public bool concederAmbiente(Ambiente ambiente)
        {
            try
            {
                if (Ambientes.Contains(ambiente))
                    return false;

                Ambientes.Add(ambiente);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("ocorreu um erro: ", ex) ;
            }
        }

        public bool revogarAmbiente(Ambiente ambiente)
        {
            try
            {
                var ambienteRevogar = Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
                if (ambienteRevogar != null)
                {
                    Ambientes.Remove(ambienteRevogar);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("ocorreu um erro: ", ex);
            }
        }
    }
}
