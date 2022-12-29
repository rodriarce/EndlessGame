﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour {

	public static PlayerLogic instance;
	private Text scoreText;
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
		scoreText = GameObject.Find("Canvas").transform.Find("gameplayUI").transform.Find("score").GetComponent<Text>();
		score = PlayerPrefs.GetInt("lastScore", 0);
		scoreText.text = "SCORE: " + score;
		

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
		GameObject.Find("Canvas").GetComponent<MenuSelect>().GameOver();
		
		explosionEffect.SetActive(true);

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
		if(col.gameObject.name.Equals("success"))
		{
			Debug.Log("Succes Pass Blocks");
			score++;
			PlayerPrefs.SetInt("lastScore", score);
			if(score > PlayerPrefs.GetInt("bestScore", 1)){
				PlayerPrefs.SetInt("bestScore", score);
			}
			scoreText.text = "SCORE: " + score;
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
