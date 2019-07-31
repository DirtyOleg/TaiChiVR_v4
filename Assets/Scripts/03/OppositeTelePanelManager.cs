namespace TaiChiVR.Simulation
{
    using TaiChiVR.Utility;
    using UnityEngine;

    public class OppositeTelePanelManager : MonoBehaviour
    {
        [SerializeField] GameObject PanelObj = null;
        [SerializeField] float startFadeIn = 355f; //headset rotation value for control panel to start fade in
        [SerializeField] float endFadeIn = 350f; //headset rotation value for control panel to end fade in

        private CanvasGroup panelCanvasGroup;
        private GameObject headsetAlise;

        void Start()
        {
            panelCanvasGroup = PanelObj.GetComponentInChildren<CanvasGroup>(true);
            headsetAlise = VRTKFunc.Instance.HeadsetAlias;
        }

        void Update()
        {
            ControlPanelFadeIn(); //TODO: maybe find another way to deal with this, instead of putting it in the Update(), using VRTK event??
        }

        private void ControlPanelFadeIn()
        {
            //Debug.Log(Mathf.Abs(headsetAlise.transform.localEulerAngles.x));
            panelCanvasGroup.alpha = Normalize(endFadeIn, startFadeIn, headsetAlise.transform.localEulerAngles.x);
        }

        //return is the normalized value of t
        private float Normalize(float endFadeIn, float startFadeIn, float t)
        {
            if (endFadeIn < t && t < startFadeIn)
            {
                return 1 - ((t - endFadeIn) / (startFadeIn - endFadeIn));
            }
            else if (300 < t && t <= endFadeIn) //300 degree means that user is looking at sky
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void EnablePanelObj()
        {
            PanelObj.SetActive(true);
        }

        public void DisablePanelObj()
        {
            PanelObj.SetActive(false);
        }
    }
}