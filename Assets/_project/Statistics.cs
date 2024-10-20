using System.Text;
using UnityEngine;

namespace QuizCraft
{
  public static class Statistics
  {
    private static string HighscoreKey => $"{GetCurrentRoundKey()}.highscore";
    private static string GameCountKey => $"{GetCurrentRoundKey()}.count";

    public static int GetHighscore()
    {
      return PlayerPrefs.GetInt(HighscoreKey);
    }

    public static void SetHighscore(int value)
    {
      PlayerPrefs.SetInt(HighscoreKey, value);
    }

    public static int GetGameCount()
    {
      return PlayerPrefs.GetInt(GameCountKey);
    }

    public static void IncrementGameCount()
    {
      PlayerPrefs.SetInt(GameCountKey, GetGameCount() + 1);
    }

    public static void Clear()
    {
      PlayerPrefs.DeleteKey(HighscoreKey);
      PlayerPrefs.DeleteKey(GameCountKey);
    }

    private static string GetCurrentRoundKey()
    {
      var builder = new StringBuilder();

      builder.Append(Game.Instance.CurrentRound.Player);
      builder.Append(".");

      builder.Append(UniqueKeyGenerator.GenerateKey(Game.Instance.CurrentRound.QuestionGenerators));
      builder.Append(".");

      builder.Append(UniqueKeyGenerator.GenerateKey(Game.Instance.CurrentRound.Progress));
      builder.Append(".");

      return builder.ToString();
    }
  }
}
