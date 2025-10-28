using UnityEngine;

public class SBaseItem : MonoBehaviour
{
    public virtual void Interact(SPlayer player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void Attack(SPlayer player)
    {
        Debug.LogError("BaseCounter.Attack();");
    }
}
