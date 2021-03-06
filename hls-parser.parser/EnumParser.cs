using System;
using System.Linq;
using Sprache;

namespace HlsParser.Parser
{
  public static class EnumParser<T>
  {
    public static Parser<T> Create()
    {
      var names = Enum.GetNames(typeof(T));

      var parser = Parse.IgnoreCase(names.First()).Token()
          .Return((T)Enum.Parse(typeof(T), names.First()));

      foreach (var name in names.Skip(1))
      {
        string nameToParse = name.Replace('_', '-');
        parser = parser.Or(Parse.IgnoreCase(nameToParse).Token().Return((T)Enum.Parse(typeof(T), name)));
      }

      return parser;
    }
  }
}