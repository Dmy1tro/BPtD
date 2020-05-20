using System;
using System.Windows.Forms;
using BPTD_Lab1.Models;
using BPTD_Lab1.Utils;

namespace BPTD_Lab1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            EncryptedData result;

            try
            {
                result = Encryptor.EncriptDES(textBoxInput.Text, textBoxKeyWord.Text);
            }
            catch
            {
                MessageBox.Show("Error occured while ecnrypting/decrypting information",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return;
            }

            textBoxInput.Text = result.Text;
            textBoxKeyWord.Text = result.Key;

            textBoxResult.Text = result.EncryptedText;
            textBoxKeyResult.Text = result.EncryptedKey;

            MessageBox.Show($"Average entropy of '1' bit: {result.AverageEntropy}", "Entropy Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            EncryptedData result;

            try
            {
                result = Encryptor.DecryptDES(textBoxInputDecrypt.Text, textBoxKeyWordDecrypt.Text);
            }
            catch
            {
                MessageBox.Show("Error occured while ecnrypting/decrypting information",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return;
            }

            textBoxResult.Text = result.Text;
            textBoxKeyResult.Text = result.Key;
        }

        private void CheckKeyIsWeak_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Encryptor.IsKeyWeak(textBoxInput.Text, textBoxKeyWord.Text);

                MessageBox.Show("Key is weak: " + result, "Check key", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error occured while ecnrypting/decrypting information",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
