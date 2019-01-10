using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
//using UnityEngine.UI;

public class ScoresManager : MonoBehaviour
{

    //public Text scoreText;
    //public Text coinCountText;
    //public Text highScoreText;


    protected int score = 0, highScore = 0, levelHighScore = 0, coinScore = 0, currentLevelID = 0;

    public static ScoresManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentLevelID = SceneManager.GetActiveScene().buildIndex;
        SecurePlayerPrefs.Init();
        ProcessScores(true);
    }

    public void ResetScore()
    {
        score = 0;
    }


    public virtual void UpdateScore()
    {
        score++;
        //scoreText.text = score.ToString();
        if (score > highScore)
        {
            highScore = score;
            //highScoreText.text = highScore.ToString();
        }

        if (score > levelHighScore)
        {
            levelHighScore = score;
            //highScoreText.text = highScore.ToString();
        }

    }

    public virtual void UpdateCoinScore()
    {
        if (LevelSelection.index != 3)
            coinScore++;
        else
            coinScore += 3;

        //coinCountText.text = "x " + coinScore.ToString();

        ProcessScores(false);
    }

    public virtual void UpdateCoinVideoAdScore(int reward)
    {
        coinScore += reward;
        //coinCountText.text = "x " + coinScore.ToString();

        ProcessScores(false);
    }

    public virtual void Buy(int price)
    {
        coinScore -= price;
        //coinCountText.text = "x " + coinScore.ToString();

        ProcessScores(false);
    }

    public virtual void ProcessScores(bool isOnStart)
    {
        if (isOnStart)
        {
            //load scores from preferences to variables
            highScore = SecurePlayerPrefs.GetInt("HighScore");
            levelHighScore = SecurePlayerPrefs.GetInt("HighScoreLvl" + currentLevelID.ToString());
            coinScore = SecurePlayerPrefs.GetInt("CoinScore");
            //scoreText.text = score.ToString();
            //highScoreText.text = highScore.ToString();
            //coinCountText.text = "x " + coinScore.ToString();

        }
        else
        {
            if (score.Equals(highScore))
            {
                SecurePlayerPrefs.SetInt("HighScore", highScore);

                if (PlayGamesPlatform.Instance.IsAuthenticated())
                {
                    //send highScore to Google Play Games Overall HighScores leaderboard
                    PlayGamesManager.AddScoreToLeaderboard(GPGSIds.leaderboard_overall_high_scores, Convert.ToInt64(highScore));
                }
            }

            if ((score.Equals(levelHighScore)) && (currentLevelID != 0))//Leaderboard
            {
                SecurePlayerPrefs.SetInt("HighScoreLvl" + currentLevelID.ToString(), highScore);

                if (PlayGamesPlatform.Instance.IsAuthenticated())
                {
                    //send highScore to Google Play Games Level leaderboard
                    switch (currentLevelID)
                    {
                        case 3:
                            PlayGamesManager.AddScoreToLeaderboard(GPGSIds.leaderboard_level_1_high_scores, Convert.ToInt64(levelHighScore));
                            break;
                        case 4:
                            PlayGamesManager.AddScoreToLeaderboard(GPGSIds.leaderboard_level_2_high_scores, Convert.ToInt64(levelHighScore));
                            break;
                        case 5:
                            PlayGamesManager.AddScoreToLeaderboard(GPGSIds.leaderboard_level_3_high_scores, Convert.ToInt64(levelHighScore));
                            break;
                        case 6:
                            PlayGamesManager.AddScoreToLeaderboard(GPGSIds.leaderboard_level_4_high_scores, Convert.ToInt64(levelHighScore));
                            break;
                    }

                }
            }

            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                switch (coinScore)
                {
                    case 5:
                        PlayGamesManager.UnlockAchievement(GPGSIds.achievement_first_5_coins);
                        break;
                    case 25:
                        PlayGamesManager.UnlockAchievement(GPGSIds.achievement_first_25_coins);
                        break;
                    case 50:
                        PlayGamesManager.UnlockAchievement(GPGSIds.achievement_first_50_coins);
                        break;
                    case 100:
                        PlayGamesManager.UnlockAchievement(GPGSIds.achievement_first_100_coins);
                        break;
                    case 250:
                        PlayGamesManager.UnlockAchievement(GPGSIds.achievement_first_250_coins);
                        break;
                    default:
                        break;
                }
            }


            SecurePlayerPrefs.SetInt("CoinScore", coinScore);
        }
    }




}
