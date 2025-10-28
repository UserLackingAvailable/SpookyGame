using System;
using UnityEngine;

public class SEnemyAI : MonoBehaviour
{
    GameObject mTarget;
    GameObject Target { get { return mTarget; } set { mTarget = value; } }

    [SerializeField] float mEyeHeight = 3f;
    [SerializeField] float mSightDistance = 5f;
    [SerializeField] float mViewAngle = 30f;

    private void Update()
    {
        UpdatePlayerPerception();
    }

    private void UpdatePlayerPerception()
    {
        SPlayer player = SGameMode.MainGameMode.mPlayer;
        if (!player)
        {
            
            Target = null;
            return;
        }


        if (Vector3.Distance(player.transform.position, transform.position) > mSightDistance)
        {
            
            Target = null;
            return;
        }

        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        if (Vector3.Angle(playerDir, transform.forward) > mViewAngle)
        {
            
            Target = null;
            return;
        }

        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;
        if (Physics.Raycast(eyeViewPoint, playerDir, out RaycastHit hitInfo, mSightDistance))
        {
            if (hitInfo.collider.gameObject != player)
            {
                Debug.Log("Object Blocking player");
                Target = null;
                return;
            }
        }

        Target = player.gameObject;
    }

    void OnDrawGizmos()
    {
        Vector3 eyeViewPoint = transform.position + Vector3.up * mEyeHeight;
        Gizmos.DrawWireSphere(eyeViewPoint, mSightDistance);

        Vector3 leftLineDir = Quaternion.AngleAxis(mViewAngle, Vector3.up) * transform.forward;
        Vector3 rightLineDir = Quaternion.AngleAxis(-mViewAngle, Vector3.up) * transform.forward;

        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + leftLineDir * mSightDistance);
        Gizmos.DrawLine(eyeViewPoint, eyeViewPoint + rightLineDir * mSightDistance);


    }
}
