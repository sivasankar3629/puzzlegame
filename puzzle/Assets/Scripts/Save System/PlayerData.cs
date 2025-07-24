using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<int> ID = new();
    public List<bool> IsMatched = new();
    public int score;
    public int matchMade;
    public int scoreMultiplyer;
    public int turnsLeft;

    public PlayerData( GameManager manager)
    {
        for (int i = 0; i < manager.GetTotalCard(); i++)
        {
            ID.Add(manager.GetCardsList()[i].ID);
            IsMatched.Add(manager.GetCardsList()[i].IsMatched);
        }
        score = manager.Score;
        matchMade = manager.MatchesMade;
        scoreMultiplyer = manager.ScoreMultiplier;
        turnsLeft = manager.TurnsLeft;
    }
}
