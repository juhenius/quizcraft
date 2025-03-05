using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class SmallAdditionsQuestionGenerator : IQuestionGenerator
  {
    private Random random;

    public SmallAdditionsQuestionGenerator()
    {
      random = RandomFactory.CreateRandom();
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
