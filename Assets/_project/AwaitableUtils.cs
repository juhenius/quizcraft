using UnityEngine;

namespace QuizCraft
{
  public static class AwaitableUtils
  {
    public static Awaitable CompletedTask()
    {
      var acs = new AwaitableCompletionSource();
      acs.SetResult();
      return acs.Awaitable;
    }
  }
}
