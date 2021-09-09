using System.IO;
using System.Net;

namespace Weather_Monitor
{
    class WeatherApi
    {
        public static string GetRespone(string city)
        {
            string url = "http://api.weatherstack.com/current?access_key=2eb609a9c79c7bdf9bcd4915a28ab788&query="+city+"&hourly=1";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            var streamReader = new StreamReader(httpWebResponse.GetResponseStream());

            return streamReader.ReadToEnd();
        }
    }
}
