using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardScript> _matchList = new();

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
            card1.GetComponentInChildren<ParticleSystem>().Play();
            CardScript card2 = _matchList[0];
            _matchList.Remove(card2);
            card2 .DisableButton();
            card2.GetComponentInChildren<ParticleSystem>().Play();
        }
        else
        {
            CardScript card1 = _matchList[0];
            _matchList.Remove(card1);
            card1.FlipBack();
            CardScript card2 = _matchList[0];
            _matchList.Remove(card2);
            card2.FlipBack();
        }
    }
}
