using IssueManagerLibrary;

namespace IssueManagerWinFormsApp.IssueOperation
{
    class AddIssueOperation(MainForm mainForm) : IssueOperation(mainForm)
    {
        public override async Task Execute(GitService gitService)
        {
            string title = _mainForm.GetIssueTitle();
            string description = _mainForm.GetIssueDescription();

            await gitService.AddNewIssue(title, description,
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
