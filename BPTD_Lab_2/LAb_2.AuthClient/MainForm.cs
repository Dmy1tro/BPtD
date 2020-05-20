using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using RsaEncryptionTool;

namespace LAb_2.AuthClient
{
    public partial class MainForm : Form
    {
        private string _publicKey;
        private HubConnection _connection;

        public MainForm()
        {
            InitializeComponent();

            SendButton.Enabled = false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Task.Delay(3000);

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/hub")
                .Build();

            _connection.On<string>("PublicKey", publicKey =>
            {
                _publicKey = publicKey;

                SendButton.Enabled = true;
            });

            _connection.On<string>("AuthResult", decrypted =>
            {
                ResultTextBox.Text = decrypted;
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("GetPublicKey");
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Text;

            var encryptedPassword = RsaUtil.Encrypt(password, _publicKey);

            await _connection.InvokeAsync("Auth", login, encryptedPassword);
        }
    }
}
