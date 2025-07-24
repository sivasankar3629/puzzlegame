using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardScript> _selectedCards = new();
    private List<CardScript> _cardsList = new();
    private int _matchesRequired;
    [SerializeField] private TMP_Text _scoreText;
    public int ScoreMultiplier = 1;
    public int Score = 0;
    public int MatchesMade;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(this);
        }
    }

    // Retrieve the list of cards from Card Spawner
    public void GetCardList(List<CardScript> cardsList)
    {
        _cardsList.AddRange(cardsList);
        _matchesRequired = cardsList.Count / 2;
    }

    public void OnCardFlip(CardScript card)
    {
        _selectedCards.Add(card);
        CheckMatch();
    }

    public void OnCardFlipBack(CardScript card)
    {
        _selectedCards.Remove(card);
    }

    public int GetTotalCard()
    {
        return _cardsList.Count;
    }

    // Return the list of cards
    public List<CardScript> GetCardsList()
    {
        return _cardsList;
    }

    void CheckMatch()
    {
        if (_selectedCards.Count % 2 == 0)
        {
            Invoke(nameof(MatchingLogic), 1f);
        }
    }

    void MatchingLogic()
    {
        if (_selectedCards.Count < 2) return;

        CardScript card1 = _selectedCards[0];
        CardScript card2 = _selectedCards[1];

        if (card1.ID == card2.ID)
        {
            HandleMatch(card1, card2);
        }
        else
        {
            HandleMismatch(card1, card2);
        }
    }

    void HandleMatch(CardScript card1, CardScript card2)
    {
        _selectedCards.Remove(card1);
        card1.DisableButton();
        card1.IsMatched = true;
        card1.MatchFound?.Invoke();

        _selectedCards.Remove(card2);
        card2.DisableButton();
        card2.IsMatched = true;
        card2.MatchFound?.Invoke();

        MatchesMade++;
        AddScore(ScoreMultiplier);
        ScoreMultiplier++; // Streak Bonus
        Invoke(nameof(GameOverCheck), 2.5f);
    }

    void HandleMismatch(CardScript card1, CardScript card2)
    {
        _selectedCards.Remove(card1);
        card1.MatchNotFound?.Invoke();
        card1.FlipBack();

        _selectedCards.Remove(card2);
        card2.MatchNotFound?.Invoke();
        card2.FlipBack();

        // Reset Multiplyer
        ScoreMultiplier = 1;
    }

    void AddScore(int multiplyer)
    {
        Score += 5 * multiplyer;
        GameSettings.Instance.Score = Score;
        UpdateScore(Score);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = Score.ToString();
    }

    void GameOverCheck()
    {
        Debug.Log("game over check");
        if ( MatchesMade == _matchesRequired)
        {
            GameSettings.Instance.Score = Score;
            GameOverScene();
            Debug.Log("match == req");
        }
    }

    void GameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }
}
