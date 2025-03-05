using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QuizCraft.Steps.Shared
{
  public class TextView : MonoBehaviour, IPointerClickHandler
  {
    private string text;
    private TextMeshProUGUI output;
    private TypewriterByCharacter writer;

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
          writer.ShowText(value);
        }
      }
    }

    public bool AnimationEnabled
    {
      get => writer.useTypeWriter;
      set => writer.useTypeWriter = value;
    }

    void Awake()
    {
      output = GetComponent<TextMeshProUGUI>();
      writer = GetComponent<TypewriterByCharacter>();
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
