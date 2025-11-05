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
            flashlight = FindFirstObjectByType<SFlashlight>();
        }

        if (batterySlider != null)
        {
            batterySlider.maxValue = 1f;
            batterySlider.value = flashlight.BatteryPercent;
        }
    }

    void Update()
    {
        if (flashlight == null || batterySlider == null)
            return;

        batterySlider.value = flashlight.BatteryPercent;
    }
}