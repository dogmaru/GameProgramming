using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using UnityEditor;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public TMP_Text scoreText;
    public TMP_Text recordText;
    public float score;

    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 10; // 1ÃÊ´ç 10Á¡
            SetText();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void AddScore()
    {
        score += 100;
        SetText();
    }

    public void SetText()
    {
        scoreText.text = "Score | " + (int)score;
    }

    public void EndGame()
    {
        isGameOver = true;
        gameoverText.SetActive(true);

        float bestScore = PlayerPrefs.GetFloat("BestScore");

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        recordText.text = "Best Score: " + (int)bestScore;
    }
}
