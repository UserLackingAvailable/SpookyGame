using UnityEngine;

public interface IHidingSpot
{
    Transform HideLocation { get; }
    Transform ExitLocation { get;}
    bool IsOccupied { get; }
    void Hide(SPlayer player);
    void Unhide(SPlayer player);
}
