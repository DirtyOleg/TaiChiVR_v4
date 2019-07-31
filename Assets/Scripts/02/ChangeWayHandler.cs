namespace TaiChiVR.Customeize
{
    using UnityEngine;
    using TMPro;
    using TaiChiVR.Utility;
    using System.Collections;
    using TaiChiVR.Utility.WaysOfInteract;
    using UnityEngine.UI;

    public class ChangeWayHandler : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text = null;
        [SerializeField] Button changeBtn = null;

        void Start()
        {
            changeBtn.onClick.AddListener(() => ChangeIt());

            if (SharedFlag.Instance.isGaze)
            {
                text.text = "Controller";
            }
            else
            {
                text.text = "Gaze";
            }
        }

        private void ChangeIt()
        {
            StartCoroutine(ChangeItHandler());
        }

        IEnumerator ChangeItHandler()
        {
            changeBtn.interactable = false;

            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);

            SharedFlag.Instance.isGaze = !SharedFlag.Instance.isGaze;
            WayofInterationManager.Instance.ChangeWay(SharedFlag.Instance.isGaze);

            if (SharedFlag.Instance.isGaze)
            {
                text.text = "Controller";
            }
            else
            {
                text.text = "Gaze";
            }

            changeBtn.interactable = true;
        }
    }
}