using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[SerializeField]
	private Timer timer;
	[SerializeField]
	private Base[] playerBases;
	[SerializeField]
	private Text winnerText;

	private void Awake()
	{
		timer.notifyOfEndedGame += ChooseWinner;
	}

	public void ChooseWinner()
	{
		IEnumerable<Player> winners = playerBases.GroupBy(b => b.Point)
												 .OrderByDescending(p => p)
												 .First()
												 .Select(b => b.MasterBaby);

		SetWinners(winners);
	}

	private void SetWinners(IEnumerable<Player> winners)
	{
		string winnerNames = string.Join(" and ", winners.Select(p => p.PlayerName).ToArray());
		winnerText.text = winnerNames + " Wins!";

		winnerText.gameObject.SetActive(true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene("GameScene");
	}
}
