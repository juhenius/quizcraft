using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps.Quiz
{
  public class HistoryRow : MonoBehaviour
  {
    [SerializeField]
    private TextView question;
    [SerializeField]
    private TextView answer;

    public void SetQuestion(Question question)
    {
      this.question.Text = question.ToString();
      this.answer.Text = question.CorrectAnswer;
    }
  }
}
