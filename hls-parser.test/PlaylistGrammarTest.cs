using HlsParser.Parser;
using NUnit.Framework;
using Sprache;
using System.Collections.Generic;

namespace HlsParser.Test
{
  public class PlaylistGrammarTest
  {
    [TestCase("#EXTM3U")]
    [TestCase("#EXTM3U:")]
    [TestCase("#EXTM3U\n")]
    [TestCase("#EXTM3U\n#EXTM3U")]
    public void ShouldParseStringThatStartsWithPoundAsPlaylistTag(string input)
    {
      string tagId = PlaylistGrammar.TagIdStringParser.Parse(input);
      Assert.AreEqual("EXTM3U", tagId);
    }

    [Test]
    public void TagIdIsParsedStronglyTyped()
    {
      var input = "#EXTM3U\n";
      PlaylistTagId typedTagId = PlaylistGrammar.TagIdParser.Parse(input);
      Assert.AreEqual(PlaylistTagId.EXTM3U, typedTagId);
    }

    [Test]
    public void SingleTagAttributeValueThatStartWithEnterIsNotParsed()
    {
      var input = "\n#EXTM3U";
      PlaylistTagAttribute tagAttribute = PlaylistGrammar.TagAttributeParser.Parse(input);
      Assert.AreEqual("", tagAttribute.Key);
      Assert.AreEqual("", tagAttribute.Value);
    }

    [Test]
    public void SingleTagAttributeValueIsParsed()
    {
      var input = "10";
      PlaylistTagAttribute tagAttribute = PlaylistGrammar.TagAttributeParser.Parse(input);
      Assert.AreEqual("10", tagAttribute.Key);
    }

    [Test]
    public void TagAttributesAreParsed()
    {
      var input = "TYPE=AUDIO";
      PlaylistTagAttribute tagAttribute = PlaylistGrammar.TagAttributeParser.Parse(input);
      Assert.AreEqual("TYPE", tagAttribute.Key);
      Assert.AreEqual("AUDIO", tagAttribute.Value);
    }

    [Test]
    public void TagAttributeWithValueInQuotesIsParsed()
    {
      var input = @"TYPE=""AUDIO""";
      PlaylistTagAttribute tagAttribute = PlaylistGrammar.TagAttributeParser.Parse(input);
      Assert.AreEqual("TYPE", tagAttribute.Key);
      Assert.AreEqual("AUDIO", tagAttribute.Value);
    }

    [Test]
    public void TagAttributeWithValueInQuotesThatIncludeACommaIsParsed()
    {
      var input = @"TYPE=""AUD,IO""";
      PlaylistTagAttribute tagAttributeString = PlaylistGrammar.AttributeWithQuotesParser.Parse(input);
      Assert.AreEqual("TYPE", tagAttributeString.Key);
      Assert.AreEqual("AUD,IO", tagAttributeString.Value);
    }

    [Test]
    public void TagAttributeWithoutQuotesIsParsed()
    {
      var input = @"TYPE=AUDIO";
      PlaylistTagAttribute tagAttributeString = PlaylistGrammar.AttributeWithoutQuotesParser.Parse(input);
      Assert.AreEqual("TYPE", tagAttributeString.Key);
      Assert.AreEqual("AUDIO", tagAttributeString.Value);
    }

    [Test]
    public void TagAttributeWithSingleValueIsParsed()
    {
      var input = @"10";
      PlaylistTagAttribute tagAttributeString = PlaylistGrammar.AttributeWithSingleValueParser.Parse(input);
      Assert.AreEqual("10", tagAttributeString.Key);
    }

    [Test]
    public void MultipleTagAttributesAreParsed()
    {
      var input = @"TYPE=AUDIO,GROUP-ID=""audio"",NAME=""audio"",DEFAULT=YES";
      List<PlaylistTagAttribute> tagAttributes = PlaylistGrammar.MultipleTagAttributesParser.Parse(input);
      Assert.That(tagAttributes.Count, Is.EqualTo(4));
      Assert.AreEqual("TYPE", tagAttributes[0].Key);
      Assert.AreEqual("AUDIO", tagAttributes[0].Value);
      Assert.AreEqual("GROUP-ID", tagAttributes[1].Key);
      Assert.AreEqual("audio", tagAttributes[1].Value);
      Assert.AreEqual("NAME", tagAttributes[2].Key);
      Assert.AreEqual("audio", tagAttributes[2].Value);
      Assert.AreEqual("DEFAULT", tagAttributes[3].Key);
      Assert.AreEqual("YES", tagAttributes[3].Value);
    }

    [Test]
    public void TagWithSingleAttributeValueIsParsed()
    {
      var input = @"#EXT-X-VERSION:4";
      PlaylistTagItem tag = (PlaylistTagItem)PlaylistGrammar.PlaylistTagParser.Parse(input);
      Assert.That(tag.Id, Is.EqualTo(PlaylistTagId.EXT_X_VERSION));
      Assert.That(tag.Attributes.Count, Is.EqualTo(1));
      Assert.AreEqual("4", tag.Attributes[0].Key);
    }

    [Test]
    public void TagWithMultipleAttributeValueIsParsed()
    {
      var input = @"#EXT-X-STREAM-INF:BANDWIDTH=555936,RESOLUTION=1120x700,CODECS=""avc1.42c01fmp4a.40.2"",AUDIO=""audio""";
      PlaylistTagItem tag = (PlaylistTagItem)PlaylistGrammar.PlaylistTagParser.Parse(input);
      Assert.That(tag.Id, Is.EqualTo(PlaylistTagId.EXT_X_STREAM_INF));
      Assert.That(tag.Attributes.Count, Is.EqualTo(4));
      Assert.AreEqual("RESOLUTION", tag.Attributes[1].Key);
      Assert.AreEqual("1120x700", tag.Attributes[1].Value);
    }

    [Test]
    public void AnUriIsASequenceOfCharactersThatStartsNotWithAPoundsymbol()
    {
      var input = "QualityLevels(400000)/Manifest(video,format=m3u8-aapl)\n";
      string uri = ((PlaylistUriItem)PlaylistGrammar.UriStringParser.Parse(input)).Uri;
      Assert.AreEqual("QualityLevels(400000)/Manifest(video,format=m3u8-aapl)", uri);
    }
  }
}