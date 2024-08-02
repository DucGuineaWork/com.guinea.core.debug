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
        [SerializeField]TMP_InputField m_fixedDeltaTime;
        [SerializeField]RangeToggleSlider m_rangeToggleSliderPrefab;
        [SerializeField]Transform m_rangeToggleSliderContainer;
        private int m_targetFrameRateIndex = 0;

        private static int[] s_fpsMapper = new int[] { -1 , 30, 60, 90, 120};

        void Start()
        {
            m_qualityDropdown.options = BuildOptionData<Quality>();
            m_fpsDropDown.options = BuildOptionData<FPSDropDown>();
            FindAndCreateToggleSlider();
        }

        void OnEnable()
        {
            m_qualityDropdown.value = QualitySettings.GetQualityLevel();
            m_fpsDropDown.value = m_targetFrameRateIndex;
            m_fixedDeltaTime.text = $"{Time.fixedDeltaTime}";
        }

        private void FindAndCreateToggleSlider()
        {
            RangeToggle[] rangeToggles = FindObjectsOfType<RangeToggle>(true);
            foreach (RangeToggle rangeToggle in rangeToggles)
            {
                RangeToggleSlider instance = Instantiate(m_rangeToggleSliderPrefab, m_rangeToggleSliderContainer);
                instance.Initialize(rangeToggle);
            }
        }

        public void OnFixedDeltaTimeChanged(string value)
        {
            Time.fixedDeltaTime = float.Parse(m_fixedDeltaTime.text);
            UnityEngine.Debug.Log(Time.fixedDeltaTime);
        }

        public void OnQualityDropDownValueChanged(int value)
        {
            QualitySettings.SetQualityLevel(m_qualityDropdown.value, true);
        }

        public void OnFPSDropDownValueChanged(int value)
        {
            bool isValidIndex =  m_fpsDropDown.value >=0 &&m_fpsDropDown.value< s_fpsMapper.Length;
            Application.targetFrameRate = isValidIndex ? s_fpsMapper[m_fpsDropDown.value] : -1;
            m_targetFrameRateIndex = isValidIndex ? value: 0;
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
