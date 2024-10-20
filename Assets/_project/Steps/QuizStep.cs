using QuizCraft.Steps.Quiz;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace QuizCraft.Steps
{
  public class QuizStep : GameStep
  {
    private bool gameIsRunning = false;
    private IRoundView[] Views => GetComponentsInChildren<IRoundView>();

    protected override Awaitable Started()
    {
      foreach (var view in Views)
      {
        view.SetCurrentRound(Round);
      }

      Round.GenerateNewQuestion();
      gameIsRunning = true;

      return AwaitableUtils.CompletedTask();
    }

    void Update()
    {
      if (!gameIsRunning)
      {
        return;
      }

      if (Round.Progress.IsFinished(Round))
      {
        Finished();
        return;
      }

      Round.TimeElapsed += Time.deltaTime;

      var previousAnswer = Round.CurrentAnswer;
      foreach (KeyControl keyControl in Keyboard.current.allKeys)
      {
        if (keyControl.wasPressedThisFrame)
        {
          if (keyControl.keyCode == Key.Backspace)
          {
            if (Round.CurrentAnswer.Length > 0)
            {
              Round.CurrentAnswer = Round.CurrentAnswer.Remove(Round.CurrentAnswer.Length - 1);
            }
          }
          else
          {
            string newAnswer = $"{Round.CurrentAnswer}{GetKeyRepresentation(keyControl, Keyboard.current)}";
            if (Round.CurrentQuestion.IsValidInputForAnswer(newAnswer))
            {
              Round.CurrentAnswer = newAnswer;
            }
          }
        }
      }

      if (Round.CurrentAnswer != previousAnswer)
      {
        if (Round.CurrentQuestion.IsCorrect(Round.CurrentAnswer))
        {
          HandleCorrectAnswer();
        }
      }
    }

    async void HandleCorrectAnswer()
    {
      gameIsRunning = false;
      Celebrate();

      await Awaitable.WaitForSecondsAsync(1, destroyCancellationToken);

      Round.GenerateNewQuestion();
      gameIsRunning = true;
    }

    private string GetKeyRepresentation(KeyControl keyControl, Keyboard keyboard)
    {
      switch (keyControl.keyCode)
      {
        case Key.Space:
          return " ";
      }

      if (keyControl.keyCode.IsTextInputKey())
      {
        return keyControl.displayName;
      }

      return "";
    }

    private void Celebrate()
    {
      foreach (var c in GetComponentsInChildren<ICelebrate>())
      {
        c.Celebrate(Round);
      }
    }
  }
}
