using System;

namespace QuizCraft
{
  public class Question : ICloneable
  {
    private readonly string question;
    private readonly string correctAnswer;
    private readonly Func<string, bool> inputValidator;

    public string CorrectAnswer => correctAnswer;

    public Question(string question, string correctAnswer)
    {
      this.question = question;
      this.correctAnswer = correctAnswer;
    }

    public Question(string question, string correctAnswer, Func<string, bool> inputValidator)
    {
      this.question = question;
      this.correctAnswer = correctAnswer;
      this.inputValidator = inputValidator;
    }

    public object Clone()
    {
      return new Question(question, correctAnswer);
    }

    public bool IsCorrect(string result)
    {
      return result == correctAnswer;
    }

    public override string ToString()
    {
      return question;
    }

    public string ToDebugString()
    {
      return $"{question} {correctAnswer}";
    }

    public bool IsValidInputForAnswer(string input)
    {
      if (inputValidator == null)
      {
        return true;
      }

      return inputValidator(input);
    }
  }
}
