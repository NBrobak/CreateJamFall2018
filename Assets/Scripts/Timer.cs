using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

	public float gameTime = 120;
	public float countDownTime = 5;

	[SerializeField]
	private Text countDownText;
	[SerializeField]
	private TextMesh timerText;
    public AudioClip countDownSound;
	public Action notifyOfEndedGame;
    private AudioSource audioSrc;
    private bool doOnce = true;

	public float timerContent {
		get { return float.Parse(timerText.text.Remove(0, 6)); }
		private set { timerText.text = "Time: " + value.ToString("n2"); }
	}

	public float countDownTimerContent
	{
		get { return float.Parse(countDownText.text.Trim('.')); }
		private set { countDownText.text = Mathf.FloorToInt(value) + "..."; }
	}

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
	{
		UpdateTimers(Time.timeSinceLevelLoad);
	}

	private void UpdateTimers(float timePlayed)
	{
		timerContent = gameTime - timePlayed;
		if (timePlayed > gameTime)
		{
			EndGame();
		}
		else if (timePlayed > gameTime - countDownTime)
		{
            if (doOnce)
            {
                doOnce = false;
                audioSrc.clip = countDownSound;
                audioSrc.volume = .1f;
                audioSrc.Play();
            }
			countDownTimerContent = gameTime - timePlayed;
		}
	}

	private void EndGame()
	{
		if(notifyOfEndedGame != null)
		{
			notifyOfEndedGame.Invoke();
		}
		gameObject.SetActive(false);
		SpawnController.Instance.StopSpawning();
	}
}
