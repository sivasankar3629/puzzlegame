using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int ID;
    public bool IsSelected;
    public Sprite CardSprite;
    [SerializeField] private Image CardImage;

    [SerializeField] private Sprite _defaultSprite;

    public void ShowSprite()
    {
        CardImage.sprite = CardSprite;
    }

    public void HideSprite()
    {
        CardImage.sprite = null;
    }

}
