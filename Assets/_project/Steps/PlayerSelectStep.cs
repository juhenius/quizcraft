using System.Linq;
using QuizCraft.Steps.PlayerSelect;
using QuizCraft.Steps.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuizCraft.Steps
{
  public class PlayerSelectStep : GameStep
  {
    private enum State
    {
      PlayerSelect,
      NewPlayerCreation
    }

    [SerializeField]
    private TextView output;
    [SerializeField]
    private PlayerNamePrompt playerNamePrompt;

    private State state = State.PlayerSelect;

    void Start()
    {
      output.OptionClicked += HandleOptionSelect;
      playerNamePrompt.gameObject.SetActive(false);
    }

    protected override Awaitable Started()
    {
      playerNamePrompt.gameObject.SetActive(false);
      ShowPlayerList();

      return AwaitableUtils.CompletedTask();
    }

    void Update()
    {
      if (state == State.PlayerSelect)
      {
        if (KeyboardUtils.GetPressedNumberKey(out var number))
        {
          HandleOptionSelect(number);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
          Players.Clear();
          ShowPlayerList();
        }
      }
    }

    private async void HandleOptionSelect(int index)
    {
      var players = Players.GetPlayers();
      if (index == 0)
      {
        Round.Player = "Testeri";
      }
      else if (index > 0 && index <= players.Count)
      {
        Round.Player = players.ElementAtOrDefault(index - 1);
      }
      else if (index == players.Count + 1)
      {
        Round.Player = await CreateNewPlayer();
      }

      if (!string.IsNullOrEmpty(Round.Player))
      {
        Finished();
      }
    }

    private async Awaitable<string> CreateNewPlayer()
    {
      state = State.NewPlayerCreation;
      HidePlayerList();

      if (await playerNamePrompt.PromptPlayerName() is (true, var player))
      {
        Players.AddPlayer(player);
        return player;
      }

      ShowPlayerList();
      return null;
    }

    private void ShowPlayerList()
    {
      state = State.PlayerSelect;

      var builder = new TextBuilder()
        .WithTitle("Valitse pelaaja");

      Players.GetPlayers().ForEach(p => builder.WithOption(p));

      output.Text = builder
        .WithOption("Uusi pelaaja")
        .Build();
    }

    private void HidePlayerList()
    {
      output.Text = "";
    }
  }
}
