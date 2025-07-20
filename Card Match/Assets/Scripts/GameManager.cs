using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardScript> _matchList = new();
    private int _score = 0;
    private int _scoreMultiplyer = 1;
    [SerializeField] private TMP_Text _scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void OnCardFlip(CardScript card)
    {
        _matchList.Add(card);
        CheckMatch();
    }

    public void OnCardFlipBack(CardScript card)
    {
        _matchList.Remove(card);
    }

    void CheckMatch()
    {
        if (_matchList.Count % 2 == 0)
        {
            Invoke(nameof(MatchingLogic), 1f);
        }
    }

    void MatchingLogic()
    {
        if (_matchList[0].ID == _matchList[1].ID)
        {
            CardScript card1 = _matchList[0];
            _matchList.Remove(card1);
            card1.DisableButton();
            card1.MatchFound?.Invoke();
            CardScript card2 = _matchList[0];
            _matchList.Remove(card2);
            card2 .DisableButton();
            card2.MatchFound?.Invoke();

            AddScore(_scoreMultiplyer);
            _scoreMultiplyer ++; // Streak Bonus
        }
        else
        {
            CardScript card1 = _matchList[0];
            _matchList.Remove(card1);
            card1.FlipBack();
            CardScript card2 = _matchList[0];
            _matchList.Remove(card2);
            card2.FlipBack();

            // Reset Multiplyer
            _scoreMultiplyer = 1;
        }
    }

    void AddScore(int multiplyer)
    {
        _score += 5 * multiplyer;
        _scoreText.text = _score.ToString();
    }
}
