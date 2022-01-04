using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public PlayerMovement player;
    public Text scoreText;
    public Text highscoreText;
    public GameObject restartButton;

    private int highscore = 0;

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale=1f;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", highscore);  
        highscoreText.text = "DIST M�X" + "\n" + highscore;
    }

    void Update()
    {
        if(player.Distance > highscore){
            highscore = player.Distance;
            PlayerPrefs.SetInt("highscore", highscore);
            highscoreText.text = "DIST M�X" + "\n" + highscore;
        }

        scoreText.text = "DIST�NCIA" + "\n" + player.Distance;

        if(Time.timeScale == 0f){
            restartButton.SetActive(true);
        } else {
            restartButton.SetActive(false);
        }
    }

    void OnDestroy() {
        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.Save();
    }
}
