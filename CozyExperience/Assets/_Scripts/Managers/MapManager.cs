using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
    [SerializeField] List<TileBase> grassTiles; // Reference to the grass tile asset
    [SerializeField] List<TileBase> groundTiles; // Reference to the water tile asset

    public static MapManager _instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        setUpTileMap();
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log($"Tile Count: {tiles.Count}");
        Debug.Log($"Grass Tile Count: {tiles.Where(t => t.Value.GetTileType() == TileType.Grass).Count()}");
        Debug.Log($"Ground Tile Count: {tiles.Where(t => t.Value.GetTileType() == TileType.Ground).Count()}");
    */
        }


    private void setUpTileMap()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tileBase = tilemap.GetTile(tilePosition);

                if (tileBase != null) // Only process painted tiles
                {
                    Tile newTile;
                    Vector2 key = new Vector2(x, y);
                    if (grassTiles.Contains(tileBase))
                    {
                        newTile = new Tile(tileBase, key, TileType.Grass, this);
                    }
                    else if (groundTiles.Contains(tileBase))
                    {
                        newTile = new Tile(tileBase, key, TileType.Ground, this);
                    }
                    else
                    {
                        newTile = new Tile(tileBase, key, TileType.None, this);
                    }
                    tiles.Add(key, newTile);

                }
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        }
        return null; // Return null if no tile exists at the given position
    }

}
