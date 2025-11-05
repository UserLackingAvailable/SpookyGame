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
    [SerializeField] private LayerMask mItemPickupMask;
    [SerializeField] private SFlashlight mFlashlight;

    [SerializeField] private SPickupController mPickupController;

    private Rigidbody mRigidbody;
    private PlayerInput mPlayerInput;
    private SFirstPersonCam firstPersonCam;


    private bool isHidden = false;
    public bool IsHidden => isHidden;

    private SBaseItem mSelectedItem;
    private SInteractionUI interactionUI;
    


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
        interactionUI = FindFirstObjectByType<SInteractionUI>();


        mGameInput = SGameInput.Instance;
        if (mGameInput == null)
        {
            Debug.LogError("GameInput Script not found!");
            return;
        }

        mGameInput.OnInteractAction += GameInput_OnInteractAction;
        mGameInput.OnAttackAction += GameInput_OnAttackAction;
        mGameInput.OnDropAction += GameInput_OnDropAction;
        mGameInput.OnFlashlightAction += GameInput_OnFlashlightAction;

        if (firstPersonCam.PlayerCamera != null)
        {
            mFlashlight = firstPersonCam.PlayerCamera.GetComponentInChildren<SFlashlight>();
            if (mFlashlight == null)
                Debug.LogError("Flashlight not found under FirstPersonCamera!");
        
            mPickupController = firstPersonCam.PlayerCamera.GetComponent<SPickupController>();
                if (mPickupController == null)
                    Debug.Log("PickupController not found on FirstPersonCamera!");
            
        }

    }
    private void GameInput_OnAttackAction(object sender, System.EventArgs e)
    {
        SPickupController pickup = GetPickupController();
        if (pickup != null && pickup.IsHoldingObject())
        {
            pickup.ThrowObject();
        }
    }


    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (mSelectedItem != null)
        {
            mSelectedItem.Interact(this);
            interactionUI?.Hide();
            SetSelectedItem(null);
        }
    }



    private void GameInput_OnDropAction(object sender, System.EventArgs e)
{
    SPickupController pickup = GetPickupController();
    if (pickup != null && pickup.IsHoldingObject())
    {
        pickup.DropObject();
    }
}


    private void GameInput_OnFlashlightAction(object sender, System.EventArgs e)
    {
        SAudioManager audioManager = FindAnyObjectByType<SAudioManager>();
        audioManager.Play("Flashlight");
        mFlashlight.ToggleFlashlight();
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

            if (mPickupController != null && mPickupController.IsHoldingObject() && hit.transform.gameObject == mPickupController.GetHeldObject())
            {
                interactionUI?.Hide();
                SetSelectedItem(null);
                return;
            }

            if (hit.transform.TryGetComponent(out SBaseItem baseItem))
            {
                string message = baseItem.GetInteractionText(this);

                if (baseItem != mSelectedItem)
                {
                    SetSelectedItem(baseItem);
                    interactionUI?.Show(message);
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
            interactionUI?.Hide();
            Debug.DrawRay(cameraPosition, cameraForward * mInteractDistance, Color.red);
        }
    }

    private void HandleMovement()
    {
        if (isHidden) return;

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

    public void AssignPickupController(SPickupController controller)
    {
        mPickupController = controller;
    }

    public SPickupController GetPickupController()
    {
        return mPickupController;
    }

    public void SetFlashLight(SFlashlight flashlight)
    {
        mFlashlight = flashlight;
    }

    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
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
        mGameInput.OnDropAction -= GameInput_OnDropAction;
         mGameInput.OnFlashlightAction -= GameInput_OnFlashlightAction;
    }

}
