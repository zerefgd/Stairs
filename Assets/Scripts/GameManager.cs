using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startButton, endPanel, blockPrefab,player;

    [SerializeField]
    private TMP_Text scoreText, diamondText, highScoreText, highScoreEndText;

    [SerializeField]
    private Vector3 startPos, offset;

    private int score, diamondCount, combo, highScore;

    public bool hasGameStarted;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Time.timeScale = 1f;
        hasGameStarted = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(true);
        endPanel.SetActive(false);

        score = 0;
        combo = 1;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        diamondCount = PlayerPrefs.HasKey("Diamonds") ? PlayerPrefs.GetInt("Diamonds") : 0;

        scoreText.text = score.ToString();
        diamondText.text = diamondCount.ToString();
        highScoreText.text = "BEST : " + highScore.ToString();

        for (int i = 0; i < 5; i++)
        {
            SpawnBlock();
        }
    }

    public void SpawnBlock()
    {
        GameObject tempBlock = Instantiate(blockPrefab);
        startPos += offset;
        tempBlock.transform.position = startPos;
    }

    public void UpdateDiamond()
    {
        diamondCount++;
        PlayerPrefs.SetInt("Diamonds", diamondCount);
        diamondText.text = diamondCount.ToString();
    }

    public void UpdateScore()
    {
        combo = 1;
        score++;
        scoreText.text = score.ToString();
    }

    public void UpdateCombo()
    {
        combo++;
        score += combo;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreEndText.text = "BEST : " + highScore.ToString();
    }

    public void GameStart()
    {
        startButton.SetActive(false);
        player.GetComponent<Player>().hasGameStarted = true;
        hasGameStarted = true;
    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }
}
