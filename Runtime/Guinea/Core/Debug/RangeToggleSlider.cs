using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RangeToggleSlider : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI m_label;
    [SerializeField]Slider m_slider;

    private RangeToggle m_rangeToggle;

    public void Initialize(RangeToggle rangeToggle)
    {
        m_rangeToggle = rangeToggle;
        m_label.text = m_rangeToggle.gameObject.name;
        m_slider.maxValue = m_rangeToggle.transform.childCount;
        int activeCount = 0;
        foreach(Transform child in rangeToggle.transform)
        {
            if(child.gameObject.activeSelf)
            {
                activeCount++;
            }
        }
        m_slider.value = activeCount;
    }
    
    public void OnValueChanged(int value)
    {
        m_rangeToggle?.ActiveChildrenInRange((int)m_slider.value);
    }
}
