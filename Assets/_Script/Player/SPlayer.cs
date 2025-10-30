using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class SPlayer : MonoBehaviour
{
    public static SPlayer Instance { get; private set; }

    public class OnSelectedItemChangedEventArgs : EventArgs { public SBaseItem mSelectedItem; }
    public event EventHandler<OnSelectedItemChangedEventArgs> OnSelectedItemChanged;


    [SerializeField] private SGameInput mGameInput;
    [SerializeField] private float mspeed = 5f;
    [SerializeField] private float mAcceleration = 10f;
    [SerializeField] private float mInteractDistance = 4f;
    //[SerializeField] private float dashForce = 10f;
    //[SerializeField] private float dashCooldown = 1f;
    [SerializeField] private LayerMask mItemPickupMask;

    //private float lastDashTime = -Mathf.Infinity;

    private Rigidbody mRigidbody;
    private PlayerInput mPlayerInput;
    private SFirstPersonCam firstPersonCam;

    private SBaseItem mSelectedItem;
    private SPickupItem mPickupController;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;

    }

    private void Start()

    {

        mRigidbody = GetComponent<Rigidbody>();
        mPlayerInput = GetComponent<PlayerInput>();
        firstPersonCam = GetComponent<SFirstPersonCam>();


        mGameInput = SGameInput.Instance;
        if (mGameInput == null)
        {
            Debug.LogError("GameInput Script not found!");
            return;
        }

        mGameInput.OnInteractAction += GameInput_OnInteractAction;
        mGameInput.OnAttackAction += GameInput_OnAttackAction;
        //mGameInput.OnDashAction += GameInput_OnSprintAction;
        mGameInput.OnFlashlightAction += GameInput_OnFlashlightAction;


    }

    //private void GameInput_OnSprintAction(object sender, EventArgs e)
    //{
    //    Sprint();
    //}

    //private void Sprint()
    //{
    //    //if (Time.time < lastDashTime + dashCooldown) return;

    //    Vector2 inputVector = mGameInput.GetMovementVectorNormalized();
    //    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

    //    if (moveDir.sqrMagnitude < 0.01f) return; // no dash if not moving

    //    //lastDashTime = Time.time;

    //    mRigidbody.AddForce(moveDir * dashForce, ForceMode.Impulse);

    //    //ADD LIMIT
    //}

    private void GameInput_OnAttackAction(object sender, System.EventArgs e)
    {
       if (mSelectedItem != null)
        {
            mSelectedItem.Attack(this);
        }
    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (mSelectedItem != null)
        {
            mSelectedItem.Interact(this);
        }
    }

    private void GameInput_OnFlashlightAction(object sender, System.EventArgs e)
    {
        //call flashlight script
        Debug.Log("Trying to call Flashlight script");
    }




    private void FixedUpdate()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector3 cameraPosition = firstPersonCam.PlayerCamera.transform.position;
        Vector3 cameraForward = firstPersonCam.PlayerCamera.transform.forward;

        if (Physics.Raycast(cameraPosition, cameraForward, out RaycastHit hit, mInteractDistance, mItemPickupMask))
        {
            Debug.Log("Hitting Item");
            Debug.DrawRay(cameraPosition, cameraForward * mInteractDistance, Color.green);

            if (hit.transform.TryGetComponent(out SBaseItem baseItem))
            {
                // Perform interaction logic if the item is different from the selected item
                if (baseItem != mSelectedItem)
                {
                    SetSelectedItem(baseItem);
                }
            }
            else
            {
                SetSelectedItem(null);
            }
        }
        else
        {
            SetSelectedItem(null);
            Debug.DrawRay(cameraPosition, cameraForward * mInteractDistance, Color.red);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = mGameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        // Move relative to player's facing direction
        moveDir = transform.TransformDirection(moveDir);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Vector3 desiredVelocity = moveDir * mspeed;
            Vector3 velocityChange = desiredVelocity - mRigidbody.linearVelocity;
            velocityChange.y = 0f;

            mRigidbody.AddForce(velocityChange * mAcceleration, ForceMode.Acceleration);
        }
        else
        {
            Vector3 deceleration = -mRigidbody.linearVelocity * mAcceleration * Time.fixedDeltaTime;
            deceleration.y = 0f;
            mRigidbody.AddForce(deceleration, ForceMode.VelocityChange);
        }
    }
    
    private void SetSelectedItem(SBaseItem mSelectedItem)
    {
        this.mSelectedItem = mSelectedItem;
        OnSelectedItemChanged?.Invoke(this, new OnSelectedItemChangedEventArgs { mSelectedItem = mSelectedItem });
    }

    private void OnDestroy()
    {
        if (mGameInput == null) return;

        mGameInput.OnInteractAction -= GameInput_OnInteractAction;
        mGameInput.OnAttackAction -= GameInput_OnAttackAction;
        //mGameInput.OnDashAction -= GameInput_OnSprintAction;
         mGameInput.OnFlashlightAction -= GameInput_OnFlashlightAction;
    }

    

}
