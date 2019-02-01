using System.Collections.Generic;
using System.IO;
using Sprache;

namespace HlsParser.Parser
{
  public class PlaylistParser
  {
    public Playlist Parse(string playlist)
    {
      using (StringReader stringReader = new StringReader(playlist))
      {
        List<PlaylistItem> playListItems = new List<PlaylistItem>();
        string line;
        while ((line = stringReader.ReadLine()) != null)
        {
          playListItems.Add(PlaylistGrammar.PlaylistParser.Parse(line));
        }
        return new Playlist(playListItems);
      }
    }
  }
}