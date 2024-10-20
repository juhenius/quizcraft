using UnityEngine;

namespace QuizCraft
{
  public class Game : MonoBehaviour
  {
    public static Game Instance;

    public Round CurrentRound { get; private set; }

    [SerializeField]
    private GameStep[] steps;

    private bool startNextRound = false;

    void Awake()
    {
      Instance = this;
    }

    void Start()
    {
      foreach (var step in steps)
      {
        step.gameObject.SetActive(false);
      }

      startNextRound = true;
    }

    void Update()
    {
      if (startNextRound)
      {
        startNextRound = false;
        PlayRound();
      }
    }

    async void PlayRound()
    {
      CurrentRound = new Round();

      foreach (var step in steps)
      {
        await step.Execute(CurrentRound);
      }

      startNextRound = true;
    }
  }

}
