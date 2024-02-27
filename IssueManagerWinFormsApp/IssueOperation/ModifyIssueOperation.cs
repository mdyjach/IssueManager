using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using IssueManagerLibrary;

namespace IssueManagerWinFormsApp.IssueOperation
{
    class ModifyIssueOperation(MainForm mainForm) : IssueOperation(mainForm)
    {
        public override async Task Execute(GitService gitService)
        {
            string issueId = _mainForm.GetModifyIssueId();
            string newTitle = _mainForm.GetNewIssueTitle();
            string newDescription = _mainForm.GetNewIssueDescription(); 

            await gitService.ModifyIssue(issueId, newTitle, newDescription,
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
