using System.Text;
using UnityEngine;

namespace QuizCraft
{
  public static class Statistics
  {
    private static string HighscoreKey => $"{GetCurrentRoundKey(player: true, questions: true, progress: true)}.highscore";
    private static string GameCountKey => $"{GetCurrentRoundKey(player: true, questions: true, progress: true)}.count";
    private static string AnswerCountKey => $"{GetCurrentRoundKey(player: true, questions: true)}.answerCount";

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

    public static int GetAnswerCount()
    {
      return PlayerPrefs.GetInt(AnswerCountKey);
    }

    public static void IncrementAnswerCount(int value)
    {
      PlayerPrefs.SetInt(AnswerCountKey, GetAnswerCount() + value);
    }

    public static void Clear()
    {
      PlayerPrefs.DeleteKey(HighscoreKey);
      PlayerPrefs.DeleteKey(GameCountKey);
    }

    private static string GetCurrentRoundKey(
      bool player = false,
      bool questions = false,
      bool progress = false
    )
    {
      var builder = new StringBuilder();

      if (player)
      {
        builder.Append(Game.Instance.CurrentRound.Player);
        builder.Append(".");
      }

      if (questions)
      {
        builder.Append(UniqueKeyGenerator.GenerateKey(Game.Instance.CurrentRound.QuestionGenerators));
        builder.Append(".");
      }

      if (progress)
      {
        builder.Append(UniqueKeyGenerator.GenerateKey(Game.Instance.CurrentRound.Progress));
        builder.Append(".");
      }

      return builder.ToString();
    }
  }
}
