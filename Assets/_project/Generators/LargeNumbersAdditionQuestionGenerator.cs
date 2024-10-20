using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class LargeNumbersAdditionQuestionGenerator : IQuestionGenerator
  {
    private Random random;

    public LargeNumbersAdditionQuestionGenerator()
    {
      var seed = (uint)(System.DateTimeOffset.Now.ToUnixTimeSeconds() * 1000);
      random = new Random(seed);
    }

    public Question Create()
    {
      var correctAnswer = random.NextInt(100, 156);
      var secondOperand = random.NextInt(10, 100);
      var firstOperand = correctAnswer - secondOperand;
      return new Question($"{firstOperand} + {secondOperand} =", $"{correctAnswer}", IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 4 && input.All(char.IsDigit);
    }
  }
}
