using UnityEngine;
using TMPro;

public class SInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionGameObject;
    [SerializeField] private TextMeshProUGUI textInteractionUI;   

     private void Awake()
    {
        if (interactionGameObject != null)
            interactionGameObject.SetActive(false); 
    }

    public void Show(string message = "Press 'E' to Interact")
    {
        if (interactionGameObject != null && textInteractionUI != null)
        {
            interactionGameObject.SetActive(true);
            textInteractionUI.text = message;
        }
    }

    public void Hide()
    {
        if (interactionGameObject != null)
            interactionGameObject.SetActive(false);
    }
}
