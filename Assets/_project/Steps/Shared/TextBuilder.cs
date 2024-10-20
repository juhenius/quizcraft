using System;
using System.Collections.Generic;
using System.Text;

namespace QuizCraft.Steps.Shared
{
  public class TextBuilder
  {
    const string EOL = "\n";

    private readonly StringBuilder result = new();
    private int nextOptionValue = 1;

    public TextBuilder WithTitle(string title)
    {
      var endLine = AppendLine(lineHeight: 150, fontSize: 150);
      result.Append(title);
      endLine();
      return this;
    }

    public TextBuilder WithOption(string value, string text)
    {
      var endLine = AppendLine();
      result.Append($"<link=\"{value}\">");
      result.Append($"<color=#999>[</color>{value}<color=#999>]</color> {text}");
      result.Append($"</link>");
      endLine();
      return this;
    }

    public TextBuilder WithOption(int value, string text)
    {
      return WithOption(value.ToString(), text);
    }

    public TextBuilder WithOption(string text)
    {
      return WithOption(nextOptionValue++, text);
    }

    public TextBuilder WithFontSize(int fontSize)
    {
      result.Append($"<size={fontSize}px>");
      return this;
    }

    public TextBuilder WithLine(string text, int lineHeight = 100, int fontSize = 100)
    {
      var endLine = AppendLine(lineHeight, fontSize);
      result.Append(text);
      endLine();
      return this;
    }

    public string Build()
    {
      return result.ToString();
    }

    private Action AppendLine(
      int lineHeight = 100,
      int fontSize = 100
    )
    {
      var settingsToClear = new List<string>();

      if (lineHeight != 100)
      {
        result.Append($"<line-height={lineHeight}%>");
        settingsToClear.Insert(0, "</line-height>");
      }

      if (fontSize != 100)
      {
        result.Append($"<size={fontSize}%>");
        settingsToClear.Insert(0, "</size>");
      }

      return () =>
      {
        AppendLineBreak();
        result.AppendJoin("", settingsToClear);
      };
    }

    private void AppendLineBreak()
    {
      result.Append(EOL);
    }
  }
}
