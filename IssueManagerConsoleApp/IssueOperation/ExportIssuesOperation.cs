using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class ExportIssuesOperation : IOperation
    {
        public async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue ID to export: ");
            string issueIdToExport = Console.ReadLine();
            Console.Write("Enter file path to export to: ");
            string exportFilePath = Console.ReadLine();
            await gitService.ExportIssuesToFile(issueIdToExport, exportFilePath);
        }
    }
}
