using UnityEngine;

public class CardModel
{
    public int Id { get; set; }
    public Vector3 Position { get; set; }

    public CardModel(int id, Vector3 position)
    {
        Id = id;
        Position = position;
    }
}
