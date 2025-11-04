using UnityEngine;

public class SPickupController : MonoBehaviour, IPickupController
{
    [SerializeField] private Transform mHoldArea;
    [SerializeField] private GameObject mHoldObject;
    [SerializeField] private Rigidbody mHoldObjectRigidbody;

    [SerializeField] private float mPickupForce = 150.0f;
    
    void Start()
    {
        SPlayer player = SPlayer.Instance;
        if (player != null)
            player.AssignPickupController(this);
    }

    private void Update()
    {
        if (mHoldObject != null)
        {
            if (mHoldObjectRigidbody == null)
            {
                mHoldObject = null; // object destroyed
                return;
            }
            MoveObject();
        }
    }


    private void MoveObject()
    {
        if(Vector3.Distance(mHoldObject.transform.position, mHoldArea.position) > 0.1f)
        {
            Vector3 moveDirection = (mHoldArea.position - mHoldObject.transform.position);
            mHoldObjectRigidbody.AddForce(moveDirection * mPickupForce);
        }
    }

    public void PickupObject(GameObject pickObject)
    {
        if (pickObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            mHoldObjectRigidbody = rb;
            mHoldObjectRigidbody.useGravity = false;
            mHoldObjectRigidbody.linearDamping = 10;
            mHoldObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            
            mHoldObjectRigidbody.transform.parent = mHoldArea;
            mHoldObject = pickObject;

            //ignore the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Collider[] playerColliders = player.GetComponentsInChildren<Collider>();
                Collider[] objectColliders = pickObject.GetComponentsInChildren<Collider>();

                foreach (var pc in playerColliders)
                {
                    foreach (var oc in objectColliders)
                    {
                        Physics.IgnoreCollision(pc, oc, true);
                    }
                }
            }
        }
    }

    public void DropObject()
    {

        mHoldObjectRigidbody.useGravity = true;
        mHoldObjectRigidbody.linearDamping = 1;
        mHoldObjectRigidbody.constraints = RigidbodyConstraints.None;

        mHoldObjectRigidbody.transform.parent = null;
        mHoldObject = null;
        mHoldObjectRigidbody = null;
    }

    public bool IsHoldingObject()
    {
        return mHoldObject != null;
    }

    public SPickupController GetPickupController()
    {
        return this;
    }

    public GameObject GetHeldObject()
    {
        return mHoldObject;
    }
}