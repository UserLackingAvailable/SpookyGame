using UnityEngine;

public class SGameMode : MonoBehaviour
{
    
    [SerializeField] private SPlayer mPlayerPrefab;


    //read only access to player variable
    public SPlayer mPlayer => mPlayerPrefab;

    public static SGameMode MainGameMode { get; private set; }

    private void Awake()
    {
        // Singleton enforcement
        if (MainGameMode != null)
        {
            Destroy(gameObject);
            
        }

        MainGameMode = this;
        SpawnPlayerAtStart();
    }

    private void SpawnPlayerAtStart()
    {
        //var infers to variable's type based on value given
        var playerStart = FindAnyObjectByType<SPlayerStart>();
        if (playerStart == null)
        {
            Debug.LogError("SPlayerStart not found in the scene!");
            return;
        }

        if (mPlayerPrefab == null)
        {
            Debug.LogError("Player prefab not assigned in SGameMode.");
            return;
        }

        Instantiate(mPlayerPrefab, playerStart.transform.position, playerStart.transform.rotation);
    }
}


