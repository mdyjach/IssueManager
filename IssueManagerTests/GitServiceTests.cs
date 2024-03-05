using IssueManagerLibrary;
using Moq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IssueManagerTests
{
    public class GitServiceTests
    {
        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task AddNewIssue(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            string successMessage = null;
            string failureMessage = null;

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.AddNewIssue("Issue Title", "Issue Description",
                onSuccess: message => successMessage = message,
                onFailure: message => failureMessage = message
            );

            // Assert
            Assert.Null(successMessage);
            Assert.NotNull(failureMessage);
            Assert.Contains("Unauthorized", failureMessage);
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task ModifyIssue(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            string successMessage = null;
            string failureMessage = null;

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.ModifyIssue("123", "New Issue Title", "New Issue Description",
                onSuccess: message => successMessage = message,
                onFailure: message => failureMessage = message
            );

            // Assert
            Assert.NotNull(failureMessage);
            Assert.Contains("Unauthorized", failureMessage);
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task CloseIssue(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            string successMessage = null;
            string failureMessage = null;

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.CloseIssue("123",
                onSuccess: message => successMessage = message,
                onFailure: message => failureMessage = message
            );

            // Assert
            Assert.NotNull(failureMessage);
            Assert.Contains("Unauthorized", failureMessage);
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task ExportIssuesToFile(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"title\":\"Issue Title\",\"body\":\"Issue Description\"}")
            };
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            var filePath = "test.json";
            string successMessage = null;
            string failureMessage = null;

            // Act
            await gitService.ExportIssuesToFile("123", filePath,
                onSuccess: message => successMessage = message,
                onFailure: message => failureMessage = message
            );

            // Assert
            Assert.True(File.Exists(filePath));
            Assert.Contains("Unauthorized", failureMessage);
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task ImportIssuesFromFile(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            var filePath = "test.json";
            File.WriteAllText(filePath, "{\"title\":\"Issue Title\",\"body\":\"Issue Description\"}");
            string successMessage = null;
            string failureMessage = null;

            // Act
            await gitService.ImportIssuesFromFile(filePath,
                onSuccess: message => successMessage = message,
                onFailure: message => failureMessage = message
            );

            // Assert
            Assert.NotNull(failureMessage);
            Assert.Contains("Unauthorized", failureMessage);
        }
    }
}
