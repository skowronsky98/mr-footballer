using UnityEngine;
using UnityEngine.UI;


public class ScoreManagerText : ScoresManager 
{
    public Text scoreText;
    public Text coinCountText;
    public Text highScoreText;

    public override void UpdateScore()
    {
        base.UpdateScore();
        scoreText.text = score.ToString();

        if (score == levelHighScore)
        {
            highScoreText.text = levelHighScore.ToString();
        }

    }

    public override void UpdateCoinScore()
    {
        base.UpdateCoinScore();
        coinCountText.text = "x " + coinScore.ToString();

    }

    public override void UpdateCoinVideoAdScore(int reward)
    {
        base.UpdateCoinVideoAdScore(reward);
        coinCountText.text = "x " + coinScore.ToString();
    }

    public override void Buy(int price)
    {
        base.Buy(price);
        coinCountText.text = "x " + coinScore.ToString();

    }


    public override void ProcessScores(bool isOnStart)
    {
        base.ProcessScores(isOnStart);

        if (isOnStart)
        {
            scoreText.text = score.ToString();
            highScoreText.text = levelHighScore.ToString();
            coinCountText.text = "x " + coinScore.ToString();

        }

    }







}
