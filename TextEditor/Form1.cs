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
using NLog;

namespace TextEditor
{
    public partial class mainForm : Form
    {
        private static Logger formLogger = LogManager.GetCurrentClassLogger();
        bool isFileChanged = false;
        public mainForm()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            formLogger.Info("Clicked Open button...");
            var windowResult = openFileDialog1.ShowDialog();
            if (windowResult == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                mainTextBox.Text = File.ReadAllText(fileName.ToString());
                isFileChanged = false;
                formLogger.Info($"Opened file {fileName}");
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            formLogger.Info("Clicked Save button...");
            var windowResult = saveFileDialog1.ShowDialog();
            if (windowResult == DialogResult.OK)
            {
                var fileName = saveFileDialog1.FileName;
                formLogger.Info("Trying to write to file...");
                try
                {
                    TextWriter txt = new StreamWriter(fileName);
                    txt.Write(mainTextBox.Text);
                    txt.Close();
                    formLogger.Info("Wrote to file successfully");
                }
                catch (IOException ex)
                {
                    formLogger.Error(ex, "File is unaccessible.");
                    var errorMessage = MessageBox.Show("File is unaccessible. Please, select other file to write.", "Access denied", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    
                }
                isFileChanged = false;
            }
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            formLogger.Info("Close button clicked...");
            if (!isFileChanged)
            {
                this.Close();
                formLogger.Info("File closed");
            }
            else
            {
                var result = MessageBox.Show("File has not been saved. Exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                    formLogger.Info("File closed");
                }
            }
        }

        private void mainTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileChanged = true;
        }
    }
}
