using IssueManagerLibrary;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IssueManagerWinFormsApp.IssueOperation
{
    class ImportIssuesOperation(MainForm mainForm) : IssueOperation(mainForm)
    {
        public override async Task Execute(GitService gitService)
        {
            string importFilePath = _mainForm.GetImportFilePath();

            await gitService.ImportIssuesFromFile(importFilePath, 
                onSuccess: message =>
                {
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                },
                onFailure: message =>
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            );

            _mainForm.ClearIssueDetails();
        }
    }
}
