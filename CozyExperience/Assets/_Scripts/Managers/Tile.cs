using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Tile
{
    [SerializeField] private TileBase tileBase;
    [SerializeField] private Vector2 position;
    [SerializeField] private TileType type;
    private MapManager mapManager;
    private bool isOccupied;

    public Tile(TileBase tileBase, Vector2 position, MapManager mapManager)
    {
        this.tileBase = tileBase;
        this.position = position;
        this.mapManager = mapManager;
        isOccupied = false;
    }

    public TileType GetTileType()
    {
        return type;
    }

    public void SetTileType(TileType newType)
    {
        type = newType;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public abstract void Interact();

    public void SetColor(Color color)
    {
        mapManager.SetTileColor(position, color);
    }

}

public enum TileType
{
    Grass,
    Water,
    Ground,
    None
    // Add more tile types as needed
}
