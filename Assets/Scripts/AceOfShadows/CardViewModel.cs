using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

public class CardViewModel
{
    private readonly List<CardModel> _cards = new List<CardModel>();

    private const int TOTAL_CARDS = 144;
    private const float MOVE_DURATION = 2f;

    private int _movementCount = 0;
    private float _yOffset = -3.0f;

    [Inject]
    public CardViewModel()
    {
        // Initialize 144 cards with default positions and visibility
        InitializeCards();
    }

    private void InitializeCards()
    {
        for (int i = 0; i < TOTAL_CARDS; i++)
        {
            _cards.Add(new CardModel(i, new Vector3(0, i * _yOffset, 0))); // Initial position stacked on top of each other
        }
    }

    public void Update(Transform destination, GameObject card)
    {
        MoveTopCardToNewStack( destination,card);
    }

    private void MoveTopCardToNewStack(Transform destination, GameObject topCard)
    {
        if (topCard != null)
        {
            var y = (_movementCount * _yOffset) + destination.position.y;
            var pos = new Vector3(destination.position.x, y, 0);
            topCard.transform.SetAsLastSibling();
            topCard.transform.DOMove(pos, MOVE_DURATION);
        }
        _movementCount++;
    }

    public List<CardModel> GetCards()
    {
        return _cards.ToList();
    }


}
