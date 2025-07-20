using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent GameOver;

    private List<CardScript> _matchList = new();
    private int _score = 0;
    private int _scoreMultiplyer = 1;
    private List<CardScript> _cardsList = new();
    [SerializeField] private TMP_Text _scoreText;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    public void GetCardList(List<CardScript> cardList)
    {
        _cardsList.AddRange(cardList);
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
            _cardsList.Remove(card1);
            card1.MatchFound?.Invoke();

            CardScript card2 = _matchList[0];
            _matchList.Remove(card2);
            card2 .DisableButton();
            _cardsList.Remove (card2);
            card2.MatchFound?.Invoke();

            AddScore(_scoreMultiplyer);
            _scoreMultiplyer ++; // Streak Bonus
            Invoke(nameof(GameOverCheck), 2f);
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
        GameSettings.Instance.Score = _score;
        _scoreText.text = _score.ToString();
    }

    void GameOverCheck()
    {
        if (_cardsList.Count == 0)
        {
            GameSettings.Instance.Score = _score;
            GameOver.AddListener(GameOverScene);
            GameOver?.Invoke();
        }
    }

    void GameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
