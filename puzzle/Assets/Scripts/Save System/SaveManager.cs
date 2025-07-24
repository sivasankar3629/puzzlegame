using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        else {
            Destroy(this);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(GameManager.Instance);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadGame();


    }
}
