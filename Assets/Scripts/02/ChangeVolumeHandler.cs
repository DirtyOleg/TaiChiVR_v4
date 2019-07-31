namespace TaiChiVR.Customeize
{
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class ChangeVolumeHandler : MonoBehaviour
    {
        [SerializeField] Slider volumeSlider = null;

        void Start()
        {
            volumeSlider.onValueChanged.AddListener(OnSliderChanged);
            volumeSlider.value = AudioListManager.Instance.GetVolume();
        }

        private void OnSliderChanged(float value)
        {
            AudioListManager.Instance.SetVolume(value);
        }
    }
}