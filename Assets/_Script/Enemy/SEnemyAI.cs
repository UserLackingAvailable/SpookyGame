using Unity.AppUI.UI;
using Unity.Behavior;
using UnityEngine;

public class SEnemyAI : MonoBehaviour
{


    GameObject mTarget;
    GameObject Target
    {
        get { return mTarget; }
        set
        {
            if (Target == value)
                return;

             if (value == null)
             {
                 mBehaviorGraphAgent.BlackboardReference.SetVariableValue("HasLastSeenPosition", true);
                mBehaviorGraphAgent.BlackboardReference.SetVariableValue("TargetLastSeenPosition", mTarget.transform.position);
             }

            mTarget = value;
            mBehaviorGraphAgent.BlackboardReference.SetVariableValue("Target", mTarget);
        }
    }

    SFieldOfView mFieldOfView;
    BehaviorGraphAgent mBehaviorGraphAgent;
    private void Awake()
    {
        mFieldOfView = GetComponent<SFieldOfView>();
        mBehaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        UpdatePlayerPerception();
    }

    private void UpdatePlayerPerception()
    {
        SPlayer player = SGameMode.MainGameMode.mPlayer;
        if (!player || player.IsHidden)
        {
            Target = null;
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        Vector3 eyeViewPoint = transform.position + Vector3.up * mFieldOfView.EyeHeight;

        // nearcheck
        if (distanceToPlayer <= mFieldOfView.NearSightDistance)
        {
            if (Vector3.Angle(playerDir, transform.forward) > mFieldOfView.NearViewAngle)
            {
                Target = null;
                return;
            }
        }
        // far check
        else if (distanceToPlayer <= mFieldOfView.FarSightDistance)
        {
            if (Vector3.Angle(playerDir, transform.forward) > mFieldOfView.FarViewAngle)
            {
                Target = null;
                return;
            }
        }

        else
        {
            Target = null;
            return;
        }


        if (Physics.Raycast(eyeViewPoint, playerDir, out RaycastHit hit, distanceToPlayer))
        {
            if (hit.collider.gameObject != player.gameObject)
            {
                Target = null;
                return;
            }
        }


        Target = player.gameObject;
    }

    public void Jumpscare()
    {

    }
    
    public void Attack()
    {
    //    if (distanceToPlayer <= mFieldOfView.NearSightDistance)
    //     {
    //         //play animation
    //     }
    }
}