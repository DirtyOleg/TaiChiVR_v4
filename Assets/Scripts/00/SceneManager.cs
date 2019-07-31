namespace TaiChiVR.Initialization
{
    using System.Collections;
    using TaiChiVR.Utility;
    using UnityEngine;

    public class SceneManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {            
            StartCoroutine(EnterScene());
        }

        IEnumerator EnterScene()
        {  
            yield return new WaitForSeconds(3f); 
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)SceneName.Welcome);
        }
    }
}