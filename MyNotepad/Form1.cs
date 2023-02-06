using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace MyNotepad
{
    public partial class Form1 : Form
    {
        bool saved;
        string File;

        public Form1(string file)
        {
            InitializeComponent();
            File = file;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            saved = true;
            if(File != "NOTHING")
            {
                try
                {
                    DocumentName.Text = File;

                    StreamReader streamReader = new StreamReader(File);
                    txt.Text = Encrypt.Decrypt(streamReader.ReadToEnd());
                    streamReader.Close();

                    saved = true;
                }
                catch
                {
                    MessageBox.Show("Could not open the document");
                }
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if(saved == true)
            {
                txt.Text = string.Empty;

                DocumentName.Text = "New Document";
            }
            else
            {
                MessageBox.Show("Not Saved", "You have not saved your work", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "My Notepad Files (*.mynotepad)|*.mynotepad";
                openFileDialog.ShowDialog();
                DocumentName.Text = openFileDialog.FileName;

                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                txt.Text = Encrypt.Decrypt(streamReader.ReadToEnd());
                streamReader.Close();

                saved = true;
            }
            catch
            {
                MessageBox.Show("Did not open a document");
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if(DocumentName.Text == "New Document" || DocumentName.Text == string.Empty)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "My Notepad Files (*.mynotepad)|*.mynotepad";
                    saveFileDialog.ShowDialog();
                    DocumentName.Text = saveFileDialog.FileName;

                    StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                    streamWriter.Write(Encrypt.encrypt(txt.Text));
                    streamWriter.Close();

                    saved = true;
                }
                catch
                {
                    MessageBox.Show("Did not save");
                }
            }
            else
            {
                try
                {
                    StreamWriter streamWriter = new StreamWriter(DocumentName.Text);
                    streamWriter.Write(Encrypt.encrypt(txt.Text));
                    streamWriter.Close();

                    saved = true;
                }
                catch
                {
                    MessageBox.Show("Did not save");
                }
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            saved = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                if (keyData == (Keys.Control | Keys.S))
                {
                    saveToolStripButton.PerformClick();
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txt.SelectedText);

                txt.SelectedText = string.Empty;
            }
            catch
            {
                //do nothing
            }
        }
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txt.SelectedText);
            }
            catch
            {
                //do nothing
            }
        }
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            string xx = Clipboard.GetText();

            txt.Text = txt.Text.Insert(txt.SelectionStart, xx);
        }
    }
}
