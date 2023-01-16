using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Button buttonPlay;
    public Button buttonLeaderboard;
    public GameObject panelUI;
    public TextMeshProUGUI textStats;
    public GameObject panelWin;
    public GameObject panelLose;
    public Button playAgainButton;
    public TextMeshProUGUI textBestScore;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonPlay.onClick.AddListener(() => {OnButtonPlay() ; });
        playAgainButton.onClick.AddListener(() => { PlayerLogic.instance.RestartGame();});
        if (DataUser.isReload)
        {
            panelWin.SetActive(false);            
            OnButtonPlay();
        }
        textBestScore.text = "SCORE: " + DataUser.amountPoints.ToString();
        
    }
    private void OnButtonPlay()
    {
        panelUI.SetActive(false);
        MenuSelect.instance.GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
