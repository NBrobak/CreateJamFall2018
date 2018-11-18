using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{

    // Use this for initialization
    public Base[] bases;
    public float Player1_score;
    public float Player2_score;
    public float Player3_score;
    public float Player4_score;
    public bool gameEnded = false;
    private bool hasRan;

    public Text winnerText;
    public string winnerString;
    private float winTextSizeFactor = 0.15f;
    public int winnersCount;
    public GameObject ragdollPrefab;
    private GameObject[] spawnPoints;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //is set through GameController
        if (gameEnded && !hasRan)
        {
            hasRan = true;
            //start coroutine wait sec, call button.
            StartCoroutine(ShowWhoWon());
        }
    }

    IEnumerator ShowWhoWon()
    {
		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPointRagDoll");
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                spawnBabies(Player1_score, spawnPoints[i].transform.position);
            }
            if (i == 1)
            {
                spawnBabies(Player2_score, spawnPoints[i].transform.position);
            }
            if (i == 2)
            {
                spawnBabies(Player3_score, spawnPoints[i].transform.position);
            }
            if (i == 3)
            {
                spawnBabies(Player4_score, spawnPoints[i].transform.position);
            }
        }
        yield return new WaitForSeconds(1.8f);
        Debug.Log(winnerText);
        // winnerText.text = winnerString + " Wins!";
        // winnerText.fontSize = (int)(winnerText.fontSize * (1f - (winTextSizeFactor * (winnersCount - 1f))));

        
        winnerText.transform.parent.gameObject.SetActive(true);


    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
        Destroy(this.gameObject);
    }
    public void spawnBabies(float amount, Vector3 pos)
    {
        for (int i = 0; i < amount; i++)
        {
            Debug.Log("Spawned Baby");
            Vector3 calcPos = new Vector3(pos.x, (pos.y * (1f * i)) + 20f, pos.z);
            Instantiate(ragdollPrefab, calcPos, Quaternion.identity);
        }
        //
    }

}
