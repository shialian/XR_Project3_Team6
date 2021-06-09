using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowScore : MonoBehaviour
{

    private Text Team1Score;
    private Text Team2Score;

    private int team1score;
    private int team2score;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Team1Score = (GameObject.Find("Team1Score")).GetComponent<Text>();
        Team2Score = (GameObject.Find("Team2Score")).GetComponent<Text>();
        gameManager = (GameObject.Find("GameManager")).GetComponent<GameManager>();

        team1score = gameManager.getScore(1);
        team2score = gameManager.getScore(2);
        Team1Score.text = team1score.ToString();
        Team2Score.text = team2score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        team1score = gameManager.getScore(1);
        team2score = gameManager.getScore(2);
        Team1Score.text = team1score.ToString();
        Team2Score.text = team2score.ToString();

    }
}
