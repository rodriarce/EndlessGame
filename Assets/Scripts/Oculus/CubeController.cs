using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class CubeController : MonoBehaviour
{
    private ColorSwap colorSwap;
    public bool isEmpty;
    public Vector3 sizeBox;
    public LayerMask layerBox;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        colorSwap = GetComponentInParent<ColorSwap>();
       

    }
    public void SelectEnter()
    {
        PlayerPrefs.SetInt("colorChange", PlayerPrefs.GetInt("colorChange") + 1);
        //changeSound.Play();
        Renderer rend = GetComponent<Renderer>();
        if (rend.material.GetColor("_Color").Equals(colorSwap.color))// Empty Block
        {
            isEmpty = true;
           //transform.GetComponent<BoxCollider>().isTrigger = true;
            rend.material.SetColor("_Color", new Color(1, 1, 1, 0.7f));
        }
        else
        {
            isEmpty = false;
            transform.GetComponent<BoxCollider>().isTrigger = false;
            rend.material.SetColor("_Color", colorSwap.color);
        }
    }

    private void GameOver()
    {
        GameObject.Find("explosionSound").GetComponent<AudioSource>().Play();
        GameObject.Find("Canvas").GetComponent<MenuSelect>().GameOver();
        explosion.transform.parent = null;
        explosion.SetActive(true);
        //Camera.main.transform.parent = null;
        Destroy(transform.parent.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
        var colliders = Physics.OverlapBox(transform.position, sizeBox, Quaternion.identity, layerBox, QueryTriggerInteraction.Collide);
        
        foreach (var collider in colliders)
        {
            if (collider.transform.CompareTag("obstacle"))
            {
                if (collider.transform.GetComponent<ObstacleController>().isFull && isEmpty)
                {
                    GameOver();
                }
                 else if (!collider.transform.GetComponent<ObstacleController>().isFull && !isEmpty)
                {
                    GameOver();
                }

                Debug.Log("You hit a box " + collider.transform.name);
                
            }
            
        }
        
        
    }
}
