using System.Net;

namespace zentao.client;

internal class ZentaoHttpClient {
    public HttpClient CreateHttpClient() => new(new HttpClientHandler {
        // Proxy = new WebProxy("http://localhost:8888"),
        CookieContainer = _cookieContainer
    });

    private readonly string _host;
    private readonly CookieContainer _cookieContainer;

    public ZentaoHttpClient(string host) {
        this._host = host;
        _cookieContainer = new CookieContainer();
    }

    public Boolean IsLogin() {
        return _cookieContainer.GetCookies(new Uri(_host)).Any(item => item.Name == "zentaosid");
    }
}