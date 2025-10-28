using UnityEngine;

public class SFirstPersonCam : MonoBehaviour
{
    [SerializeField] private Camera cameraPrefab;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float pitchClamp = 89f;
    [SerializeField] private float cameraHeight = 3f;


    private Camera playerCamera;
    private float pitch;
    private SGameInput gameInput;

    public Camera PlayerCamera => playerCamera;

    private void Start()
    {
        if (cameraPrefab != null)
        {
            playerCamera = Instantiate(cameraPrefab, transform);
            playerCamera.transform.position = transform.position + new Vector3(0f, cameraHeight, 0f);
        }
        else
        {
            Debug.LogError("Camera prefab not assigned.");
        }

        gameInput = SGameInput.Instance;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (playerCamera == null || gameInput == null) return;

        // Skip look input if disabled (e.g., pause or UI)
        if (!gameInput.IsInputEnabled)
            return;

        Vector2 lookDelta = gameInput.GetLookDelta() * lookSensitivity * Time.deltaTime * 100f;
        // Multiply by deltaTime*100 to match the feel of Mouse.current.delta

        // Rotate player (yaw)
        transform.Rotate(Vector3.up * lookDelta.x);

        // Rotate camera (pitch)
        pitch -= lookDelta.y;
        pitch = Mathf.Clamp(pitch, -pitchClamp, pitchClamp);
        playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
    

}