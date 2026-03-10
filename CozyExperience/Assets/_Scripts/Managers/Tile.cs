using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile
{
    [SerializeField] private TileBase tileBase;
    [SerializeField] private Vector2 position;
    [SerializeField] private TileType type;
    private MapManager mapManager;

    public Tile(TileBase tileBase, Vector2 position, TileType type, MapManager mapManager)
    {
        this.tileBase = tileBase;
        this.position = position;
        this.type = type;
        this.mapManager = mapManager;
    }

    public TileType GetTileType()
    {
        return type;
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
