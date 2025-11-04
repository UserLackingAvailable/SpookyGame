using UnityEngine;

public class SIDCardItem : SBaseItem
{
    public override string GetInteractionText(SPlayer player)
    {
        return "ID CARD\nPress 'E' to Grab";
    }
}
