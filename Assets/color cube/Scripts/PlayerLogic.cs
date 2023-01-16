using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;


public class PlayerLogic : MonoBehaviour {

	public static PlayerLogic instance;
	public TextMeshProUGUI scoreText;
	public float speed = 0.3f;
	public Rigidbody rb;
	private AudioSource successSound;
	private int score = 0;
	public static int collision = 0;
	public bool isGameOver;
	public GameObject explosionEffect;
	public Transform lastPosition;


    private void Awake()
    {
        if (instance == null)
        {
			instance = this;
        }
    }
    void Start() {
		successSound = GameObject.Find("successSound").GetComponent<AudioSource> ();
		//scoreText = UIManager.instance.textStats.GetComponent<TextMeshPro>();
		score = PlayerPrefs.GetInt("lastScore", 0);
		UIManager.instance.textStats.text = "SCORE: " + score;
		

		if(collision >= 1) {//this is used to check if player crashed and than continued game after watching the ad
			GameObject newObstacle = Instantiate (Resources.Load ("obstacle") as GameObject);
			newObstacle.transform.parent = GameObject.Find("obstacles").transform;
			newObstacle.transform.localPosition = new Vector3(0,0.55f, (score + collision) * 100 + 100);

			GameObject newFloor = Instantiate (Resources.Load ("floor") as GameObject);
			newFloor.transform.parent = GameObject.Find("floorPlanes").transform;
			newFloor.transform.localPosition = new Vector3(0,0.55f, (score + collision) * 100);
			speed = 0.3f + ((float)score / 100);
		}
	}

	public void GameOver()
    {
		isGameOver = true;
		GameObject.Find("explosionSound").GetComponent<AudioSource>().Play();
		//GameObject.Find("Canvas").GetComponent<MenuSelect>().GameOver();
		//MenuSelect.instance.GameOver();
		UIManager.instance.panelUI.SetActive(true);
		UIManager.instance.panelWin.SetActive(false);
		UIManager.instance.panelLose.SetActive(true);
		
		explosionEffect.SetActive(true);

	}

	public void RestartGame()
	{
		Debug.Log("Restar Game");
		DataUser.isReload = true;
		PlayerPrefs.SetInt("restartTheGame", 1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnReloadGame()
    {
		Time.timeScale = 1;
		
		//collision++;
		explosionEffect.SetActive(false);
		//Destroy(GameObject.Find("Main Camera"));
		MenuSelect.instance.gameOverMenuUI.SetActive(false);
		MenuSelect.instance.gameplayUI.SetActive(true);
		//GameObject player = Instantiate(Resources.Load("player") as GameObject);
		transform.position = new Vector3(0, 0, lastPosition.position.z);
		//player.name = "player";
		MenuSelect.instance.pauseButton.SetActive(true);
		MenuSelect.instance.scoreText.SetActive(true);
		isGameOver = false;
	}

	void FixedUpdate () {
		if (!isGameOver)
        {
			rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + speed);
		}
		
	}

	void OnTriggerEnter(Collider col) {
        if (col.gameObject.name.Equals("success"))
        {
            Debug.Log("Succes Pass Blocks");
            score++;
            PlayerPrefs.SetInt("lastScore", score);
            if (score > DataUser.amountPoints) {
                PlayerPrefs.SetInt("bestScore", score);
                var playerStatistics = new UpdatePlayerStatisticsRequest();
                StatisticUpdate stat = new StatisticUpdate();
                stat.StatisticName = "Points";
                stat.Value = score;
                DataUser.amountPoints = score;
                playerStatistics.Statistics = new List<StatisticUpdate>()
            {
                stat,
            };
                PlayFabClientAPI.UpdatePlayerStatistics(playerStatistics, result => { Debug.Log("Succes Update Stat"); }, error => { Debug.Log(error.GenerateErrorReport()); });
                UIManager.instance.textBestScore.text = "SCORE: " + DataUser.amountPoints.ToString();

            }
           
			scoreText.text = score.ToString();
			GameObject newObstacle = Instantiate (Resources.Load ("obstacle") as GameObject);
			newObstacle.transform.parent = GameObject.Find("obstacles").transform;
			newObstacle.transform.localPosition = new Vector3(0,0.55f, (score + collision) * 100 + 100);
			Destroy(col.transform.parent.gameObject, 0.5f);

			GameObject newFloor = Instantiate (Resources.Load ("floor") as GameObject);
			newFloor.transform.parent = GameObject.Find("floorPlanes").transform;
			newFloor.transform.localPosition = new Vector3(0,0.55f, (score + collision) * 100);

			GameObject[] floorGameObjects = GameObject.FindGameObjectsWithTag("floor");
			foreach (GameObject floor in floorGameObjects)
			{
				if(floor.transform.position.z < (transform.position.z - 100)) {
					Destroy(floor);
				}
			}

			successSound.Play();
			speed+=0.01f;

		}

		if (col.CompareTag("lastPosition"))
        {
			lastPosition = col.transform;
			lastPosition.transform.parent = null;

		}


	}
	
}
