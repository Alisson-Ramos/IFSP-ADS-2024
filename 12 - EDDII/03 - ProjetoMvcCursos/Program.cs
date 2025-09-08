// See https://aka.ms/new-console-template for more information
using ProjetoMvcCursos.Controllers;
using ProjetoMvcCursos.Model;
using ProjetoMvcCursos.View;
using System;
using System.Linq;

namespace ProjetoMvcCursos
{
    class Program
    {
        static void Main(string[] args)
        {
            Escola escola = new Escola();
            bool executando = true;
            while (executando)
            {
                View.View.MostrarMenu();
                int opcao = View.View.LerOpcao();

                switch (opcao)
                {
                    case 0:
                        executando = false;
                        break;

                    case 1: // Adicionar curso
                        int idCurso = View.View.LerInteiro("ID do curso: ");
                        string descCurso = View.View.LerTexto("Descrição do curso: ");
                        var curso = new Curso { Id = idCurso, Descricao = descCurso };
                        View.View.MostrarMensagem(escola.AdicionarCurso(curso)
                            ? "Curso adicionado com sucesso!"
                            : "Não foi possível adicionar o curso.");
                        break;

                    case 2: // Pesquisar curso
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null)
                        {
                            View.View.MostrarMensagem($"Curso: {curso.Descricao}");
                            foreach (var d in curso.GetDisciplinas())
                                View.View.MostrarMensagem($" - Disciplina: {d.Descricao}");
                        }
                        else View.View.MostrarMensagem("Curso não encontrado.");

                        break;

                    case 3: // Remover curso
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null && escola.RemoverCurso(curso))
                            View.View.MostrarMensagem("Curso removido com sucesso!");
                        else
                            View.View.MostrarMensagem(
                                "Não foi possível remover o curso (verifique se há disciplinas associadas).");
                        break;

                    case 4: // Adicionar disciplina
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null)
                        {
                            int idDisc = View.View.LerInteiro("ID da disciplina: ");
                            string descDisc = View.View.LerTexto("Descrição da disciplina: ");
                            var disciplina = new Disciplina { Id = idDisc, Descricao = descDisc };
                            View.View.MostrarMensagem(curso.AdicionarDisciplina(disciplina)
                                ? "Disciplina adicionada!"
                                : "Não foi possível adicionar (limite 12).");
                        }
                        else View.View.MostrarMensagem("Curso não encontrado.");

                        break;

                    case 5: // Pesquisar disciplina
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null)
                        {
                            int idDisc = View.View.LerInteiro("ID da disciplina: ");
                            var disciplina = curso.PesquisarDisciplina(idDisc);
                            if (disciplina != null)
                            {
                                View.View.MostrarMensagem($"Disciplina: {disciplina.Descricao}");
                                foreach (var a in disciplina.GetAlunos())
                                    View.View.MostrarMensagem($" - Aluno: {a.Nome}");
                            }
                            else View.View.MostrarMensagem("Disciplina não encontrada.");
                        }
                        else View.View.MostrarMensagem("Curso não encontrado.");

                        break;

                    case 6: // Remover disciplina
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null)
                        {
                            int idDisc = View.View.LerInteiro("ID da disciplina: ");
                            var disciplina = curso.PesquisarDisciplina(idDisc);
                            if (disciplina != null && curso.RemoverDisciplina(disciplina))
                                View.View.MostrarMensagem("Disciplina removida.");
                            else
                                View.View.MostrarMensagem("Não foi possível remover (verifique se há alunos matriculados).");
                        }
                        else View.View.MostrarMensagem("Curso não encontrado.");

                        break;

                    case 7: // Matricular aluno
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null)
                        {
                            int idDisc = View.View.LerInteiro("ID da disciplina: ");
                            var disciplina = curso.PesquisarDisciplina(idDisc);
                            if (disciplina != null)
                            {
                                int idAluno = View.View.LerInteiro("ID do aluno: ");
                                string nomeAluno = View.View.LerTexto("Nome do aluno: ");
                                var aluno = new Aluno { Id = idAluno, Nome = nomeAluno };
                                View.View.MostrarMensagem(disciplina.MatricularAluno(aluno)
                                    ? "Aluno matriculado!"
                                    : "Não foi possível matricular (limite 15 ou já inscrito).");
                            }
                            else View.View.MostrarMensagem("Disciplina não encontrada.");
                        }
                        else View.View.MostrarMensagem("Curso não encontrado.");

                        break;

                    case 8: // Remover aluno da disciplina
                        idCurso = View.View.LerInteiro("ID do curso: ");
                        curso = escola.PesquisarCurso(idCurso);
                        if (curso != null)
                        {
                            int idDisc = View.View.LerInteiro("ID da disciplina: ");
                            var disciplina = curso.PesquisarDisciplina(idDisc);
                            if (disciplina != null)
                            {
                                int idAluno = View.View.LerInteiro("ID do aluno: ");
                                var aluno = disciplina.GetAlunos().FirstOrDefault(a => a.Id == idAluno);
                                if (aluno != null && disciplina.DesmatricularAluno(aluno))
                                    View.View.MostrarMensagem("Aluno removido da disciplina.");
                                else
                                    View.View.MostrarMensagem("Aluno não encontrado nesta disciplina.");
                            }
                            else View.View.MostrarMensagem("Disciplina não encontrada.");
                        }
                        else View.View.MostrarMensagem("Curso não encontrado.");

                        break;

                    case 9: // Pesquisar aluno
                        string nomeBusca = View.View.LerTexto("Nome do aluno: ");
                        bool encontrado = false;
                        foreach (var c in escola.GetCursos())
                        {
                            foreach (var d in c.GetDisciplinas())
                            {
                                foreach (var a in d.GetAlunos())
                                {
                                    if (a.Nome.Equals(nomeBusca, StringComparison.OrdinalIgnoreCase))
                                    {
                                        View.View.MostrarMensagem(
                                            $"Aluno: {a.Nome} | Disciplina: {d.Descricao} | Curso: {c.Descricao}");
                                        encontrado = true;
                                    }
                                }
                            }
                        }

                        if (!encontrado)
                            View.View.MostrarMensagem("Aluno não encontrado.");
                        break;

                    default:
                        View.View.MostrarMensagem("Opção inválida!");
                        break;
                }
            }
        }
    }
}