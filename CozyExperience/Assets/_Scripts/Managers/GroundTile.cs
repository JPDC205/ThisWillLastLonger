using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTile : Tile
{
    public GroundTile(TileBase tileBase, Vector2 position, MapManager mapManager) : base(tileBase, position, mapManager)
    {
        SetTileType(TileType.Ground);
        Debug.Log("Created Ground Tile at position: " + position);
    }

    public override void Interact()
    {
        Debug.Log("Interacting with Ground Tile at position: " + GetPosition());
        if (!IsOccupied())
        {
            Debug.Log("Ground Tile is not occupied. Performing interaction.");
            SetColor(Color.green); // Example interaction: change color to red
                                     // Additional interaction logic can be added here
        }
    }
}
