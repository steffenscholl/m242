using BlazorProject.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Web.Http;

namespace BlazorProject.Server.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public List<string> Get([FromUri] string repoOwner, [FromUri] string repoName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://api.github.com/repos/facebook/react/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "M242-IoTKitV3");

            var client = _httpClientFactory.CreateClient();

            var response = client.SendAsync(request);

            if (response.Result.IsSuccessStatusCode)
            {
                using var responseStream = response.Result.Content.ReadAsStreamAsync();

                var res = JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream.Result).Result;
                var s = new branchListsModel() { ListBranches = res};
                var responseLList = new List<string>();

                
                foreach (var item in res)
                {
                    responseLList.Add(item.Name);
                }
                

                return responseLList ;
            }
            return new List<string>() { "FUCK"};
            


            //if (response.Result.IsSuccessStatusCode)
            //{
            //    using var responseStream = response.Result.Content.ReadAsStreamAsync();

            //   return JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream.Result).Result;
            //}
            //else
            //    return new List<GitHubBranch>() { new GitHubBranch() { Name="notfound"} };
        }
    }
}