using UnityEngine;

public class SFieldOfView : MonoBehaviour
{
    [SerializeField] float mEyeHeight = 3f;
    [SerializeField] float mFarSightDistance = 10f;
    [SerializeField] float mFarViewAngle = 30f;

    [SerializeField] float mNearSightDistance = 3f;
    [SerializeField] float mNearViewAngle = 90f;

    public float EyeHeight => mEyeHeight;
    public float FarSightDistance => mFarSightDistance;
    public float FarViewAngle => mFarViewAngle;
    public float NearSightDistance => mNearSightDistance;
    public float NearViewAngle => mNearViewAngle;

    void OnDrawGizmos()
    {
        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyeViewPoint, mFarSightDistance);
        Vector3 farLeft = Quaternion.AngleAxis(mFarViewAngle, Vector3.up) * transform.forward;
        Vector3 farRight = Quaternion.AngleAxis(-mFarViewAngle, Vector3.up) * transform.forward;
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + farLeft * mFarSightDistance);
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + farRight * mFarSightDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(eyeViewPoint, mNearSightDistance);
        Vector3 nearLeft = Quaternion.AngleAxis(mNearViewAngle, Vector3.up) * transform.forward;
        Vector3 nearRight = Quaternion.AngleAxis(-mNearViewAngle, Vector3.up) * transform.forward;
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + nearLeft * mNearSightDistance);
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + nearRight * mNearSightDistance);


    }
}
