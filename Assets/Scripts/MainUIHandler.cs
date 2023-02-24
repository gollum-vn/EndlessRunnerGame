using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    private GameManager gameManager;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI yourScoreText;
    public Button restartButton;
    public Button backToMenuButton;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void Update()
    {
        ScoreUpdate();
    }
    public void SetActive()
    {
        Invoke("ActiveButton", 5f);
        gameOverText.gameObject.SetActive(true);
        yourScoreText.gameObject.SetActive(true);
    }
    void ActiveButton()
    {
        restartButton.gameObject.SetActive(true);
        backToMenuButton.gameObject.SetActive(true);
    }
    public void SetInactive()
    {
        gameOverText.gameObject.SetActive(false);
        yourScoreText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        backToMenuButton.gameObject.SetActive(false);
    }
    void ScoreUpdate()
    {
        if (gameManager.isGameActive)
        {
            gameManager.score += Time.time * gameManager.ObstacleSpeed / 600;
            scoreText.text = "Score:" + (int)gameManager.score;
            yourScoreText.text = gameManager.playerName + ": " + (int)gameManager.score;
        }
    }
}
