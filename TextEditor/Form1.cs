using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class mainForm : Form
    {
        bool isFileChanged = false;
        public mainForm()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            var windowResult = openFileDialog1.ShowDialog();
            if (windowResult == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                mainTextBox.Text = File.ReadAllText(fileName.ToString());
                isFileChanged = false;
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            var windowResult = saveFileDialog1.ShowDialog();

            if (windowResult == DialogResult.OK)
            {
                var fileName = saveFileDialog1.FileName;
                TextWriter txt = new StreamWriter(fileName);
                txt.Write(mainTextBox.Text);
                txt.Close();
                isFileChanged = false;
            }
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (!isFileChanged)
            {
                this.Close();
            }
            var result = MessageBox.Show("File has not been saved. Exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void mainTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileChanged = true;
        }
    }
}
