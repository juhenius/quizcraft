using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class TensAdditionQuestionGenerator : IQuestionGenerator
  {
    private Random random;

    public TensAdditionQuestionGenerator()
    {
      random = RandomFactory.CreateRandom();
    }

    public Question Create()
    {
      var firstOperand = random.NextInt(11) * 10;
      var secondOperand = random.NextInt(11) * 10;
      var correctAnswer = $"{firstOperand + secondOperand}";
      return new Question($"{firstOperand} + {secondOperand} =", correctAnswer, IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 4 && input.All(char.IsDigit);
    }
  }
}
