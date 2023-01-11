using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLeaderboard : MonoBehaviour
{
    public GameObject rankingUser;
    public Transform parentRanking;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void GetLeaderBoardData()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest();
        request.MaxResultsCount = 10;
        request.StatisticName = "Points";

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, error => { Debug.Log(error.GenerateErrorReport()); });
    }

    private void OnGetLeaderboard(GetLeaderboardResult result)
    {
        foreach (var user in result.Leaderboard)
        {
            GameObject newItem = Instantiate(rankingUser, parentRanking);
            newItem.transform.localScale = Vector3.one;
            newItem.GetComponent<LeaderboardItem>().SetDataLeaderboard(user.DisplayName, user.Position.ToString(), user.StatValue.ToString());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
