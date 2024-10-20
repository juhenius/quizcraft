using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps
{
  public class StartCountdownStep : GameStep
  {
    [SerializeField]
    private int durationInSeconds = 3;

    [SerializeField]
    private TextView output;

    protected override async Awaitable Started()
    {
      for (var i = durationInSeconds; i > 0; --i)
      {
        SetText(i);
        await Awaitable.WaitForSecondsAsync(1, destroyCancellationToken);

        if (destroyCancellationToken.IsCancellationRequested)
        {
          return;
        }
      }

      SetText("GO");
      await Awaitable.WaitForSecondsAsync(1, destroyCancellationToken);

      Finished();
    }

    private void SetText(string text)
    {
      output.Text = $"<size=200px>{text}";
    }

    private void SetText(int number)
    {
      SetText(number.ToString());
    }
  }
}
