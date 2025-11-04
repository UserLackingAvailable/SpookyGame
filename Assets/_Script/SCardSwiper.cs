using UnityEngine;

public class SCardSwiper : SBaseItem
{
    [SerializeField] private Transform doorExit;
    public override string GetInteractionText(SPlayer player)
    {

        SPickupController pickup = player.GetPickupController();

        if (pickup != null && pickup.IsHoldingObject() &&
            pickup.GetHeldObject().GetComponent<SIDCardItem>() != null)
        {
            return "Press E to SWIPE ID CARD";
        }

        return "Need ID CARD";
    }

    public override void Interact(SPlayer player)
    {
        SPickupController pickup = player.GetPickupController();

        if (pickup != null && pickup.IsHoldingObject())
        {
            GameObject heldGameobject = pickup.GetHeldObject();

            if (heldGameobject.GetComponent<SIDCardItem>() != null)
            {

                // Teleport the player
                if (doorExit != null)
                {
                    player.transform.position = doorExit.position;
                    player.transform.rotation = doorExit.rotation;
                }
            }
        }
    }
}
