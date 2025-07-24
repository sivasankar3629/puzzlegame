using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
    public int Column;
    public int Row;

    [SerializeField] private Sprite[] _cardSprites;
    [SerializeField] private RectTransform _cardContainer;
    [SerializeField] private Transform _tempContainer;
    [SerializeField] private CardScript _cardPrefab;

    private List<CardScript> _cardsList = new();

    private void Awake()
    {
        if (GameSettings.Instance.IsLoadingSavedGame) return;

        Column = GameSettings.Instance.Column;
        Row = GameSettings.Instance.Row;
    }

    private void Start()
    {
        if (GameSettings.Instance.IsLoadingSavedGame)
        {
            LoadGame();
            ScaleCards();
        }
        else
        {
            InitRandomCards();
        }
    }

    void InitRandomCards()
    {
        InstantiateRandomCards();
        ScaleCards();
    }

    void LoadGame()
    {
        PlayerData data = SaveSystem.LoadGame();

        InstantiateKnownCards(data);
        GameManager.Instance.Score = data.score;
        GameManager.Instance.MatchesMade = data.matchMade;
        GameManager.Instance.UpdateScore(data.score);
        GameManager.Instance.ScoreMultiplier = data.scoreMultiplyer;
    }

    void InstantiateKnownCards(PlayerData data)
    {
        List<CardScript> cards = new List<CardScript>();
        for (int i = 0; i < data.ID.Count; i++)
        {
            CardScript card = Instantiate(_cardPrefab, _cardContainer, false);
            card.IsMatched = data.IsMatched[i];
            cards.Add(card);

            if (card.IsMatched)
            {
                card.ID = data.ID[i];
                card.DisableCard();
            }
            else
            {
                card.ID = data.ID[i];
                card.CardSprite = _cardSprites[data.ID[i]];
            }
        }
        GameManager.Instance.GetCardList(cards);
    }

    void InstantiateRandomCards()
    {
        int total = Column * Row;
        List<int> idList = new List<int>();

        // Card ID
        for ( int i = 0; i < total / 2; i++)
        {
            int randomID = Random.Range(0,_cardSprites.Length);
            idList.Add(randomID);
            idList.Add(randomID);
        }

        // Shuffle ID
        for (int i = 0; i < idList.Count; i++)
        {
            int randomNumber = Random.Range(0, idList.Count);
            int temp = idList[i];
            idList[i] = idList[randomNumber];
            idList[randomNumber] = temp;
        }

        // Instantiate
        foreach(int id in idList)
        {
            CardScript card = Instantiate(_cardPrefab, _cardContainer, false);
            card.ID = id;
            card.CardSprite = _cardSprites[id];
            _cardsList.Add(card);
        }
        GameManager.Instance.GetCardList(_cardsList);
    }

    void ScaleCards()
    {
        GridLayoutGroup gridLayout = _cardContainer.GetComponent<GridLayoutGroup>();

        // Total Length
        float totalWidth = _cardContainer.rect.width;
        float totalHeight = _cardContainer.rect.height;

        // Padding
        int top = gridLayout.padding.top = (int)(totalHeight * 0.1);
        int bottom = gridLayout.padding.bottom = (int)(totalHeight * 0.1);
        int left = gridLayout.padding.left = (int)(totalWidth * 0.2);
        int right = gridLayout.padding.right = (int)(totalWidth * 0.2);

        // Available Length
        float availableWidth = totalWidth - left - right;
        float availableHeight = totalHeight - top - bottom;

        // Size based on Width
        float spacingW = availableWidth * 0.01f;
        float totalSpacingW = spacingW * (Column - 1);
        float cellW = (availableWidth - totalSpacingW) / Column;

        // Size based on Height
        float spacingH = availableHeight * 0.01f;
        float totalSpacingH = spacingH * (Row - 1);
        float cellH = (availableHeight - totalSpacingH) / Row;

        // Comparing width and height
        float cellSize = Mathf.Min(cellW, cellH);
        float spacing = Mathf.Min(spacingW, spacingH);

        // Setting Values
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
        gridLayout.spacing = new Vector2(spacing, spacing);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = Column;
    }

}
