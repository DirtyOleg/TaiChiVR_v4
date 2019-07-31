namespace TaiChiVR.Simulation.ControlPanel
{
    using TaiChiVR.Utility;
    using UnityEngine;

    public class ControlPanelManager : MonoBehaviour
    {
        [SerializeField] GameObject PanelObj = null;
        [SerializeField] float startFadeIn = 19; //headset rotation value for control panel to start fade in
        [SerializeField] float endFadeIn = 23; //headset rotation value for control panel to end fade in

        private CanvasGroup controlPanelCanvasGroup;
        private GameObject headsetAlise;

        void Start()
        {
            controlPanelCanvasGroup = PanelObj.GetComponentInChildren<CanvasGroup>(true);
            headsetAlise = VRTKFunc.Instance.HeadsetAlias;
        }

        void Update()
        {
            ControlPanelFadeIn(); //TODO: maybe find another way to deal with this, instead of putting it in the Update(), using VRTK event??
        }

        private void ControlPanelFadeIn()
        {
            //Debug.Log(headsetAlise.transform.localEulerAngles.x);
            controlPanelCanvasGroup.alpha = Normalize(endFadeIn, startFadeIn, headsetAlise.transform.localEulerAngles.x);
        }

        //return is the normalized value of t
        private float Normalize(float endFadeIn, float startFadeIn, float t)
        {
            if (endFadeIn <= t && t < 80f) //80 degree means that user is looking at ground
            {
                return 1;
            }
            else if (startFadeIn < t && t < endFadeIn)
            {
                return (t - startFadeIn) / (endFadeIn - startFadeIn);
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