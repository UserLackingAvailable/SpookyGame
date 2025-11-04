using UnityEngine;
using UnityEngine.UI;

public class SBatteryUI : MonoBehaviour
{
    [SerializeField] private SFlashlight flashlight;
    [SerializeField] private Slider batterySlider;

    void Start()
    {
        // Auto-assign flashlight from player if not set
        if (flashlight == null && SPlayer.Instance != null)

        if (batterySlider != null)
            batterySlider.maxValue = 1f; // normalized to 0–1
    }

    void Update()
    {
        if (flashlight == null || batterySlider == null)
            return;

        batterySlider.value = Mathf.Clamp01(flashlight.BatteryPercent);
    }
}