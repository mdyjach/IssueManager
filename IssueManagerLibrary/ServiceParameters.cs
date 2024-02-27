using Newtonsoft.Json;

namespace IssueManagerLibrary
{
    public class ServiceParameters
    {
        public required GitServiceProvider GtService { get; set; }
        public required string User { get; set; }
        public required string Repo { get; set; }
        public required string Token { get; set; }

        public static bool ReadParametersFromFile(string filePath, out ServiceParameters parameters)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    parameters = JsonConvert.DeserializeObject<ServiceParameters>(json);

                    return true;
                }
                else
                {
                    parameters = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}