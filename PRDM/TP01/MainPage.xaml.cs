namespace TP01
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOkClicked(object sender, EventArgs e)
        {
            string id = IdEntry.Text;
            string pass = PassEntry.Text;

            if (id == "admin" && pass == "senha@dmin")
            {
                await DisplayAlert("Sucesso", "Logou com sucesso", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Login não autorizado", "OK");
            }
        }

        private void OnLimparClicked(object sender, EventArgs e)
        {
            IdEntry.Text = string.Empty;
            PassEntry.Text = string.Empty;
            IdEntry.Focus();
        }

        private async void OnCreditosClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Créditos", "Autores do APP:\nAlisson Santos", "OK");
        }
    }
}
