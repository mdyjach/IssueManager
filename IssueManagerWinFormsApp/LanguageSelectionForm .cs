using System;
using System.Globalization;
using System.Windows.Forms;

namespace IssueManagerWinFormsApp
{
    public partial class LanguageSelectionForm : Form
    {
        public string SelectedLanguage { get; private set; }

        public LanguageSelectionForm()
        {
            InitializeComponent();
            comboBoxLanguages.SelectedIndex = 0; 
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            switch (comboBoxLanguages.SelectedIndex)
            {
                case 0:
                    SelectedLanguage = "en-US"; 
                    break;
                case 1:
                    SelectedLanguage = "pl-PL"; 
                    break;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
