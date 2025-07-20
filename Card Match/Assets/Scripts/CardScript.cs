using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public int ID;
    private bool _isSelected;
    [Header("References")]
    public Sprite CardSprite;
    [SerializeField] private Image _cardImage;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _animator2;

    public void ShowSprite()
    {
        _cardImage.sprite = CardSprite;
        _isSelected = true;
        GameManager.Instance.OnCardFlip(this);
    }

    public void HideSprite()
    {
        _cardImage.sprite = _defaultSprite;
        _isSelected = false;
        GameManager.Instance.OnCardFlipBack(this);
        EnableButton();
    }

    public void FlipBack()
    {
        _animator.SetTrigger("FlipBack");
    }

    public void DisableButton()
    {
        _button.interactable = false;
    }

    public void EnableButton()
    {
        _button.interactable = true;
    }
}
