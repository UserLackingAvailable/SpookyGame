using UnityEngine;

public class SFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject mLightOfFlashlight;


    private void Show()
    {
        mLightOfFlashlight.SetActive(true);
    }
    
    private void Hide()
    {
        mLightOfFlashlight.SetActive(false);
        
    }
}
