using UnityEngine;

public class SGameMode : MonoBehaviour
{
    [SerializeField] SPlayer mPlayerGameObjectPrefab;
    SPlayer mPlayerGameObject;


    //read only access to player variable
    public SPlayer mPlayer => mPlayerGameObject;
    public static SGameMode MainGameMode;

     void OnDestroy()
    {
        if(MainGameMode == this)
        {
            MainGameMode = null;
        }
    }

    private void Awake()
    {
        // Singleton enforcement
        if (MainGameMode != null)
        {
            Destroy(gameObject);
        }

        MainGameMode = this;

        SPlayerStart playerStart = FindAnyObjectByType<SPlayerStart>();
        if (!playerStart)
        {
            throw new System.Exception("Need a player start in scene");
        }

        mPlayerGameObject = Instantiate(mPlayerGameObjectPrefab, playerStart.transform.position, playerStart.transform.rotation);
    }
}


