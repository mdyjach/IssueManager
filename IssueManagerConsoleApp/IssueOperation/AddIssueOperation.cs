using IssueManagerConsoleApp.IssueOperation;
using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class AddIssueOperation : IOperation
    {
        public async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue title: ");
            string title = Console.ReadLine();
            Console.Write("Enter issue description: ");
            string description = Console.ReadLine();
            await gitService.AddNewIssue(title, description);
        }
    }
}