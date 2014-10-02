using System.Windows.Forms;

namespace InvisibleDiary
{
    public partial class FrmEnterPassword : Form
    {
        public string Password {
            get { return textBox1.Text; }
        }

        public FrmEnterPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
