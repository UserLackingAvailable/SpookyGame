using UnityEngine;

public class SPickupItem : SBaseItem
{
    [SerializeField] Transform mHoldArea;
    [SerializeField] GameObject mHoldObject;
    [SerializeField] Rigidbody mHoldObjectRigidbody;

    [SerializeField] private float mPickupForce = 150.0f;

    private void Update()
    {
        if (mHoldObject != null)
        {
            MoveObject();
        }
    }

       public override void Interact(SPlayer player)
    {
        // Only pick up the item if the player isn't already holding something
        if (mHoldObject == null)
        {
            PickupObject(gameObject);  // Pick up the object
        }
    }

    // Override Attack to drop the item
    public override void Attack(SPlayer player)
    {
        if (mHoldObject != null)
        {
            DropObject();  
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(mHoldObject.transform.position, mHoldArea.position) > 0.1f)
        {
            Vector3 moveDirection = (mHoldArea.position -  mHoldObject.transform.position);
            mHoldObjectRigidbody.AddForce(moveDirection * mPickupForce);
        }
    }

    void PickupObject(GameObject pickObject)
    {
        if (pickObject.GetComponent<Rigidbody>())
        {
            mHoldObjectRigidbody = pickObject.GetComponent<Rigidbody>();
            mHoldObjectRigidbody.useGravity = false;
            mHoldObjectRigidbody.linearDamping = 10;
            mHoldObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            mHoldObjectRigidbody.transform.parent = mHoldArea;
            mHoldObject = pickObject;
        }
    }
    void DropObject()
    {

        mHoldObjectRigidbody.useGravity = true;
        mHoldObjectRigidbody.linearDamping = 1;
        mHoldObjectRigidbody.constraints = RigidbodyConstraints.None;

        mHoldObjectRigidbody.transform.parent = null;
        mHoldObject = null;
    }
}

