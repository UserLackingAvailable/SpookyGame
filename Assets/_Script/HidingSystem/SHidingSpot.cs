using Unity.VisualScripting;
using UnityEngine;

public class SHidingSpot : SBaseItem, IHidingSpot
{
    [SerializeField] private Transform hideSpot;
    [SerializeField] private Transform exitSpot;
    private SPlayer mPlayer;

    public Transform HideLocation => hideSpot;
    public Transform ExitLocation => exitSpot;
    public bool IsOccupied => mPlayer != null;

    public void Hide(SPlayer player)
    {
        if (IsOccupied) return;

        mPlayer = player;
        player.SetHidden(true); 
         Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        player.transform.position = hideSpot.position;
        player.transform.rotation = hideSpot.rotation;
    }

    public void Unhide(SPlayer player)
    {
        if (mPlayer != player) return;

        player.SetHidden(false);
        player.transform.position = exitSpot.position;
        player.transform.rotation = exitSpot.rotation;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        mPlayer = null;
    }
    
    public override string GetInteractionText(SPlayer player)
    {
        if (IsOccupied && mPlayer == player)
        {
            return "Press E to EXIT";
        }

        if (!IsOccupied)
        {
            return "Press E to HIDE";
        }

        return base.GetInteractionText(player);
    }

    public override void Interact(SPlayer player)
    {
         if (player.IsHidden && mPlayer == player)
        {
            Unhide(player);
        }
        else if (!IsOccupied) //Add && if not seen by enemy 
        {
            Hide(player);
        }
    }
}

