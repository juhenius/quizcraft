using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps.Quiz
{
  public class AnswerCountView : MonoBehaviour, IRoundView
  {
    [SerializeField]
    private TextView label;
    [SerializeField]
    private TextView value;

    public void SetCurrentRound(Round round)
    {
      SetFromRound(round);
      round.PropertyChanged += (_, args) =>
      {
        if (args.PropertyName == nameof(Round.History))
        {
          SetFromRound(round);
        }
      };
    }

    private void SetFromRound(Round round)
    {
      var answerCount = round.History.Count;

      var isHighscore = IsHighscore(answerCount);
      var startHighscoreTag = isHighscore ? "<rainb f=2>" : "";
      var endHighscoreTag = isHighscore ? "</rainb>" : "";

      value.Text = $"{startHighscoreTag}{answerCount}{endHighscoreTag}";
    }

    private bool IsHighscore(int value)
    {
      return value > Statistics.GetHighscore();
    }
  }
}
