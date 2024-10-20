using UnityEngine.InputSystem;

namespace QuizCraft.Steps.Shared
{
  public static class KeyboardUtils
  {
    public static bool GetPressedNumberKey(out int number)
    {
      number = -1;
      if (Keyboard.current.digit0Key.wasPressedThisFrame)
      {
        number = 0;
      }
      if (Keyboard.current.digit1Key.wasPressedThisFrame)
      {
        number = 1;
      }
      if (Keyboard.current.digit2Key.wasPressedThisFrame)
      {
        number = 2;
      }
      if (Keyboard.current.digit3Key.wasPressedThisFrame)
      {
        number = 3;
      }
      if (Keyboard.current.digit4Key.wasPressedThisFrame)
      {
        number = 4;
      }
      if (Keyboard.current.digit5Key.wasPressedThisFrame)
      {
        number = 5;
      }
      if (Keyboard.current.digit6Key.wasPressedThisFrame)
      {
        number = 6;
      }
      if (Keyboard.current.digit7Key.wasPressedThisFrame)
      {
        number = 7;
      }
      if (Keyboard.current.digit8Key.wasPressedThisFrame)
      {
        number = 8;
      }
      if (Keyboard.current.digit9Key.wasPressedThisFrame)
      {
        number = 9;
      }

      return number != -1;
    }
  }
}
