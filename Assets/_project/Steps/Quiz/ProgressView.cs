using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps.Quiz
{
  public class ProgressView : MonoBehaviour, IRoundView
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
        if (args.PropertyName == nameof(Round.TimeElapsed))
        {
          SetFromRound(round);
        }
      };
    }

    private void SetFromRound(Round round)
    {
      label.Text = round.Progress.GetLabel(round);
      value.Text = round.Progress.GetValue(round);
    }
  }
}
