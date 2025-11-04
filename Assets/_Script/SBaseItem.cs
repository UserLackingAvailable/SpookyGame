using UnityEngine;

public class SBaseItem : MonoBehaviour
{
    public virtual void Interact(SPlayer player)
    {
        Debug.Log("Player trying to pick item up");
        SPickupController controller = player.GetPickupController();

        if (controller != null && controller.GetHeldObject() == null)
        {
            controller.PickupObject(gameObject);
        }
    }
    public virtual void Attack(SPlayer player)
    {
        Debug.LogError("BaseItem.Attack();");
    }
    public virtual string GetInteractionText(SPlayer player)
    {
        return "Press E to Interact";
    }
}
