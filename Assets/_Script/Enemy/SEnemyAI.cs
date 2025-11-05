using Unity.AppUI.UI;
using Unity.Behavior;
using UnityEngine;

using System.Collections;

public class SEnemyAI : MonoBehaviour
{
    private GameObject mTarget;
    public GameObject Target
    {
        get { return mTarget; }
        set
        {
            if (Target == value)
                return;

            if (value == null)
            {
                mBehaviorGraphAgent.BlackboardReference.SetVariableValue("HasLastSeenLocation", true);
                mBehaviorGraphAgent.BlackboardReference.SetVariableValue("TargetLastSeenPosition", mTarget.transform.position);
            }

            mTarget = value;
            mBehaviorGraphAgent.BlackboardReference.SetVariableValue("Target", mTarget);
        }
    }

    [SerializeField] private Transform sphereCollider;

    [SerializeField] private SGameManager gameManager;

    SFieldOfView mFieldOfView;
    BehaviorGraphAgent mBehaviorGraphAgent;

    private void Awake()
    {
        mFieldOfView = GetComponent<SFieldOfView>();
        mBehaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
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

        if (distanceToPlayer <= mFieldOfView.NearSightDistance)
        {
            if (Vector3.Angle(playerDir, transform.forward) > mFieldOfView.NearViewAngle)
            {
                Target = null;
                return;
            }
        }
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

        RotateTwardsPlayer(player.transform.position);
        Target = player.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Target = null;
            //could cause issues with Bgraph
        }
    }
    
    private void RotateTwardsPlayer(Vector3 playerPosition)
    {
        Vector3 dirToPlayer = (playerPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }




    public void Attack()
    {
        if (Target != null)
        {
            float distanceToPlayer = Vector3.Distance(Target.transform.position, transform.position);
            if (distanceToPlayer <= mFieldOfView.NearSightDistance)
            {


                SAudioManager audioManager = FindFirstObjectByType<SAudioManager>();

                audioManager.Play("EnemyBite");

                StartCoroutine(NotifyGameOverAfterDelay());
            }
        }
    }

    private IEnumerator NotifyGameOverAfterDelay()
    {
        
        yield return new WaitForSeconds(3f);

        
        if (gameManager != null)
        {
            gameManager.GameOver();  
        }
    }

        private void OnDrawGizmos()
    {
        if (mTarget != null)
        {
            
            GameObject player = mTarget;
            Vector3 playerPosition = player.transform.position;
            Vector3 enemyPosition = transform.position;

            
            Gizmos.color = Color.red; // Line color
            Gizmos.DrawLine(enemyPosition, playerPosition);

            
            Gizmos.color = Color.green; // Sphere color
            Gizmos.DrawSphere(playerPosition, 0.5f); 
        }
    }

}

