using System;
using UnityEngine;
using UnityEngine.Pool;

public class AceOfShadows : MonoBehaviour
{
    [SerializeField] CardStack cardStackOne, cardStackTwo;
    [SerializeField] SpriteRenderer cardPrefab;

    IObjectPool<SpriteRenderer> _cardPool;

    const float AnimationInterval = 1f;
    const float AnimationDuration = 2f;
    float _lastAnimationTime;

    void Awake()
    {
        _cardPool = new LinkedPool<SpriteRenderer>(CreateCard, ActivateCard, ReleaseCard);
        cardStackOne.InjectPool(_cardPool);
        cardStackTwo.InjectPool(_cardPool);
    }

    SpriteRenderer CreateCard() => Instantiate(cardPrefab);
    void ActivateCard(SpriteRenderer obj) => obj.gameObject.SetActive(true);
    void ReleaseCard(SpriteRenderer obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
    }

    void Update()
    {
        if (Time.time - _lastAnimationTime < AnimationInterval)
            return;

        _lastAnimationTime = Time.time;
        Animate();
    }

    void Animate()
    {
        var card = cardStackOne.PopCard();

        card.transform.LeanMove(cardStackTwo.TopCardPosition, AnimationDuration)
            .setOnComplete(() => OnCardAnimationFinished(card));
    }

    void OnCardAnimationFinished(SpriteRenderer card)
    {
        cardStackTwo.PushCard(card);
    }
}