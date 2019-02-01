using System;
using System.Linq;
using System.Net.Http;
using HlsParser.Parser;

namespace HlsParser.console
{
  class Program
  {
    static void Main(string[] args)
    {
      const string url = "https://bitdash-a.akamaihd.net/content/sintel/hls/playlist.m3u8";

      System.Console.WriteLine("Retrieving hls playlist.....");
      using (HttpClient client = new HttpClient())
      using (HttpResponseMessage res = client.GetAsync(url).Result)
      using (HttpContent content = res.Content)
      {
        string data = content.ReadAsStringAsync().Result;

        System.Console.WriteLine("Parsing playlist.....");
        PlaylistParser parser = new PlaylistParser();
        Playlist playlist = parser.Parse(data);

        System.Console.WriteLine($"Found {playlist.Items.Count} playlist items");

        System.Console.WriteLine($"First item is {((PlaylistTagItem)playlist.Items[0]).Id}");

      }
    }
  }
}