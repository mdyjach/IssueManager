using IssueManagerLibrary;

namespace IssueManagerWinFormsApp.IssueOperation
{
    interface IOperation
    {
        Task Execute(GitService gitService);
    }

    internal abstract class IssueOperation(MainForm mainForm) : IOperation
    {
        protected readonly MainForm _mainForm = mainForm;

        public abstract Task Execute(GitService gitService);
    }
}
