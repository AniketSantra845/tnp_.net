using Microsoft.AspNetCore.Http;

namespace Demo.api
{
    public class Webapi
    {
        HttpClient client;
        Uri baseAddress = new Uri("http://127.0.0.1//api/api_modify.php?what=");
        //Uri baseAddress = new Uri("https://tnpdeveloper.000webhostapp.com/api/api_modify.php?what=");
        //Uri baseAddress = new Uri("https://tnp.srlimba.ac.in/srl/api_modify.php?what=");


        public HttpClient response()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseProxy = false };
            client = new HttpClient(handler);
            client.BaseAddress = baseAddress;
            return client;
        }




    }
}
