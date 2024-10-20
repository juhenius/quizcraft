using System.Linq;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace QuizCraft.Generators
{
  public class PairsThatMakeCloseToTenAdditionQuestionGenerator : IQuestionGenerator
  {
    private Random random;

    public PairsThatMakeCloseToTenAdditionQuestionGenerator()
    {
      var seed = (uint)(System.DateTimeOffset.Now.ToUnixTimeSeconds() * 1000);
      random = new Random(seed);
    }

    public Question Create()
    {
      var firstOperand = random.NextInt(11);
      var offset = random.NextInt(-1, 2);
      var secondOperand = Mathf.Max(0, 10 - firstOperand - offset);
      var correctAnswer = $"{firstOperand + secondOperand}";
      return new Question($"{firstOperand} + {secondOperand} =", correctAnswer, IsValidAnswerInput);
    }

    private bool IsValidAnswerInput(string input)
    {
      return input.Length <= 3 && input.All(char.IsDigit);
    }
  }
}
