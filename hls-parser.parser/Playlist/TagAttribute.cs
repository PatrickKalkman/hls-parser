namespace HlsParser.Parser
{
  public class PlaylistTagAttribute
  {
    public PlaylistTagAttribute(string key, string value)
    {
      Key = key;
      Value = value;
    }

    public string Key { get; }
    public string Value { get; }
  }
}