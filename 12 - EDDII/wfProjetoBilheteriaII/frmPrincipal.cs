using System;
using System.Drawing;
using System.Windows.Forms;

namespace wfProjetoBilheteriaII
{
    public partial class frmPrincipal : Form
    {
        #region Variáveis Globais
        private int _fileiras = 15;
        private int _poltronas = 40;
        private int _faturamento = 0;
        private CheckBox[,]? _chkPoltronas;
        private Label? _lblResultado;
        private int _ocupados = 0;
        #endregion

        public frmPrincipal()
        {
            InitializeComponent();
            InitializeDefaultComponents();
            InitMatrizPoltronas();
        }

        private void InitializeDefaultComponents()
        {
            // ALGUMAS PROPRIEDADES DE COMPONENTES 
            // ============================================== 
            // SIZE : TAMANHO DO COMPONENTE 
            // LOCATION : LOCALIZAÇÃO NO FORMULÁRIO PAI 
            // PARENT : QUEM É O PAI QUE ELE É ATRELADO 
            // TEXT : TEXTO DO COMPONENTE 
            // CURSOR : TIPO DE CURSOR 
            // AUTO SIZE : AJUSTA O TAMANHO AUTOMATICAMENTE 
            // ==============================================

            // Form
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Projeto Bilheteria";
            this.WindowState = FormWindowState.Maximized;
            // Botão Faturamento
            Button btnFaturamento = new Button()
            {
                Text = "Faturamento",
                Size = new Size(150, 30),
                Location = new Point(650, 50),
                Parent = this,
                Cursor = Cursors.Hand
            };
            btnFaturamento.Click += BtnFaturamento_Click;

            // Botão Finalizar
            Button btnFinalizar = new Button()
            {
                Text = "Finalizar",
                Size = new Size(150, 30),
                Location = new Point(650, 100),
                Parent = this,
                Cursor = Cursors.Hand
            };
            btnFinalizar.Click += (s, e) =>
            {
                var result = MessageBox.Show("Deseja sair?",
                                 "Confirmação",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            };

            // Label Resultado
            _lblResultado = new Label()
            {
                Text = "Sistema Bilheteria",
                AutoSize = true,
                Location = new Point(650, 150),
                Parent = this
            };
        }

        private void InitMatrizPoltronas()
        {
            GroupBox gbPoltronas = new GroupBox()
            {
                Parent = this,
                Text = "Mapa de Poltronas",
                Size = new Size(600, 500),
                Location = new Point(20, 20)
            };

            _chkPoltronas = new CheckBox[_fileiras, _poltronas];

            for (int i = 0; i < _fileiras; i++)
            {
                for (int j = 0; j < _poltronas; j++)
                {
                    _chkPoltronas[i, j] = new CheckBox()
                    {
                        Checked = false,
                        Text = "",
                        AutoSize = true,
                        Location = new Point(20 + (j * 14), 30 + (i * 25)),
                        Tag = $"{i + 1}-{j + 1}" // Guarda fileira e poltrona
                    };

                    // Handler de reserva
                    _chkPoltronas[i, j].Click += Poltrona_Click;

                    gbPoltronas.Controls.Add(_chkPoltronas[i, j]);
                }
            }
        }

        /// <summary>
        /// Confirmação da reserva ao clicar na poltrona
        /// </summary>
        private void Poltrona_Click(object? sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender!;
            string[] pos = chk.Tag.ToString()!.Split('-');
            int fileira = int.Parse(pos[0]);
            int poltrona = int.Parse(pos[1]);

            if (chk.Checked)
            {
                // Pergunta se confirma reserva
                var result = MessageBox.Show($"Deseja reservar a poltrona {poltrona} da fileira {fileira}?",
                                             "Confirmação de Reserva",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _ocupados++;
                    if (fileira <= 5) _faturamento += 50;
                    else if (fileira <= 10) _faturamento += 30;
                    else _faturamento += 15;

                    chk.Enabled = false; // Bloqueia a poltrona ocupada
                    chk.BackColor = Color.Red; // Visual: ocupada em vermelho
                }
                else
                {
                    chk.Checked = false; // Cancela seleção
                }
            }
        }

        /// <summary>
        /// Mostra o faturamento e qtde ocupados
        /// </summary>
        private void BtnFaturamento_Click(object? sender, EventArgs e)
        {
            _lblResultado!.Text = $"Qtde ocupados: {_ocupados}\n" +
                                  $"Faturamento: R$ {_faturamento:N2}";
        }
    }
}
