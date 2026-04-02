using UnityEngine;

public class Player_AnimatorManager : MonoBehaviour
{
    private CharacterController playerController;
    private void Awake()
    {
        playerController = GetComponentInParent<CharacterController>();
        this.enabled = false;
    }
    
    void OnActionEnded()
    {
        playerController.FinishAction();
    }
}
