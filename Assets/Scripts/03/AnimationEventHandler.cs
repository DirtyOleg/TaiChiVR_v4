namespace TaiChiVR.Simulation
{
    using TaiChiVR.Simulation.ControlPanel;
    using UnityEngine;

    public class AnimationEventHandler : MonoBehaviour
    {
        //useful link: https://docs.unity3d.com/Manual/animeditor-AnimationEvents.html

        public void OnAnimClipStart()
        {
            ProgressIndicatorManager.clipStarted = true; //TODO: find a better way to do this 
        }
    }
}