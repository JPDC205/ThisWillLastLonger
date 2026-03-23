using UnityEngine;

public abstract class TileEntity : MonoBehaviour
{
    private Tile rootTile { get; set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact();

    private void Initialize(Tile tile)
    {
        rootTile = tile;
    }
}
