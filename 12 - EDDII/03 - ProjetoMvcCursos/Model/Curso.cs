using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMvcCursos.Model
{
    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        private List<Disciplina> disciplinas = new List<Disciplina>();

        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplinas.Count >= 12) return false;
            if (!disciplinas.Contains(disciplina))
            {
                disciplinas.Add(disciplina);
                return true;
            }
            return false;
        }

        public Disciplina PesquisarDisciplina(int id)
        {
            return disciplinas.FirstOrDefault(d => d.Id == id);
        }

        public bool RemoverDisciplina(Disciplina disciplina)
        {
            if (disciplinas.Contains(disciplina) && !disciplina.GetAlunos().Any())
            {
                disciplinas.Remove(disciplina);
                return true;
            }
            return false;
        }

        public IEnumerable<Disciplina> GetDisciplinas()
        {
            return disciplinas;
        }
    }
}
