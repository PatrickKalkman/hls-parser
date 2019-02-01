using HlsParser.Parser;
using NUnit.Framework;
using Sprache;
using System;
using System.Collections.Generic;

namespace HlsParser.Test
{
  [TestFixture]
  public class PlaylistParserTest
  {
    [Test]
    public void TwoRowsPlaylistIsParsed()
    {
      string input =
         "#EXTM3U\n#EXT-X-VERSION:4\n";
      var playlistParser = new PlaylistParser();
      Playlist playlist = playlistParser.Parse(input);
      Assert.That(playlist.Items.Count, Is.EqualTo(2));
      Assert.That(((PlaylistTagItem)playlist.Items[0]).Id, Is.EqualTo(PlaylistTagId.EXTM3U));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Id, Is.EqualTo(PlaylistTagId.EXT_X_VERSION));
    }

    [Test]
    public void SimplePlaylistIsParsed()
    {
      string input = "#EXTM3U" + Environment.NewLine +
         "#EXT-X-TARGETDURATION:10" + Environment.NewLine +
         "#EXT-X-VERSION:3" + Environment.NewLine +
         "#EXTINF:9.009," + Environment.NewLine +
         "http://media.example.com/first.ts" + Environment.NewLine +
         "#EXTINF:9.009," + Environment.NewLine +
         "http://media.example.com/second.ts" + Environment.NewLine +
         "#EXTINF:3.003," + Environment.NewLine +
         "http://media.example.com/third.ts" + Environment.NewLine +
         "#EXT-X-ENDLIST";
      var playlistParser = new PlaylistParser();
      Playlist playlist = playlistParser.Parse(input);
      Assert.That(playlist.Items.Count, Is.EqualTo(10));
      Assert.That(((PlaylistTagItem)playlist.Items[0]).Id, Is.EqualTo(PlaylistTagId.EXTM3U));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Id, Is.EqualTo(PlaylistTagId.EXT_X_TARGETDURATION));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Value, Is.EqualTo("10"));
    }

    [Test]
    public void MasterPlaylistFromSpecificationIsParsed()
    {
      string input = "#EXTM3U" + Environment.NewLine +
         "#EXT-X-STREAM-INF:BANDWIDTH=1280000,AVERAGE-BANDWIDTH=1000000" + Environment.NewLine +
         "http://example.com/low.m3u8" + Environment.NewLine +
         "#EXT-X-STREAM-INF:BANDWIDTH=2560000,AVERAGE-BANDWIDTH=2000000" + Environment.NewLine +
         "http://example.com/mid.m3u8" + Environment.NewLine +
         "#EXT-X-STREAM-INF:BANDWIDTH=7680000,AVERAGE-BANDWIDTH=6000000" + Environment.NewLine +
         "http://example.com/hi.m3u8" + Environment.NewLine +
         "#EXT-X-STREAM-INF:BANDWIDTH=65000,CODECS=\"mp4a.40.5\"" + Environment.NewLine +
         "http://example.com/audio-only.m3u8";
      var playlistParser = new PlaylistParser();
      Playlist playlist = playlistParser.Parse(input);
      Assert.That(playlist.Items.Count, Is.EqualTo(9));
      Assert.That(((PlaylistTagItem)playlist.Items[0]).Id, Is.EqualTo(PlaylistTagId.EXTM3U));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Id, Is.EqualTo(PlaylistTagId.EXT_X_STREAM_INF));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Attributes.Count, Is.EqualTo(2));
    }

    [Test]
    public void MasterPlaylistFromAzureMediaServicesIsParsed()
    {
      string input = "#EXTM3U" + Environment.NewLine +
                     "#EXT-X-VERSION:4" + Environment.NewLine +
                     "#EXT-X-MEDIA:TYPE=AUDIO,GROUP-ID=\"audio\",NAME=\"audio\",DEFAULT=YES,URI=\"QualityLevels(128000)/Manifest(audio,format=m3u8-aapl)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=555936,RESOLUTION=1120x700,CODECS=\"avc1.42c01f,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(400000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=555936,RESOLUTION=1120x700,CODECS=\"avc1.42c01f\",URI=\"QualityLevels(400000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=571266,RESOLUTION=426x240,CODECS=\"avc1.4d4015,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(415000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=571266,RESOLUTION=426x240,CODECS=\"avc1.4d4015\",URI=\"QualityLevels(415000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=913636,RESOLUTION=640x360,CODECS=\"avc1.4d401e,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(750000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=913636,RESOLUTION=640x360,CODECS=\"avc1.4d401e\",URI=\"QualityLevels(750000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=1475736,RESOLUTION=640x360,CODECS=\"avc1.64001e,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(1300000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=1475736,RESOLUTION=640x360,CODECS=\"avc1.64001e\",URI=\"QualityLevels(1300000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=1680136,RESOLUTION=1920x960,CODECS=\"avc1.640028,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(1500000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=1680136,RESOLUTION=1920x960,CODECS=\"avc1.640028\",URI=\"QualityLevels(1500000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=2446636,RESOLUTION=1280x720,CODECS=\"avc1.4d401f,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(2250000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=2446636,RESOLUTION=1280x720,CODECS=\"avc1.4d401f\",URI=\"QualityLevels(2250000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=2702136,RESOLUTION=1120x700,CODECS=\"avc1.64001f,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(2500000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=2702136,RESOLUTION=1120x700,CODECS=\"avc1.64001f\",URI=\"QualityLevels(2500000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=4235136,RESOLUTION=1280x720,CODECS=\"avc1.4d401f,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(4000000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=4235136,RESOLUTION=1280x720,CODECS=\"avc1.4d401f\",URI=\"QualityLevels(4000000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=5257136,RESOLUTION=1920x1080,CODECS=\"avc1.640028,mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(5000000)/Manifest(video,format=m3u8-aapl)" + Environment.NewLine +
                     "#EXT-X-I-FRAME-STREAM-INF:BANDWIDTH=5257136,RESOLUTION=1920x1080,CODECS=\"avc1.640028\",URI=\"QualityLevels(5000000)/Manifest(video,format=m3u8-aapl,type=keyframes)\"" + Environment.NewLine +
                     "#EXT-X-STREAM-INF:BANDWIDTH=138976,CODECS=\"mp4a.40.2\",AUDIO=\"audio\"" + Environment.NewLine +
                     "QualityLevels(128000)/Manifest(audio,format=m3u8-aapl)";
      var playlistParser = new PlaylistParser();
      Playlist playlist = playlistParser.Parse(input);
      Assert.That(playlist.Items.Count, Is.EqualTo(32));
      Assert.That(((PlaylistTagItem)playlist.Items[0]).Id, Is.EqualTo(PlaylistTagId.EXTM3U));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Id, Is.EqualTo(PlaylistTagId.EXT_X_VERSION));
      Assert.That(((PlaylistTagItem)playlist.Items[1]).Attributes.Count, Is.EqualTo(1));
      Assert.That(((PlaylistTagItem)playlist.Items[3]).Attributes[2].Key, Is.EqualTo("CODECS"));
      Assert.That(((PlaylistTagItem)playlist.Items[3]).Attributes[2].Value, Is.EqualTo("avc1.42c01f,mp4a.40.2"));
    }
  }
}