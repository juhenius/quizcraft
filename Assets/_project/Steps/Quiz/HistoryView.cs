using System.Collections.Generic;
using UnityEngine;

namespace QuizCraft.Steps.Quiz
{
  public class HistoryView : MonoBehaviour, IRoundView
  {
    [SerializeField]
    private HistoryRow prefab;

    private readonly Dictionary<Question, HistoryRow> rows = new();

    public void SetCurrentRound(Round round)
    {
      SetFromRound(round);
      round.PropertyChanged += (_, args) =>
      {
        if (args.PropertyName == nameof(Round.History))
        {
          SetFromRound(round);
        }
      };
    }

    internal void SetFromRound(Round round)
    {
      int count = Mathf.Min(6, round.History.Count);
      int startIndex = round.History.Count - count;
      var questionsToShow = round.History.GetRange(startIndex, count);

      AddNewRows(questionsToShow);
      RemoveExtraRows(questionsToShow);
    }

    private void AddNewRows(List<Question> questions)
    {
      foreach (var question in questions)
      {
        SetupQuestionRow(question);
      }
    }

    private void RemoveExtraRows(List<Question> questions)
    {
      var keysToRemove = new List<Question>();
      foreach (var question in rows.Keys)
      {
        if (!questions.Contains(question))
        {
          keysToRemove.Add(question);
        }
      }

      foreach (var question in keysToRemove)
      {
        var row = rows[question];
        Destroy(row.gameObject);
        rows.Remove(question);
      }
    }

    private void SetupQuestionRow(Question question)
    {
      if (rows.TryGetValue(question, out var row))
      {
        return;
      }

      var newRow = Instantiate(prefab, transform);
      newRow.SetQuestion(question);
      rows.Add(question, newRow);
    }
  }
}
