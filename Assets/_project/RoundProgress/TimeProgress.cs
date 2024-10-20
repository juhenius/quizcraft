using UnityEngine;

namespace QuizCraft.Steps.RoundProgress
{
  public class TimeProgress : IRoundProgress, IUniqueKeyProvider
  {
    private readonly int seconds;

    public TimeProgress(int seconds)
    {
      this.seconds = seconds;
    }

    public string GetLabel(Round round)
    {
      return "Aikaa jäljellä";
    }

    public string GetValue(Round round)
    {
      var roundTimeLeft = Mathf.FloorToInt(seconds - round.TimeElapsed);
      var startTag = roundTimeLeft < 15f ? "<shake a=0.1>" : "";
      var endTag = roundTimeLeft < 15f ? "</shake>" : "";
      return $"{startTag}{roundTimeLeft}{endTag}";
    }

    public bool IsFinished(Round round)
    {
      return round.TimeElapsed >= seconds;
    }

    public string GetUniqueName()
    {
      return UniqueKeyGenerator.CreateUniqueName(this, seconds);
    }
  }
}
