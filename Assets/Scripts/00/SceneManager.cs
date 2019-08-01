namespace TaiChiVR.Initialization
{
    using System.Collections;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;

    public class SceneManager : MonoBehaviour
    {
        public InstructorListManager myInstructor;
        public TerrainListManager myTerrain;
        public AudioListManager myAudio;

        private IEnumerable state;

        IEnumerator Start()
        {
            yield return StartCoroutine(Init());
            StartCoroutine(LoadNextScene());
        }

        //RunStateMachine
        IEnumerator Init()
        {
            state = SetupInstructor();

            while (state != null)
            {
                foreach (var cur in state)
                {
                    yield return cur;
                }
            }
        }

        IEnumerable SetupInstructor()
        {
            IEnumerable init = myInstructor.Initialization();
            foreach (var cur in init)
            {
                yield return cur;
            }

            state = SetupTerrain();
        }

        IEnumerable SetupTerrain()
        {
            IEnumerable init = myTerrain.Initialization();
            foreach (var cur in init)
            {
                yield return cur;
            }

            state = SetupAudio();
        }

        IEnumerable SetupAudio()
        {
            IEnumerable init = myAudio.Initialization();
            foreach (var cur in init)
            {
                yield return cur;
            }

            state = null;
        }

        IEnumerator LoadNextScene()
        {
            yield return null;
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)SceneName.Welcome);
        }
    }
}