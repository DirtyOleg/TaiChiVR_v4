namespace TaiChiVR.Welcome
{
    using UnityEngine;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.ListManager;
    using System.Collections;

    //SceneManager script will handle all action needed when entering or exiting this scene
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] GameObject initPlayerPosition = null;

        void Start()
        {
            VRTKFunc.Instance.ChangePlayerPosition(initPlayerPosition);
            AudioListManager.Instance.SetVolume(0.5f);
            AudioListManager.Instance.PlayDefaultAudio();
        }

        public void ExitScene(bool isGoingCustomization)
        {
            if (isGoingCustomization)
            {
                StartCoroutine(ExitActionHandler((int)SceneName.Customize));
            }
            else
            {
                StartCoroutine(ExitActionHandler((int)SceneName.Simulation));
            }
        }

        IEnumerator ExitActionHandler(int index)
        {
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);

            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }
    }
}