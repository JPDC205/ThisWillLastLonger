using UnityEngine;

public class PlowedLandEntity : TileEntity
{
    public override void Interact()
    {
        Debug.Log("Interacted with Plowed Land Entity at position: " + transform.position);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
