using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuizCraft
{
  public static class Players
  {
    const string SEPARATOR = "|";
    const string KEY = "players";

    public static List<string> GetPlayers()
    {
      var serialized = PlayerPrefs.GetString(KEY);
      if (serialized == null)
      {
        return new List<string>();
      }

      return serialized.Split(SEPARATOR, System.StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static void AddPlayer(string player)
    {
      if (player.Contains(SEPARATOR))
      {
        throw new System.Exception($"Player name cannot contain {SEPARATOR} character");
      }

      var existing = GetPlayers();
      if (existing.Contains(player))
      {
        throw new System.Exception($"Player name {player} already exists");
      }

      existing.Add(player);
      existing.Sort();
      var serialized = string.Join(SEPARATOR, existing);
      PlayerPrefs.SetString(KEY, serialized);
    }

    public static void Clear()
    {
      PlayerPrefs.DeleteKey(KEY);
    }
  }
}
