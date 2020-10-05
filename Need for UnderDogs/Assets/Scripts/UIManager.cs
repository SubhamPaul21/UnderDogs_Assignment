using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] Text firstPosition;
    [SerializeField] Text secondPosition;
    [SerializeField] Text thirdPosition;
    [SerializeField] Image subhamRank;
    [SerializeField] Image godSpeedRank;
    [SerializeField] Image pikachuRank;

    [SerializeField] Sprite[] rankSprites;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    void Update()
    {
        UpdateLeaderBoard();
    }

    private void UpdateLeaderBoard()
    {
        Dictionary<GameObject, float> leaderBoardDict = GameHandler.Instance.CalculateLeaderBoard();
        firstPosition.text = leaderBoardDict.ElementAt(2).Key.name;
        secondPosition.text = leaderBoardDict.ElementAt(1).Key.name;
        thirdPosition.text = leaderBoardDict.ElementAt(0).Key.name;
        DisplayPlayerHeadRank(leaderBoardDict);
    }

    void DisplayPlayerHeadRank(Dictionary<GameObject, float> leaderBoardDict)
    {
        // Third Position
        if (leaderBoardDict.ElementAt(0).Key.name == "Subham") { subhamRank.sprite = rankSprites[2]; }
        if (leaderBoardDict.ElementAt(0).Key.name == "GodSpeed") { godSpeedRank.sprite = rankSprites[2]; }
        if (leaderBoardDict.ElementAt(0).Key.name == "Pikachu") { pikachuRank.sprite = rankSprites[2]; }

        // Second Position
        if (leaderBoardDict.ElementAt(1).Key.name == "Subham") { subhamRank.sprite = rankSprites[1]; }
        if (leaderBoardDict.ElementAt(1).Key.name == "GodSpeed") { godSpeedRank.sprite = rankSprites[1]; }
        if (leaderBoardDict.ElementAt(1).Key.name == "Pikachu") { pikachuRank.sprite = rankSprites[1]; }

        // First Position
        if (leaderBoardDict.ElementAt(2).Key.name == "Subham") { subhamRank.sprite = rankSprites[0]; }
        if (leaderBoardDict.ElementAt(2).Key.name == "GodSpeed") { godSpeedRank.sprite = rankSprites[0]; }
        if (leaderBoardDict.ElementAt(2).Key.name == "Pikachu") { pikachuRank.sprite = rankSprites[0]; }
    }
}
