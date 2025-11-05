using UnityEngine;

public class SBatteryItem : SBaseItem
{

    public override void Interact(SPlayer player)
    {

        SFlashlight flashlight = player.GetComponentInChildren<SFlashlight>();
        if (flashlight != null)
        {
            flashlight.AddBattery();
        }
        else
        {
            Debug.LogWarning("Player does not have a flashlight!");
        }

        Destroy(gameObject); // Remove the battery item
    }

    public override string GetInteractionText(SPlayer player)
    {
        return "BATTERY \nPress 'E' to use";
    }
}
