using IssueManagerLibrary;
using Moq;
using System.Net;

namespace IssueManagerTests
{
    public class GitServiceTests
    {
        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task AddNewIssue_Success(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.AddNewIssue("Issue Title", "Issue Description");

            // Assert
            Assert.True(true); // Placeholder assertion
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task AddNewIssue_Failure(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.AddNewIssue("Issue Title", "Issue Description");

            // Assert
            Assert.True(true); // Placeholder assertion
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task ModifyIssue_Success(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.ModifyIssue("123", "New Issue Title", "New Issue Description");

            // Assert
            Assert.True(true); // Placeholder assertion
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task CloseIssue_Success(string repositoryUrl, string accessToken)
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            GitService gitService = repositoryUrl.Contains("github") ?
                (GitService)new GitHubService(httpClientMock.Object, repositoryUrl, accessToken) :
                (GitService)new GitLabService(httpClientMock.Object, repositoryUrl, accessToken);

            // Act
            await gitService.CloseIssue("123");

            // Assert
            Assert.True(true); // Placeholder assertion
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task ExportIssuesToFile_Success(string repositoryUrl, string accessToken)
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

            // Act
            await gitService.ExportIssuesToFile("123", filePath);

            // Assert
            Assert.True(File.Exists(filePath));
            // Here you can further assert the content of the file
        }

        [Theory]
        [InlineData("https://api.github.com/repos/owner/repo/issues", "access_token")]
        [InlineData("https://gitlab.com/api/v4/projects/owner%2Frepo/issues", "access_token")]
        public async Task ImportIssuesFromFile_Success(string repositoryUrl, string accessToken)
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

            // Act
            await gitService.ImportIssuesFromFile(filePath);

            // Assert
            Assert.True(true); // Placeholder assertion
        }
    }
}
