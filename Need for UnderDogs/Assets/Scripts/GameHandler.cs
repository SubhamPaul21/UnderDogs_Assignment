using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    public bool IsGameReady { get; private set; }

    [SerializeField] GameObject[] cars;

    float subhamRemainingDistance;
    float godSpeedRemainingDistance;
    float pikachuRemainingDistance;

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
        FetchRemainingDistance();
    }

    private void FetchRemainingDistance()
    {
        subhamRemainingDistance = cars[0].GetComponent<PlayerCar>().GetPathRemainingDistance();
        print("subhamRemainingDistance: " + subhamRemainingDistance);
        godSpeedRemainingDistance = cars[1].GetComponent<AICar>().GetPathRemainingDistance();
        //print("godSpeedRemainingDistance: " + godSpeedRemainingDistance);
        pikachuRemainingDistance = cars[2].GetComponent<AICar>().GetPathRemainingDistance();
        //print("pikachuRemainingDistance: " + pikachuRemainingDistance);
        CalculateLeaderBoard();
    }

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
}
