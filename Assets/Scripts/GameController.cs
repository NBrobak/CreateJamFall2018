using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	private Timer timer;
	[SerializeField]
	private Base[] playerBases;

	private void Awake()
	{
		timer.notifyOfEndedGame += ChooseWinner;
	}

	public void ChooseWinner()
	{

	}

	public void RestartGame()
	{

	}
}
