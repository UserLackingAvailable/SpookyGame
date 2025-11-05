using UnityEngine;
using System.Collections;

public class SJumpscare : MonoBehaviour
{
    [SerializeField] private GameObject player;
  


    private Camera playerCamera;
    private bool isJumpscaring = false;

    private void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerCamera = player.GetComponentInChildren<Camera>();
        }
    }

    public void TriggerJumpscare()
    {
        if (isJumpscaring) return;
        isJumpscaring = true;
    }


    private void DisablePlayerMovement()
    {
        
    }


    private void TriggerGameOver()
    {
        Debug.Log("Game Over! Restarting game...");

    }
}
