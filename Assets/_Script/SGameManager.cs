using UnityEngine;
using UnityEngine.UI;

public class SGameManager : MonoBehaviour
{
    public static SGameManager Instance;

    public Text gameOverText; 
    public Text winText; 

    private bool isGameOver = false;
    private bool isGameWon = false;
    private bool hasCollectedAllItems = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!hasCollectedAllItems)
        {
            CheckItemsCollected();
        }

    }

    private void CheckItemsCollected()
    {
        GameObject[] collectItems = GameObject.FindGameObjectsWithTag("Collect");
        hasCollectedAllItems = true;

        foreach (GameObject item in collectItems)
        {
            if (item != null) 
            {
                hasCollectedAllItems = false; 
                break;
            }
        }

        if (hasCollectedAllItems)
        {
            // Display code input UI
            winText.gameObject.SetActive(false);
        }
    }


    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverText.gameObject.SetActive(true);
            winText.gameObject.SetActive(false);
        }
    }

    private void WinGame()
    {
        if (!isGameWon)
        {
            isGameWon = true;
            winText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(false);
        }
    }
}
