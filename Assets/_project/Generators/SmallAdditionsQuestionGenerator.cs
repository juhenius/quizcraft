using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class SmallAdditionsQuestionGenerator : IQuestionGenerator
  {
    private Random random;

    public SmallAdditionsQuestionGenerator()
    {
      var seed = (uint)(System.DateTimeOffset.Now.ToUnixTimeSeconds() * 1000);
      random = new Random(seed);
    }

    public Question Create()
    {
      var firstOperand = random.NextInt(15);
      var secondOperand = random.NextInt(3);
      var correctAnswer = $"{firstOperand + secondOperand}";
      return new Question($"{firstOperand} + {secondOperand} =", correctAnswer, IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 3 && input.All(char.IsDigit);
    }
  }
}
