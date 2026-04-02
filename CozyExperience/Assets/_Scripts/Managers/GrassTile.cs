using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassTile : Tile
{
    public GrassTile(TileBase tileBase, Vector2 position, MapManager mapManager) : base(tileBase, position, mapManager)
    {
        SetTileType(TileType.Grass);
    }

    public override void Interact()
    {
        base.Interact();
    }
}
