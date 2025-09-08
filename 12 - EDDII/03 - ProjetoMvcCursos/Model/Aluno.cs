using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMvcCursos.Model
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        private List<Disciplina> disciplinasMatriculadas = new List<Disciplina>();

        public bool PodeMatricular(Curso curso)
        {
            return disciplinasMatriculadas.Count < 6 && curso != null;
        }

        public void AdicionarDisciplina(Disciplina disciplina)
        {
            if (!disciplinasMatriculadas.Contains(disciplina))
                disciplinasMatriculadas.Add(disciplina);
        }

        public void RemoverDisciplina(Disciplina disciplina)
        {
            disciplinasMatriculadas.Remove(disciplina);
        }

        public IEnumerable<Disciplina> GetDisciplinas()
        {
            return disciplinasMatriculadas;
        }
    }
}
