using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class CrossingTenAdditionQuestionGenerator : IQuestionGenerator
  {
    private Random random;

    public CrossingTenAdditionQuestionGenerator()
    {
      random = RandomFactory.CreateRandom();
    }

    public Question Create()
    {
      var firstOperand = random.NextInt(6, 10);
      var correctAnswer = random.NextInt(11, 10 + firstOperand);
      var secondOperand = correctAnswer - firstOperand;
      return new Question($"{firstOperand} + {secondOperand} =", $"{correctAnswer}", IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 3 && input.All(char.IsDigit);
    }
  }
}
