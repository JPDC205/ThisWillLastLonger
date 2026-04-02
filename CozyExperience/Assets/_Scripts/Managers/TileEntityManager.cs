using UnityEngine;

public class TileEntityManager : MonoBehaviour
{
    public static TileEntityManager Instance { get; private set; }

    public GameObject GroundPile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
