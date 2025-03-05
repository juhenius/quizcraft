using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class LargeNumbersAdditionQuestionGenerator : IQuestionGenerator
  {
    private Random random;
    private int minAnswer = 100;
    private int maxAnswer = 156;
    private int minValue = 10;
    private int maxValue = 100;

    public LargeNumbersAdditionQuestionGenerator()
    {
      random = RandomFactory.CreateRandom();
    }

    public LargeNumbersAdditionQuestionGenerator(int minAnswer, int maxAnswer, int minValue, int maxValue) : this()
    {
      this.minAnswer = minAnswer;
      this.maxAnswer = maxAnswer;
      this.minValue = minValue;
      this.maxValue = maxValue;
    }

    public Question Create()
    {
      var correctAnswer = random.NextInt(minAnswer, maxAnswer);
      var secondOperand = random.NextInt(minValue, maxValue);
      var firstOperand = correctAnswer - secondOperand;
      return new Question($"{firstOperand} + {secondOperand} =", $"{correctAnswer}", IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 4 && input.All(char.IsDigit);
    }
  }
}
