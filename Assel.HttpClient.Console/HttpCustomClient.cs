using CSharpFunctionalExtensions;
using System.Text.Json;

namespace Assel.University.Console
{
    public static class HttpCustomClient
    {
        public static readonly string baseAddress = "http://universities.hipolabs.com/search?name=middle";
        public static HttpClient Client = new HttpClient();

        public static async Task<Result<IEnumerable<University>>> GetData()
        {
            using (var client = Client ?? new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(baseAddress);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var result = JsonSerializer.Deserialize<List<University>>(jsonResponse, options).Where((u) => u.Country != null && u.Country == "United Kingdom");

                    return Result.Success(result);
                }
                catch (HttpRequestException rex)
                {
                    return Result.Failure<IEnumerable<University>>(nameof(HttpRequestException) + ": " + rex.Message);
                }
                catch (Exception ex)
                {
                    return Result.Failure<IEnumerable<University>>(nameof(Exception) + ": " + ex.Message);
                }
            }
        }
    }
}
