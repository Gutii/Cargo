using Cargo.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cargo.ServiceHost.Controllers;

[Route("Api/[controller]/V1")]
[ApiController]
public class TranslateController : ControllerBase
{
    private YandexApiService _yandexApiService;
    public TranslateController(YandexApiService yandexApiService)
    {
        _yandexApiService = yandexApiService;
    }

    [HttpPost(nameof(Translate))]
    public async Task<string> Translate(string text)
    {
        return await _yandexApiService.Translate(text);
    }
}