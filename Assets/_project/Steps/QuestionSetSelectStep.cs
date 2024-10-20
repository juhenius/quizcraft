using System.Collections.Generic;
using QuizCraft.Generators;
using QuizCraft.Steps.Shared;
using UnityEngine;

namespace QuizCraft.Steps
{
  public class QuestionSetSelectStep : GameStep
  {
    private enum State
    {
      TopLevel,
      MultiplicationTables,
      Additions,
    }

    [SerializeField]
    private TextView output;

    private State state = State.TopLevel;

    void Start()
    {
      output.OptionClicked += HandleOptionSelect;
    }

    protected override Awaitable Started()
    {
      ShowTopLevelMenu();
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
      switch (state)
      {
        case State.TopLevel:
          if (index == 1)
          {
            ShowAdditionsMenu();
          }
          else if (index == 2)
          {
            ShowMultiplicationTablesMenu();
          }
          break;

        case State.Additions:
          if (index == 1)
          {
            Round.QuestionGenerators = CreateSmallAdditionsQuestionSet();
          }
          else if (index == 2)
          {
            Round.QuestionGenerators = CreatePairsThatMakeTenQuestionSet();
          }
          else if (index == 3)
          {
            Round.QuestionGenerators = CreateCrossingTenQuestionSet();
          }
          else if (index == 4)
          {
            Round.QuestionGenerators = CreateTensQuestionSet();
          }
          else if (index == 5)
          {
            Round.QuestionGenerators = CreateLargeNumbersQuestionSet();
          }
          break;

        case State.MultiplicationTables:
          if (index >= 2 && index <= 9)
          {
            Round.QuestionGenerators = CreateMultiplicationTableQuestionSet(index);
          }
          break;
      }


      if (Round.QuestionGenerators.Count > 0)
      {
        Finished();
      }
    }

    private void ShowTopLevelMenu()
    {
      state = State.TopLevel;
      output.Text = new TextBuilder()
              .WithTitle("Valitse kysymykset")
              .WithFontSize(30)
              .WithOption(1, "Yhteenlaskut")
              .WithOption(2, "Kertolaskut")
              .Build();
    }

    private void ShowAdditionsMenu()
    {
      state = State.Additions;
      output.Text = new TextBuilder()
              .WithTitle("Valitse yhteenlaskut")
              .WithFontSize(30)
              .WithOption(1, "Pienet lisäykset")
              .WithOption(2, "Kymppiparit")
              .WithOption(3, "Kymmenylitykset")
              .WithOption(4, "Kympit")
              .WithOption(5, "Isot luvut")
              .Build();
    }

    private void ShowMultiplicationTablesMenu()
    {
      state = State.MultiplicationTables;
      output.Text = new TextBuilder()
              .WithTitle("Valitse kertolaskut")
              .WithFontSize(30)
              .WithOption(2, "2-kertotaulu")
              .WithOption(3, "3-kertotaulu")
              .WithOption(4, "4-kertotaulu")
              .WithOption(5, "5-kertotaulu")
              .WithOption(6, "6-kertotaulu")
              .WithOption(7, "7-kertotaulu")
              .WithOption(8, "8-kertotaulu")
              .WithOption(9, "9-kertotaulu")
              .Build();
    }

    private List<IQuestionGenerator> CreateSmallAdditionsQuestionSet()
    {
      return new List<IQuestionGenerator>()
      {
        new SmallAdditionsQuestionGenerator()
      };
    }

    private List<IQuestionGenerator> CreatePairsThatMakeTenQuestionSet()
    {
      return new List<IQuestionGenerator>()
      {
        new PairsThatMakeCloseToTenAdditionQuestionGenerator()
      };
    }

    private List<IQuestionGenerator> CreateCrossingTenQuestionSet()
    {
      return new List<IQuestionGenerator>()
      {
        new CrossingTenAdditionQuestionGenerator()
      };
    }

    private List<IQuestionGenerator> CreateTensQuestionSet()
    {
      return new List<IQuestionGenerator>()
      {
        new TensAdditionQuestionGenerator()
      };
    }

    private List<IQuestionGenerator> CreateLargeNumbersQuestionSet()
    {
      return new List<IQuestionGenerator>()
      {
        new LargeNumbersAdditionQuestionGenerator()
      };
    }

    private List<IQuestionGenerator> CreateMultiplicationTableQuestionSet(int table)
    {
      return new List<IQuestionGenerator>()
      {
        new MultiplicationTableQuestionGenerator(table)
      };
    }
  }
}
