using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    public bool IsGameReady { get; private set; }

    public string HasPlayerWon { get; private set; }

    [SerializeField] GameObject[] cars;

    float subhamRemainingDistance;
    float godSpeedRemainingDistance;
    float pikachuRemainingDistance;

    PlayerCar subham;
    AICar godspeed;
    AICar pikachu;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    void Start()
    {
        StartCoroutine(InitializeGame());
    }

    // Initialize game after 3 seconds
    private IEnumerator InitializeGame()
    {
        IsGameReady = false;
        yield return new WaitForSeconds(3f);
        IsGameReady = true;
    }

    void Update()
    {
        if (IsGameReady)
        {
            FetchRemainingDistance();
        }
    }

    // calculate the remaining distance for all the cars
    private void FetchRemainingDistance()
    {
        subham = cars[0].GetComponent<PlayerCar>();
        if (subham != null) { subhamRemainingDistance = subham.GetPathRemainingDistance(); }
        //print("subhamRemainingDistance: " + subhamRemainingDistance);
        godspeed = cars[1].GetComponent<AICar>();
        if (godspeed != null) { godSpeedRemainingDistance = godspeed.GetPathRemainingDistance(); }
        //print("godSpeedRemainingDistance: " + godSpeedRemainingDistance);
        pikachu = cars[2].GetComponent<AICar>();
        if (pikachu != null) { pikachuRemainingDistance = pikachu.GetPathRemainingDistance(); }
        //print("pikachuRemainingDistance: " + pikachuRemainingDistance);
        CalculateLeaderBoard();
        CalculateGameOutcome(subham);
    }

    // calculate the leaderboard stats
    public Dictionary<GameObject, float> CalculateLeaderBoard()
    {
        Dictionary<GameObject, float> leaderBoardDict = new Dictionary<GameObject, float>
        {
            { cars[0], subhamRemainingDistance },
            { cars[1], godSpeedRemainingDistance },
            { cars[2], pikachuRemainingDistance }
        };
        leaderBoardDict = leaderBoardDict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        return leaderBoardDict;
    }

    // calculate if player is winner or loser
    private void CalculateGameOutcome(PlayerCar player)
    {
        if (subhamRemainingDistance <= 0.3f && player.HasPlayerReachedEnd
            && godSpeedRemainingDistance >= 0.3f && pikachuRemainingDistance >= 0.3f)
        {
            HasPlayerWon = "yes";
        }
        else if (godSpeedRemainingDistance <= 0.3f || pikachuRemainingDistance <= 0.3f
            && !player.HasPlayerReachedEnd)
        {
            HasPlayerWon = "no";
        }
    }

    // method to go to menu on button click
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // method to quit game
    public void QuitApplication()
    {
        Application.Quit();
    }
}


