using System;
using System.Windows.Forms;

namespace InvisibleDiary
{
    public partial class FrmCreateDiary : Form
    {
        public string Passphrase {
            get { return textBox1.Text; }
        }

        public FrmCreateDiary()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show(Strings.EmptyPassphrase);
                return;
            }

            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show(Strings.PassphraseNotMatch);
                return;
            }

            if (textBox1.Text.Length < 10)
            {
                MessageBox.Show(Strings.PassphraseTooShort);
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
