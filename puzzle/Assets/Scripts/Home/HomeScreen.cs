using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public void SetRow(int row)
    {
        GameSettings.Instance.Row = row;
    }

    public void SetColumn(int col)
    {
        GameSettings.Instance.Column = col;
    }

    public void OnClickLevel()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickLoad()
    {
        GameSettings.Instance.IsLoadingSavedGame = true;
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
