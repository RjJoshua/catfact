using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public CatFactResponse? DataResult { get; set; }

    public async Task<IActionResult> OnGet()
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync("https://catfact.ninja/facts");

        if (response.IsSuccessStatusCode)
        {
            string? content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
            {
                DataResult = JsonConvert.DeserializeObject<CatFactResponse>(content);
            }
        }
        return Page();
    }
}

public class CatFactResponse
{
    public int CurrentPage { get; set; }
    public List<CatFact> Data { get; set; }
    // Other properties...
}

public class CatFact
{
    public string Fact { get; set; }
    public int Length { get; set; }
}