using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QuizCraft.Steps.Shared
{
  public class TextView : MonoBehaviour, IPointerClickHandler
  {
    private string text;
    private TextMeshProUGUI output;

    public delegate void LinkClickHandler(string linkId);
    public event LinkClickHandler LinkClicked;
    public delegate void OptionClickHandler(int optionIndex);
    public event OptionClickHandler OptionClicked;

    public string Text
    {
      get => text;
      set
      {
        if (value != text)
        {
          text = value;
          output.text = value;
        }
      }
    }

    void Awake()
    {
      output = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      int linkIndex = TMP_TextUtilities.FindIntersectingLink(output, eventData.position, eventData.pressEventCamera);
      if (linkIndex == -1)
      {
        return;
      }

      TMP_LinkInfo linkInfo = output.textInfo.linkInfo[linkIndex];
      var linkId = linkInfo.GetLinkID();
      if (!string.IsNullOrWhiteSpace(linkId))
      {
        LinkClicked?.Invoke(linkId);

        if (int.TryParse(linkId, out var optionIndex))
        {
          OptionClicked?.Invoke(optionIndex);
        }
      }
    }

    public void SetFade(float value)
    {
      Color color = output.color;
      color.a = value;
      output.color = color;
    }
  }
}
