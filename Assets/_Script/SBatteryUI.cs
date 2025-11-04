using UnityEngine;
using UnityEngine.UI;

public class SBatteryUI : MonoBehaviour
{
    [SerializeField] private SFlashlight flashlight;
    [SerializeField] private Slider batterySlider;

    void Start()
    {
        
        if (flashlight == null && SPlayer.Instance != null)
        {
            
        }

        if (batterySlider != null)
            batterySlider.maxValue = 1f; 
    }

    void Update()
    {
        if (flashlight == null || batterySlider == null)
            return;

        batterySlider.value = Mathf.Clamp01(flashlight.BatteryPercent);
    }
}