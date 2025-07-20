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
        InitCards();
    }

    void InitCards()
    {
        InstantiateCards();
        ScaleCards();
        ShuffleCards(_cardsList);
        PlaceCards(_cardsList);
    }

    void InstantiateCards()
    {
        int total = Column * Row;
        for (int i = 0; i < total / 2; i++)
        {
            int randomNumber = Random.Range(0, _cardSprites.Length);
            for (int j = 0; j < 2; j++)
            {
                CardScript card = Instantiate(_cardPrefab.GetComponent<CardScript>(), _tempContainer, false);
                if (card != null)
                {
                    card.ID = randomNumber;
                    Debug.Log(card.ID);
                    card.CardSprite = _cardSprites[randomNumber];
                    _cardsList.Add(card);
                }
            }
        }
    }

    void ShuffleCards(List<CardScript> cards)
    {
        for ( int i = 0; i < cards.Count; i++ )
        {
            int randomNumber = Random.Range(0, cards.Count);
            CardScript temp = cards[i];
            cards[i] = cards[randomNumber];
            cards[randomNumber] = temp;
        }
    }

    void PlaceCards(List<CardScript> cards)
    {
        for ( int i = 0; i < cards.Count; i++ )
        {
            CardScript temp = cards[i];
            temp.transform.SetParent(_cardContainer);
        }
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
        Debug.Log(cellSize);
        gridLayout.spacing = new Vector2(spacing, spacing);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = Column;
    }

}
