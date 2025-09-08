using ProjetoMvcCursos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMvcCursos.Controllers
{
    public class Escola
    {
        private List<Curso> cursos = new List<Curso>();

        public bool AdicionarCurso(Curso curso)
        {
            if (cursos.Count >= 5) return false;
            if (!cursos.Contains(curso))
            {
                cursos.Add(curso);
                return true;
            }
            return false;
        }

        public Curso PesquisarCurso(int id)
        {
            return cursos.FirstOrDefault(c => c.Id == id);
        }

        public bool RemoverCurso(Curso curso)
        {
            if (cursos.Contains(curso) && !curso.GetDisciplinas().Any())
            {
                cursos.Remove(curso);
                return true;
            }
            return false;
        }

        public IEnumerable<Curso> GetCursos()
        {
            return cursos;
        }
    }
}
