using UnityEngine;
using UnityEngine.InputSystem;

namespace QuizCraft.Steps.Shared
{
  public class RewardView : MonoBehaviour
  {
    [SerializeField]
    private TextView output;
    private bool visible;

    void Update()
    {
      if (Keyboard.current.spaceKey.wasPressedThisFrame)
      {
        ToggleVisibility();
      }

      if (IsVisible())
      {
        UpdateOutput();
      }
    }

    private bool IsVisible()
    {
      return visible;
    }

    private void ToggleVisibility()
    {
      visible = !IsVisible();
      output.AnimationEnabled = true;
      UpdateOutput();
      output.AnimationEnabled = false;
    }

    private void UpdateOutput()
    {
      if (!IsVisible() || Statistics.GetPlayerReward() == 0)
      {
        output.Text = "";
      }
      else
      {
        output.Text = new TextBuilder()
          .WithLine($"<font=\"Englebert-Regular\">Palkinto: {FormatReward(Statistics.GetPlayerReward())}", fontSize: 24)
          .Build();
      }
    }

    private static string FormatReward(int reward)
    {
      var minutes = Mathf.FloorToInt(reward / 60);
      var seconds = reward % 60;
      if (minutes > 0)
      {
        return $"{minutes} m {seconds} s";
      }

      return $"{seconds} s";
    }
  }
}