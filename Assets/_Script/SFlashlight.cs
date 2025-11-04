using UnityEngine;

public class SFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject mLightOfFlashlight;

    [SerializeField] private float mMaxBattery = 100f;
    [SerializeField] private float mDrainRate = 1f; 

    private float mCurrentBattery;
    private bool mIsOn = true;

    public float BatteryPercent => mCurrentBattery / mMaxBattery;

    void Start()
    {
        mCurrentBattery = mMaxBattery;

        SPlayer player = SPlayer.Instance;
        if (player != null)
            player.SetFlashLight(this);

        Show(); 
    }

    void Update()
    {
        //  Only drain battery when flashlight is on
        if (mIsOn && mCurrentBattery > 0f)
        {
            DrainBattery();
            
        }
    }

    public void ToggleFlashlight()
    {
        // Prevent turning on if battery is empty
        if (!mIsOn && mCurrentBattery <= 0f)
        {
            Debug.Log("Flashlight battery is dead!");
            return;
        }

        mIsOn = !mIsOn;

        if (mIsOn)
            Show();
        else
            Hide();
    }

    private void DrainBattery()
    {
        mCurrentBattery -= mDrainRate * Time.deltaTime;

        if (mCurrentBattery <= 0f)
        {
            mCurrentBattery = 0f;
            mIsOn = false;
            Hide();
            Debug.Log("Flashlight battery depleted!");
        }
    }

    public void AddBattery(float amount)
    {
         mCurrentBattery = Mathf.Min(mCurrentBattery + amount, mMaxBattery);
         
     }

    private void Show()
    {
        if (mLightOfFlashlight != null)
            mLightOfFlashlight.SetActive(true);
    }

    private void Hide()
    {
        if (mLightOfFlashlight != null)
            mLightOfFlashlight.SetActive(false);
    }

}