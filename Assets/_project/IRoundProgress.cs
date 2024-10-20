namespace QuizCraft
{
  public interface IRoundProgress
  {
    public string GetLabel(Round round);
    public string GetValue(Round round);
    public bool IsFinished(Round round);
  }
}
