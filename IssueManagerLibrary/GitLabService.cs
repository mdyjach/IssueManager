using IssueManagerLibrary;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

public class GitLabService : GitService
{
    public GitLabService(HttpClient httpClient, string repositoryUrl, string accessToken) : base(httpClient, repositoryUrl, accessToken) { }

    public static string GetRepositoryUrl(string repo, string project)
    {
        string projectPath = $"{repo}/{project}";
        string encodedProjectPath = Uri.EscapeDataString(projectPath);
        return $"https://gitlab.com/api/v4/projects/{encodedProjectPath}/issues";
    }

    public override async Task AddNewIssue(string title, string description)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _repositoryUrl);
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            request.Headers.Add("Cookie", "_cfuvid=cjLyLoUpBPdRb7YlXmNmFJSLpDNjK_eC.umUPuyP4J8-1708545359502-0.0-604800000");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(title), "title");
            content.Add(new StringContent(description), "description");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Issue added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add new issue. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error occurred while adding new issue: {ex.Message}");
        }
    }


    public override async Task ModifyIssue(string issueId, string newName, string newDescription)
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
                Console.WriteLine($"Issue {issueId} modified successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to modify issue {issueId}. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error occurred while modifying issue: {ex.Message}");
        }
    }

    public override async Task CloseIssue(string issueId)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_repositoryUrl}/{issueId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent("close"), "state_event"); // Set the state_event parameter to "close" to close the issue
            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Issue {issueId} closed successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to close issue {issueId}. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error occurred while closing issue: {ex.Message}");
        }
    }

    public override async Task ExportIssuesToFile(string issueId, string filePath)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_repositoryUrl}/{issueId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var issueJson = await response.Content.ReadAsStringAsync();

                // Write issue JSON data to a file
                await File.WriteAllTextAsync(filePath, issueJson);

                Console.WriteLine($"Issue {issueId} exported to file: {filePath}");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Issue {issueId} not found.");
            }
            else
            {
                Console.WriteLine($"Failed to export issue {issueId}. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error occurred while exporting issue: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error occurred while writing to file: {ex.Message}");
        }
    }


    public override async Task ImportIssuesFromFile(string filePath)
    {
        try
        {
            // Read issue data from the file
            var issueJson = await File.ReadAllTextAsync(filePath);

            // Deserialize JSON data to an object
            var issueData = JsonConvert.DeserializeObject<IssueData>(issueJson);

            // Construct the request body
            var jsonData = new
            {
                title = issueData.Title,
                description = issueData.Description
                // Add other necessary properties from the issueData object
            };

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

            // Send a POST request to create the issue
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_repositoryUrl}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Issue imported successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to import issue. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error occurred while importing issue: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error occurred while reading file: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error occurred while deserializing JSON: {ex.Message}");
        }
    }

}
