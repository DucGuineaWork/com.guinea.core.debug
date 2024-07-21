using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Guinea.Core.Debug
{
    public class Setting : MonoBehaviour
    {
        [SerializeField]TMP_Dropdown m_qualityDropdown;
        [SerializeField]TMP_Dropdown m_fpsDropDown;

        private static int[] s_fpsMapper = new int[] { -1 , 30, 60, 90, 120};

        void Start()
        {
            m_qualityDropdown.options = BuildOptionData<Quality>();
            m_fpsDropDown.options = BuildOptionData<FPSDropDown>();
        }

        void OnEnable()
        {
            m_qualityDropdown.value = QualitySettings.GetQualityLevel();
            m_fpsDropDown.value = 0;
        }

        public void OnQualityDropDownValueChanged(int value)
        {
            UnityEngine.Debug.Log(m_qualityDropdown.value);
            QualitySettings.SetQualityLevel(m_qualityDropdown.value, true);
        }

        public void OnFPSDropDownValueChanged(int value)
        {
            Application.targetFrameRate = m_fpsDropDown.value >=0 &&m_fpsDropDown.value< s_fpsMapper.Length ? s_fpsMapper[m_fpsDropDown.value] : -1, true);
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

       public enum FPSDropDown
       {
            Default = 0,
            FPS_30,
            FPS_60,
            FPS_90,
            FPS_120,
       }
    }
}
