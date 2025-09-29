using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgendaContatos
{
    public partial class Form1 : Form
    {
        private List<Contato> contatos = new List<Contato>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Contato c = new Contato
            {
                Nome = txtNome.Text,
                Telefone = txtTelefone.Text,
                Email = txtEmail.Text
            };
            contatos.Add(c);
            AtualizarLista();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (lstContatos.SelectedItem != null)
            {
                contatos.RemoveAt(lstContatos.SelectedIndex);
                AtualizarLista();
            }
        }

        private void AtualizarLista()
        {
            lstContatos.Items.Clear();
            foreach (var c in contatos)
            {
                lstContatos.Items.Add(c.ToString());
            }
        }
    }
}
