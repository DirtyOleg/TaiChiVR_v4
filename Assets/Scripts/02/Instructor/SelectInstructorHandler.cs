namespace TaiChiVR.Customeize.Instructor
{
    using System.Collections;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class SelectInstructorHandler : MonoBehaviour
    {
        public void Selected(int index)
        {
            StartCoroutine(ExitActionHandler(index));
        }

        IEnumerator ExitActionHandler(int index)
        {
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);
            InstructorListManager.Instance.DisEnableAllInstructor();
            InstructorListManager.Instance.SelectThisInstructor(index);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)SceneName.Customize);
        }
    }
}