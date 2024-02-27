using IssueManagerLibrary;

namespace IssueManagerWinFormsApp.IssueOperation
{
    class ExportIssuesOperation(MainForm mainForm) : IssueOperation(mainForm)
    {

        public override async Task Execute(GitService gitService)
        {
            string issueIdToExport = _mainForm.GetExportIssueId();
            string exportFilePath = _mainForm.GetExportFilePath();

            await gitService.ExportIssuesToFile(issueIdToExport, exportFilePath, 
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
