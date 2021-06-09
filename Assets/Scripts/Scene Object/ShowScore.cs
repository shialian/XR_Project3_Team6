using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowScore : MonoBehaviour
{

    public TextMeshProUGUI Team1Score;
    public TextMeshProUGUI Team2Score;

    private int team1score;
    private int team2score;

    // Start is called before the first frame update
    void Start()
    {
        Team1Score.SetText("0");
        Team2Score.SetText("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.singleton)
        {
            team1score = GameManager.singleton.getCookies[0];
            team2score = GameManager.singleton.getCookies[1];
            Team1Score.SetText(team1score.ToString());
            Team2Score.SetText(team2score.ToString());
        }
    }
}
