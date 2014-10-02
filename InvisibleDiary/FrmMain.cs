using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using InvisibleDiary.Properties;

namespace InvisibleDiary
{
    public partial class FrmMain : Form
    {
        private Diary _diary = new Diary();

        private string _filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "diary.ivd");

        public FrmMain()
        {
            InitializeComponent();
            this.Icon = Resources.diary;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = true;
            dateLabel.Text = string.Format(Strings.TodayIs, DateTime.Today.ToLongDateString());

            try
            {
                if (File.Exists(_filePath))
                    _diary.OpenDiary(_filePath);

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }


            if (!_diary.IsOpen)
            {
                using (var frmCreate = new FrmCreateDiary())
                {
                    if (frmCreate.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            _diary.CreateNew(_filePath, frmCreate.Passphrase);
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show(x.Message, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }

        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            if (mainTextBox.ReadOnly)
            {
                tagsTextBox.Text = "";
                mainTextBox.Text = "";
                dateLabel.Text = string.Format(Strings.TodayIs, DateTime.Today.ToLongDateString());
                writeButton.Text = Strings.WriteDown;
                mainTextBox.ReadOnly = false;
                tagsTextBox.ReadOnly = false;
                return;
            }

            if (string.IsNullOrEmpty(mainTextBox.Text))
            {
                MessageBox.Show(Strings.WriteSomething);
                return;
            }

            var diaryRecord = new DiaryRecord();
            diaryRecord.Created = DateTime.Now;
            diaryRecord.Content = mainTextBox.Text;
            diaryRecord.Tags = tagsTextBox.Text;

            try
            {
                _diary.AddRecord(diaryRecord);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!_diary.IsLocked)
                AddToTree(diaryRecord);

            tagsTextBox.Text = "";
            mainTextBox.Text = "";

        }

        private async void unlockButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmEnterPassword = new FrmEnterPassword())
                {
                    if (frmEnterPassword.ShowDialog() == DialogResult.OK)
                    {
                        _diary.Unlock(frmEnterPassword.Password);
                        splitContainer.Panel2Collapsed = false;

                        _diary.RewindBack();
                        var records = new List<DiaryRecord>();

                        while (_diary.CanReadMoreRecords)
                        {
                            var record = await Task.Run(() => _diary.ReadPrevious());
                            AddToTree(record);
                            records.Add(record);
                        }

                        calendar.BoldedDates = records.Select(r => r.Created).ToArray();
                        unlockButton.Hide();
                    }
                }
            }

            catch (Exception x)
            {
                if (x is CryptographicException)
                {
                    MessageBox.Show(Strings.InvalidPassphrase);
                }
                else
                {
                    MessageBox.Show(x.Message, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private void AddToTree(DiaryRecord record)
        {
            var folder = record.Created.Date.ToString("yyyy-MM");
            var node = entriesTreeView.Nodes[folder];
            if (node == null)
            {
                node = entriesTreeView.Nodes.Add(folder, folder);
            }

            var entryNode = node.Nodes.Add(record.Created.ToString());
            entryNode.Tag = record;
        }

        private void entriesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var record = e.Node.Tag as DiaryRecord;
            if (record != null)
            {
                if (!mainTextBox.ReadOnly && !string.IsNullOrEmpty(mainTextBox.Text))
                {
                    if (MessageBox.Show(Strings.EntryNotSaved, Strings.Confirm, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                mainTextBox.ReadOnly = true;
                mainTextBox.Text = record.Content;
                dateLabel.Text = string.Format(Strings.EntryFrom, record.Created);
                tagsTextBox.Text = record.Tags;
                tagsTextBox.ReadOnly = true;
                writeButton.Text = Strings.AddNew;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mainTextBox.ReadOnly && !string.IsNullOrEmpty(mainTextBox.Text))
            {
                if (MessageBox.Show(Strings.EntryNotSaved, Strings.Confirm, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        
    }
}
