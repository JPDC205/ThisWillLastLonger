using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Tile : I_Interactable
{
    [SerializeField] private TileBase tileBase;
    [SerializeField] private Vector2 position;
    [SerializeField] private TileType type;
    private MapManager mapManager;
    private TileEntity tileEntity;

    public Tile(TileBase tileBase, Vector2 position, MapManager mapManager)
    {
        this.tileBase = tileBase;
        this.position = position;
        this.mapManager = mapManager;
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
        return tileEntity != null;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public Vector2 GetWorldPosition()
    {
        return mapManager.TileToWorldPosition(position);
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with tile at position: " + position);
        if (IsOccupied())
        {
            tileEntity.Interact();
            return;
        }
        Debug.Log("No tile entity found on tile at position: " + position);
    }

    public void SetColor(Color color)
    {
        mapManager.SetTileColor(position, color);
    }

    public void SetTileEntity(TileEntity entity)
    {
        tileEntity = entity;
    }
    
    public TileEntity GetTileEntity()
    {
        return tileEntity;
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
