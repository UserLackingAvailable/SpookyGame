using UnityEngine;

public interface IPickupController
{
    bool IsHoldingObject();
    void PickupObject(GameObject pickObject);
    void DropObject();

    GameObject GetHeldObject();
    SPickupController GetPickupController();
}
