using HlsParser.Parser;
using NUnit.Framework;
using Sprache;
using System.Collections.Generic;

namespace HlsParser.Test
{
  public class PlaylistGrammarTest
  {
    [SetUp]
    public void Setup()
    {

    }

    [TestCase("#EXTM3U")]
    [TestCase("#EXTM3U:")]
    [TestCase("#EXTM3U\n")]
    [TestCase("#EXTM3U\n#EXTM3U")]
    public void ASequenceOfCharactersThatStartWithPoundSymbolIsTag(string input)
    {
      string tagId = PlaylistGrammar.TagIdStringParser.Parse(input);
      Assert.AreEqual("EXTM3U", tagId);
    }
  }
}