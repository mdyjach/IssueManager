using IssueManagerLibrary;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IssueManagerLibrary
{
    public class GitLabService : GitService
    {
        public GitLabService(HttpClient httpClient, string repositoryUrl, string accessToken) : base(httpClient, repositoryUrl, accessToken) { }

        public static string GetRepositoryUrl(string repo, string project)
        {
            string projectPath = $"{repo}/{project}";
            string encodedProjectPath = Uri.EscapeDataString(projectPath);
            return $"https://gitlab.com/api/v4/projects/{encodedProjectPath}/issues";
        }

        public override async Task AddNewIssue(string title, string description, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _repositoryUrl);
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(title), "title");
                content.Add(new StringContent(description), "description");
                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke("Issue added successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to add new issue. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error adding new issue: {ex.Message}");
            }
        }

        public override async Task ModifyIssue(string issueId, string newName, string newDescription, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"{_repositoryUrl}/{issueId}");
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(newName), "title");
                content.Add(new StringContent(newDescription), "description");
                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke($"Issue {issueId} modified successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to modify issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error modifying issue: {ex.Message}");
            }
        }

        public override async Task CloseIssue(string issueId, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"{_repositoryUrl}/{issueId}");
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");

                var content = new MultipartFormDataContent();
                content.Add(new StringContent("close"), "state_event");
                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke($"Issue {issueId} closed successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to close issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error closing issue: {ex.Message}");
            }
        }

        public override async Task ExportIssuesToFile(string issueId, string filePath, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_repositoryUrl}/{issueId}");
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var issueJson = await response.Content.ReadAsStringAsync();

                    await File.WriteAllTextAsync(filePath, issueJson);

                    onSuccess?.Invoke($"Issue {issueId} exported to file: {filePath}");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    onFailure?.Invoke($"Issue {issueId} not found.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to export issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error exporting issue: {ex.Message}");
            }
            catch (IOException ex)
            {
                onFailure?.Invoke($"Error writing to file: {ex.Message}");
            }
        }

        public override async Task ImportIssuesFromFile(string filePath, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var issueJson = await File.ReadAllTextAsync(filePath);

                var issueData = JsonConvert.DeserializeObject<IssueData>(issueJson);

                var jsonData = new
                {
                    title = issueData.Title,
                    description = issueData.Description
                };

                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, $"{_repositoryUrl}");
                request.Headers.Add("Authorization", $"Bearer {_accessToken}");

                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke("Issue imported successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to import issue. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error occurred while importing issue: {ex.Message}");
            }
            catch (IOException ex)
            {
                onFailure?.Invoke($"Error occurred while reading file: {ex.Message}");
            }
            catch (JsonException ex)
            {
                onFailure?.Invoke($"Error occurred while deserializing JSON: {ex.Message}");
            }
        }

    }
}