using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private Transform cardParent;
    [SerializeField] private Transform cardDestination;
    private const float MOVE_INTERVAL = 1f;

    private CardViewModel _viewModel;
    private readonly Stack<GameObject> _cards = new Stack<GameObject>();

    [Inject]
    public void Constructor(CardViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    void Start()
    {
        CreateCards();
        InvokeRepeating("MoveCard", 1.0f, MOVE_INTERVAL);
    }

    void MoveCard()
    {
        _viewModel.Update(cardDestination, _cards.Pop());
    }

    private void CreateCards()
    {
        foreach (var card in _viewModel.GetCards())
        {
            var cardInstance = Instantiate(cardPrefab, cardParent);
            cardInstance.GetComponent<RectTransform>().anchoredPosition = card.Position;
            _cards.Push(cardInstance);
        }
    }
    void OnDestroy()
    {
        DOTween.KillAll(); 
    }
}
