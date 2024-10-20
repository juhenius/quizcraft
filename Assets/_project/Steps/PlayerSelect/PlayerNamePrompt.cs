using System;
using TMPro;
using UnityEngine;

namespace QuizCraft.Steps.PlayerSelect
{
  public class PlayerNamePrompt : MonoBehaviour
  {
    [SerializeField]
    private TMP_InputField input;

    private AwaitableCompletionSource<(bool, string)> process;

    void Start()
    {
      input.onSubmit.AddListener(OnSubmit);
    }

    public async Awaitable<(bool, string)> PromptPlayerName()
    {
      if (process != null)
      {
        throw new Exception("Multiple player name prompt in progress");
      }

      gameObject.SetActive(true);
      input.Select();
      input.ActivateInputField();

      process = new();
      var result = await process.Awaitable;

      gameObject.SetActive(false);
      process = null;

      return result;
    }

    private void OnSubmit(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        process.SetResult((false, null));
      }
      else
      {
        process.SetResult((true, value.Trim()));
      }
    }
  }
}