using UnityEngine;

public class SBatteryItem : SBaseItem
{
    [SerializeField] private float batteryAmountGiven = 25f; 

    public override void Interact(SPlayer player)
    {
        SFlashlight flashlight = player.GetComponent<SFlashlight>();
        if (flashlight != null)
        {
            flashlight.AddBattery(batteryAmountGiven); // actually add the battery
            Debug.Log($"Added {batteryAmountGiven} battery to flashlight!");
        }
        else
        {
            Debug.LogWarning("Player has no flashlight to charge!");
        }

        Destroy(gameObject); // remove the battery from the world
    }

    public override string GetInteractionText(SPlayer player)
    {
        return "BATTERY \nPress 'E' to use";
    }
}
