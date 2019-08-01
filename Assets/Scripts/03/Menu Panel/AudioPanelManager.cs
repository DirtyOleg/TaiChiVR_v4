namespace TaiChiVR.Simulation.MenuPanel
{
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using TaiChiVR.Utility;
    using System.Collections;

    public class AudioPanelManager : MonoBehaviour
    {
        [SerializeField] Toggle muteToggle = null;
        [SerializeField] Slider volSlider = null;
        [SerializeField] TextMeshProUGUI volValueText = null;
        [SerializeField] GameObject muteImageObj = null;

        private bool isMuting = false;
        private float volValue = 0;

        void Start()
        {
            if (SharedInfo.Instance.isTest)
            {
                StartCoroutine(WaitOneSecond());
            }
            else
            {
                muteToggle.onValueChanged.AddListener(OnToggleChanged);
                volSlider.onValueChanged.AddListener(OnSliderChanged);

                volSlider.value = AudioListManager.Instance.GetVolume();
                volValueText.text = GetRoundVolValue() + "%";
            }
        }

        IEnumerator WaitOneSecond()
        {
            yield return new WaitForSeconds(1f);

            muteToggle.onValueChanged.AddListener(OnToggleChanged);
            volSlider.onValueChanged.AddListener(OnSliderChanged);

            volSlider.value = AudioListManager.Instance.GetVolume();
            volValueText.text = GetRoundVolValue() + "%";
        }

        private void OnToggleChanged(bool isOn)
        {
            if (isOn)
            {
                //Mute
                Mute();
            }
            else
            {
                //UnMute
                UnMute();
            }
        }

        private void OnSliderChanged(float value)
        {
            if (value == 0)
            {
                //Mute
                Mute();
                muteToggle.isOn = true;
            }
            else
            {
                //UnMute
                if (isMuting)
                {
                    UnMute();
                    muteToggle.isOn = false;
                }
            }

            AudioListManager.Instance.SetVolume(value);

            if (!isMuting)
            {
                volValueText.text = GetRoundVolValue() + "%";
            }
        }

        private void Mute()
        {
            isMuting = true;
            volValueText.gameObject.SetActive(false);
            muteImageObj.SetActive(true);
            volValue = AudioListManager.Instance.GetVolume();
            AudioListManager.Instance.SetVolume(0);
        }

        private void UnMute()
        {
            isMuting = false;
            muteImageObj.SetActive(false);
            volValueText.gameObject.SetActive(true);
            AudioListManager.Instance.SetVolume(volValue);
        }

        private int GetRoundVolValue()
        {
            return (int)(AudioListManager.Instance.GetVolume() * 100);
        }
    }
}