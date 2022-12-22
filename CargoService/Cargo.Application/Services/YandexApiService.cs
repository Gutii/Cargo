using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cargo.Application.Services;

public class YandexApiService
{
    //private string yandexPassportOauthToken = "AQAAAAAeWuy0AATuwVpnfPrdXUhigV5Dh0ybYEk";
    private string? iamToken;
    private DateTime iamTokenDateTime;
    private IOptions<YandexSettings> _options;
    private TelegrammService _telegrammService;

    public YandexApiService(IOptions<YandexSettings> options, TelegrammService telegrammService)
    {
        _options = options;
        _telegrammService = telegrammService;
    }

    public async Task<string> Translate(string text)
    {
        var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(15);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (DateTime.Now - TimeSpan.FromHours(1) > iamTokenDateTime)
            await GetNewIAMToken(httpClient);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", iamToken);
        var content = new StringContent(JsonSerializer.Serialize(new TranslateRequest
        {
            texts = text,
            folderId = _options.Value.FolderId
        }));
        try
        {
            var response = await httpClient.PostAsync("https://translate.api.cloud.yandex.net/translate/v2/translate", content);
            string serialized = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<TranslateResult>(serialized);
            if (res == null || res.translations == null || res.translations.Length == 0)
                return null;
            return res.translations.First().text;
        }
        catch (HttpRequestException httpRequestException)
        {
            await _telegrammService.SendError(httpRequestException);
            throw;
        }
    }

    private async Task GetNewIAMToken(HttpClient httpClient)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { yandexPassportOauthToken = _options.Value.YandexPassportOauthToken }));
        var response = await httpClient.PostAsync("https://iam.api.cloud.yandex.net/iam/v1/tokens", content);
        string serialized = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<iamTokenResult>(serialized);
        if (res != null)
        {
            iamToken = res.iamToken;
            iamTokenDateTime = res.expiresAt;
            return;
        }

        throw new Exception("Неудалось получить IAM токен");
    }

    private class TranslateResult
    {
        public TranslateResultItem[] translations { get; set; }
    }

    private class TranslateResultItem
    {
        public string text { get; set; }
        public string detectedLanguageCode { get; set; }
    }

    private class TranslateRequest
    {
        public string targetLanguageCode { get; set; } = "en";
        public string texts { get; set; }
        public string folderId { get; set; }
    }

    private class iamTokenResult
    {
        public string iamToken { get; set; }
        public DateTime expiresAt { get; set; }
    }
}

public class YandexSettings
{
    public string FolderId { get; set; }
    public string YandexPassportOauthToken { get; set; }
}