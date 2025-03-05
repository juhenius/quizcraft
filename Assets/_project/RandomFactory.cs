using Random = Unity.Mathematics.Random;

namespace QuizCraft
{
  public static class RandomFactory
  {
    private static int nextSeedMultiplier = 1;

    public static Random CreateRandom()
    {
      var seed = (uint)(System.DateTimeOffset.Now.ToUnixTimeSeconds() * nextSeedMultiplier++);
      return new Random(seed);
    }
  }
}
