using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class MultiplicationQuestionGenerator : IQuestionGenerator, IUniqueKeyProvider
  {
    private Random random;
    private readonly int[][] operands;
    private readonly int maximumAnswerLength;

    public MultiplicationQuestionGenerator(int[][] operands)
    {
      var seed = (uint)(System.DateTimeOffset.Now.ToUnixTimeSeconds() * 1000);
      random = new Random(seed);
      this.operands = operands;

      var maximumPossibleResult = operands.Select(a => a.Max())
        .Aggregate(1, (acc, max) => acc * max);
      maximumAnswerLength = $"{maximumPossibleResult}".Length + 1;
    }

    public Question Create()
    {
      var numbers = new int[operands.Length];

      for (var i = 0; i < operands.Length; ++i)
      {
        var possibleValues = operands[i];
        numbers[i] = possibleValues[random.NextInt(possibleValues.Length)];
      }

      var shuffledNumbers = numbers.OrderBy(x => random.NextInt());

      var question = $"{string.Join(" • ", shuffledNumbers)} =";
      var correctAnswer = $"{numbers.Aggregate((result, number) => number * result)}";
      return new Question(question, correctAnswer, IsValidAnswerInput);
    }

    public string GetUniqueName()
    {
      return UniqueKeyGenerator.CreateUniqueName(this, UniqueKeyGenerator.JaggedArrayToString(operands));
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= maximumAnswerLength && input.All(char.IsDigit);
    }
  }
}
