using QuizCraft.Steps.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuizCraft.Steps
{
  public class ScoresStep : GameStep
  {
    [SerializeField]
    private TextView output;
    [SerializeField]
    private ParticleSystem[] fireworks;
    [SerializeField]
    private float textDelay = 5f;
    private int currentScore = 0;

    protected override async Awaitable Started()
    {
      output.Text = "";
      currentScore = Round.History.Count;

      Statistics.IncrementGameCount();

      var highscore = Statistics.GetHighscore();
      if (currentScore > highscore)
      {
        Statistics.SetHighscore(currentScore);
      }

      foreach (var particleSystem in fireworks)
      {
        particleSystem.Play();
      }

      await Awaitable.WaitForSecondsAsync(textDelay, destroyCancellationToken);
      ShowText();
    }

    void Update()
    {
      if (Keyboard.current.enterKey.wasPressedThisFrame)
      {
        Finished();
      }

      if (Keyboard.current.rKey.wasPressedThisFrame)
      {
        currentScore = 0;
        Statistics.Clear();
        ShowText();
      }
    }

    void ShowText()
    {
      var highscore = Statistics.GetHighscore();
      var gameCount = Statistics.GetGameCount();

      output.Text = new TextBuilder()
        .WithTitle("Peli päättyi!")
        .WithLine($"Oikeita vastauksia: <rainb>{currentScore}</rainb>", fontSize: 80, lineHeight: 85)
        .WithLine($"Ennätyksesi: <rainb>{highscore}</rainb>", fontSize: 80, lineHeight: 85)
        .WithLine($"Pelattuja pelejä: <rainb>{gameCount}</rainb>", fontSize: 80, lineHeight: 115)
        .WithLine("<font=\"Englebert-Regular\">Uusi peli", fontSize: 40, lineHeight: 50)
        .WithLine("[<incr>Enter</incr>]", fontSize: 40)
        .Build();
    }
  }
}
