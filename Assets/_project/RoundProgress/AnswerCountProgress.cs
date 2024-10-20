namespace QuizCraft.Steps.RoundProgress
{
  public class AnswerCountProgress : IRoundProgress, IUniqueKeyProvider
  {
    private readonly int count;

    public AnswerCountProgress(int count)
    {
      this.count = count;
    }

    public string GetLabel(Round round)
    {
      return "Laskuja jäljellä";
    }

    public string GetValue(Round round)
    {
      var questionsLeft = count - round.History.Count;
      var startTag = questionsLeft <= 1 ? "<shake a=0.1>" : "";
      var endTag = questionsLeft <= 1 ? "</shake>" : "";
      return $"{startTag}{questionsLeft}{endTag}";
    }

    public bool IsFinished(Round round)
    {
      return round.History.Count >= count;
    }

    public string GetUniqueName()
    {
      return UniqueKeyGenerator.CreateUniqueName(this, count);
    }
  }
}
