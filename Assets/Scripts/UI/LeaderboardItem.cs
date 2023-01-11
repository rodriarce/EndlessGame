using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public TextMeshProUGUI textUserName;
    public TextMeshProUGUI textPosRanking;
    public TextMeshProUGUI textPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetDataLeaderboard(string nameUser, string posRanking, string textPoint)
    {
        textUserName.text = nameUser;
        textPosRanking.text = posRanking;
        textPoints.text = textPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
