
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace QuizCraft
{
  public class Round : INotifyPropertyChanged
  {
    private string player;
    private List<IQuestionGenerator> questionGenerators = new();
    private IRoundProgress progress;
    private Question currentQuestion;
    private string currentAnswer = string.Empty;
    private List<Question> history = new();
    private float timeElapsed;

    public string Player
    {
      get => player;
      set
      {
        player = value;
        OnPropertyChanged(nameof(Player));
      }
    }

    public List<IQuestionGenerator> QuestionGenerators
    {
      get => questionGenerators;
      set
      {
        questionGenerators = value;
        OnPropertyChanged(nameof(QuestionGenerators));
      }
    }

    public IRoundProgress Progress
    {
      get => progress;
      set
      {
        progress = value;
        OnPropertyChanged(nameof(Progress));
      }
    }

    public Question CurrentQuestion
    {
      get => currentQuestion;
      set
      {
        currentQuestion = value;
        OnPropertyChanged(nameof(CurrentQuestion));
      }
    }

    public string CurrentAnswer
    {
      get => currentAnswer;
      set
      {
        currentAnswer = value;
        OnPropertyChanged(nameof(CurrentAnswer));
      }
    }

    public List<Question> History
    {
      get => history;
      set
      {
        history = value;
        OnPropertyChanged(nameof(History));
      }
    }

    public float TimeElapsed
    {
      get => timeElapsed;
      set
      {
        timeElapsed = value;
        OnPropertyChanged(nameof(TimeElapsed));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void GenerateNewQuestion()
    {
      if (CurrentQuestion != null)
      {
        AddToHistory(CurrentQuestion);
      }

      var generator = QuestionGenerators[Random.Range(0, QuestionGenerators.Count)];
      CurrentQuestion = generator.Create();
      CurrentAnswer = "";
    }

    private void AddToHistory(Question question)
    {
      History.Add(question);
      OnPropertyChanged(nameof(History));
    }

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
