using System;
using UnityEngine;
using UnityEngine.Pool;

public class CardStack : MonoBehaviour
{
    IObjectPool<SpriteRenderer> _cardPool;
    [SerializeField] BottomCardFacade bottomCardFacade;
    [SerializeField] uint startingAmount = 144;

    SpriteRenderer _topCard;
    internal uint CardAmount;

    internal Vector3 TopCardPosition
    {
        get
        {
            var result = transform.position;
            result.y += bottomCardFacade.transform.localScale.y;
            return result;
        }
    }

    public void InjectPool(IObjectPool<SpriteRenderer> cardPool)
    {
        _cardPool = cardPool;
    }

    void Awake()
    {
        CardAmount = startingAmount;
    }

    void Start()
    {
        bottomCardFacade.RenderAmount(CardAmount);
        if (startingAmount > 1)
            CreateNewTopCard();
    }

    void CreateNewTopCard()
    {
        _topCard = _cardPool.Get();
        _topCard.transform.parent = transform;
        _topCard.transform.position = TopCardPosition;
    }

    internal SpriteRenderer PopCard()
    {
        if (_topCard == null)
            throw new NullReferenceException("There's no card to pop");

        var result = _topCard;
        result.sortingOrder = (int)(startingAmount - CardAmount);

        CardAmount--;

        CreateNewTopCard();

        bottomCardFacade.RenderAmount(CardAmount - 1);

        return result;
    }

    internal void PushCard(SpriteRenderer card)
    {
        if (_topCard != null)
            _cardPool.Release(_topCard);

        _topCard = card;
        CardAmount++;
        bottomCardFacade.RenderAmount(CardAmount - 1);
    }
}