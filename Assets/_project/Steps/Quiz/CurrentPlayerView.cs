using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps.Quiz
{
  public class CurrentPlayerView : MonoBehaviour, IRoundView
  {
    [SerializeField]
    private TextView playerName;

    public void SetCurrentRound(Round round)
    {
      playerName.Text = round.Player;
    }
  }
}
