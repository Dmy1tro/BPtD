using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using AesEncryptionTool;
using DiffieHellmanTool;
using Microsoft.AspNetCore.SignalR.Client;

namespace Lab_2.Client
{
    public partial class MainForm : Form
    {
        private BigInteger _key;
        private HubConnection _connection;

        public MainForm()
        {
            InitializeComponent();

            UserNameTextBox.Enabled = false;
            MessageTextBox.Enabled = false;
            SendButton.Enabled = false;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Task.Delay(3000);

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/hub")
                .Build();

            _connection.On<int, int, int>("ReceiveKeys", async (q, a, yb) =>
            {
                var xc = DiffieHellmanService.GetX(q);
                var yc = DiffieHellmanService.GetY(a, xc, q);
                _key = DiffieHellmanService.GetKey(yb, xc, q);
                
                await _connection.InvokeAsync("ReceiveKey", (int)yc);

                UserNameTextBox.Enabled = true;
                MessageTextBox.Enabled = true;
                SendButton.Enabled = true;
            });

            _connection.On<string, string>("Chat", (name, message) =>
            {
                ChatTextBox.Font = new Font(ChatTextBox.SelectionFont, FontStyle.Bold);
                ChatTextBox.AppendText($"{name}: {message}");
                ChatTextBox.AppendText("\n");
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("GetKeys");
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameTextBox.Text) || string.IsNullOrEmpty(MessageTextBox.Text))
            {
                return;
            }

            var encryptedMessage = AesUtil.Encrypt(MessageTextBox.Text, ((int)_key).ToString());

            await _connection.InvokeAsync("SendMessage", UserNameTextBox.Text, encryptedMessage);

            MessageTextBox.Text = string.Empty; 
        }
    }
}
