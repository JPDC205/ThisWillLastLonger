using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTile : Tile
{
    public GroundTile(TileBase tileBase, Vector2 position, MapManager mapManager) : base(tileBase, position, mapManager)
    {
        SetTileType(TileType.Ground);
    }

    public override void Interact()
    {
        base.Interact();
        Vector2 worldPosition = GetWorldPosition();
        var newGroundPile = GameObject.Instantiate(TileEntityManager.Instance.GroundPile, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);

        if (newGroundPile.TryGetComponent(out PlowedLandEntity groundPileComponent))
        {
            groundPileComponent.Initialize(this);
            this.SetTileEntity(groundPileComponent);
        }
    }
}
