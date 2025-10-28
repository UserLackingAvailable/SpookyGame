using UnityEngine;

public class SSelectedItemVisual : MonoBehaviour
{
    [SerializeField] private SBaseItem mBaseItem;
    [SerializeField] private GameObject[] mVisualGameObjectArray;

    private void Start()
    {
        SPlayer.Instance.OnSelectedItemChanged += Player_OnSelectedItemChanged;

    }

    private void Player_OnSelectedItemChanged(object sender, SPlayer.OnSelectedItemChangedEventArgs e)
    {
        if (e.mSelectedItem == mBaseItem)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in mVisualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in mVisualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
