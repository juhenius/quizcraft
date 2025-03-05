using System.Linq;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class DivisionQuestionGenerator : IQuestionGenerator
  {
    private Random random;
    private int minAnswer = 1;
    private int maxAnswer = 5;
    private int minDivisor = 1;
    private int maxDivisor = 5;

    public DivisionQuestionGenerator()
    {
      random = RandomFactory.CreateRandom();
    }

    public DivisionQuestionGenerator(int minAnswer, int maxAnswer, int minDivisor, int maxDivisor) : this()
    {
      this.minAnswer = minAnswer;
      this.maxAnswer = maxAnswer;
      this.minDivisor = minDivisor;
      this.maxDivisor = maxDivisor;
    }

    public Question Create()
    {
      var correctAnswer = random.NextInt(minAnswer, maxAnswer);
      var divisor = random.NextInt(minDivisor, maxDivisor);
      var divident = correctAnswer * divisor;
      return new Question($"{divident} : {divisor} =", $"{correctAnswer}", IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 4 && input.All(char.IsDigit);
    }
  }
}
