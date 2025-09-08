using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMvcCursos.Model
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        private List<Aluno> alunos = new List<Aluno>();

        public bool MatricularAluno(Aluno aluno)
        {
            if (alunos.Count >= 15) return false;
            if (!alunos.Contains(aluno) && aluno.PodeMatricular(null))
            {
                alunos.Add(aluno);
                aluno.AdicionarDisciplina(this);
                return true;
            }
            return false;
        }

        public bool DesmatricularAluno(Aluno aluno)
        {
            if (alunos.Contains(aluno))
            {
                alunos.Remove(aluno);
                aluno.RemoverDisciplina(this);
                return true;
            }
            return false;
        }

        public IEnumerable<Aluno> GetAlunos()
        {
            return alunos;
        }
    }
}
