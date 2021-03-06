using System.Collections.Generic;

namespace HlsParser.Parser
{
  public class Playlist
  {
    public Playlist()
    {
    }

    public Playlist(IEnumerable<PlaylistItem> items)
    {
      Items = new List<PlaylistItem>(items);
    }

    public bool IsMaster { get; set; }
    public List<PlaylistItem> Items { get; set; }
  }
}
