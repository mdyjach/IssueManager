using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    interface IOperation
    {
        Task Execute(GitService gitService);
    }

    abstract class IssueOperation(Action<string> onSuccess, Action<string> onFailure) : IOperation
    {
        protected Action<string> _onSuccess = onSuccess;
        protected Action<string> _onFailure = onFailure;

        public abstract Task Execute(GitService gitService);
    }
}
