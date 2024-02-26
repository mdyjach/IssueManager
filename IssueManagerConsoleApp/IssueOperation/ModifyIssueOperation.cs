using IssueManagerConsoleApp.IssueOperation;
using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class ModifyIssueOperation : IOperation
    {
        public async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Enter new description: ");
            string newDescription = Console.ReadLine();
            await gitService.ModifyIssue(id, newTitle, newDescription);
        }
    }
}
