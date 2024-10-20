using QuizCraft.Steps.RoundProgress;
using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps
{
  public class ProgressSelectStep : GameStep
  {
    [SerializeField]
    private TextView output;

    void Start()
    {
      output.OptionClicked += HandleOptionSelect;
    }

    protected override Awaitable Started()
    {
      output.Text = new TextBuilder()
        .WithTitle("Valitse pelimuoto")
        .WithOption("Aika: 60 s")
        .WithOption("Määrä: 10 kpl")
        .Build();
      return AwaitableUtils.CompletedTask();
    }

    void Update()
    {
      if (KeyboardUtils.GetPressedNumberKey(out var number))
      {
        HandleOptionSelect(number);
      }
    }

    private void HandleOptionSelect(int index)
    {
      switch (index)
      {
        case 1:
          Round.Progress = new TimeProgress(60);
          break;
        case 2:
          Round.Progress = new AnswerCountProgress(10);
          break;
      }

      if (Round.Progress != null)
      {
        Finished();
      }
    }
  }
}
