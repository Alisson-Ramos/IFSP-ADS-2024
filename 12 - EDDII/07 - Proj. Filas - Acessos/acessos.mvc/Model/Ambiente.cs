namespace acessos.mvc.Model
{
    public class Ambiente
    {
        public required int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Queue<Log> Logs { get; set; }

        public void RegistrarLog(Log log)
        {
            try
            {
                if (Logs.Count >= 100)
                    Logs.Dequeue();

                Logs.Enqueue(log);
            }
            catch (Exception ex)
            {
                throw new Exception("ocorreu um erro: ", ex);
            }
        }
    }
}
