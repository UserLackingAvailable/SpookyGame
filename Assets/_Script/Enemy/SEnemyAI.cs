using System;
using UnityEngine;
using UnityEngine.AI; 

public class SEnemyAI : MonoBehaviour
{
    GameObject Target { get {  return Target; } set { Target = value; } }

    private void Update()
    {
        UpdatePlayerPerception();
    }

    private void UpdatePlayerPerception()
    {
        SPlayer player = SGameMode.MainGameMode.mPlayer;
        if(!player)
        {
            Debug.Log("Target not found");
            Target = null;
            return;
        }


        //if (Vector3.Distance(player.transform.position, transform.position) > mSightDistance);
        //{
        //    Target = null;
        //    return;
        //}
    }
}
