using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    private void Awake()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score : {GameSettings.Instance.Score}";
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("Game");
    }
}
