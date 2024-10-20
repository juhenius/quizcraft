using UnityEngine;

namespace QuizCraft
{
  public class GameStep : MonoBehaviour
  {
    private AwaitableCompletionSource completionSource;
    protected Round Round { get; private set; }

    public async Awaitable Execute(Round round)
    {
      completionSource = new();
      Round = round;
      gameObject.SetActive(true);
      await Started();
      await completionSource.Awaitable;
    }

    protected async void Finished()
    {
      await Ended();
      gameObject.SetActive(false);
      completionSource.SetResult();
    }

    protected virtual Awaitable Started()
    {
      return AwaitableUtils.CompletedTask();
    }

    protected virtual Awaitable Ended()
    {
      return AwaitableUtils.CompletedTask();
    }
  }
}
