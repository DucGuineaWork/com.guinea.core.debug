using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Guinea.Core.Debug
{
    public class Setting : MonoBehaviour
    {
        [SerializeField]TMP_Dropdown m_qualityDropdown;

        void Start()
        {
            m_qualityDropdown.options = BuildOptionData<Quality>();
            m_qualityDropdown.value = QualitySettings.GetQualityLevel();
        }

        public void OnQualityDropDownValueChanged(int value)
        {
            QualitySettings.SetQualityLevel(value, true);
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public static List<TMP_Dropdown.OptionData> BuildOptionData<TEnum>()
        {
            List<TMP_Dropdown.OptionData> options = new();
            foreach(var option in Enum.GetNames(typeof(TEnum)))
            {
                options.Add(new TMP_Dropdown.OptionData(option));
            }
            return options;
        }

       public enum Quality
       {
            Low = 0,
            Medium,
            High
       }
    }
}
