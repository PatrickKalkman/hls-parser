namespace HlsParser.Parser
{
  public class PlaylistUriItem : PlaylistItem
  {
    public string Uri { get; }

    public PlaylistUriItem(string uri)
    {
      Uri = uri;
    }
  }
}