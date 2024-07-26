using UnityEngine;

public class RangeToggle : MonoBehaviour
{
    public void ActiveChildrenInRange(int value)
    {
        for(int i=0; i<transform.childCount;i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(i<value);
        }
    }
}
