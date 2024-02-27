using IssueManagerLibrary;

namespace IssueManagerWinFormsApp.IssueOperation
{
    class CloseIssueOperation(MainForm mainForm) : IssueOperation(mainForm)
    {
        public override async Task Execute(GitService gitService)
        {
            string issueIdToClose = _mainForm.GetCloseIssueId();

            await gitService.CloseIssue(issueIdToClose,
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