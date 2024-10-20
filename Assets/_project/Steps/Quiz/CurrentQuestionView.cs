using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps.Quiz
{
  public class CurrentQuestionView : MonoBehaviour, IRoundView, ICelebrate
  {
    [SerializeField]
    private TextView question;
    [SerializeField]
    private TextView answer;

    public void SetCurrentRound(Round round)
    {
      SetQuestion(round);
      SetAnswer(round);

      round.PropertyChanged += (_, args) =>
      {
        if (args.PropertyName == nameof(Round.CurrentQuestion))
        {
          SetQuestion(round);
        }
        if (args.PropertyName == nameof(Round.CurrentAnswer))
        {
          SetAnswer(round);
        }
      };
    }

    private void SetQuestion(Round round)
    {
      question.Text = round.CurrentQuestion != null ? round.CurrentQuestion.ToString() : "";
    }

    private void SetAnswer(Round round)
    {
      answer.Text = round.CurrentAnswer;
    }

    public void Celebrate(Round round)
    {
      var startTag = "<rainb f=2><bounce a=0.2 f=2>";
      var endTag = "</bounce></rainb>";

      question.Text = $"{startTag}{round.CurrentQuestion}{endTag}";
      answer.Text = $"{startTag}{round.CurrentAnswer}{endTag}";
    }
  }
}
