namespace TaiChiVR.Simulation.MenuPanel
{
    using System.Collections;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;

    public class BackToWelcome : MonoBehaviour
    {
        public void Back()
        {
            StartCoroutine(ChangeSceneHandler());
        }

        IEnumerator ChangeSceneHandler()
        {
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);

            InstructorListManager.Instance.DisEnableSelectedInstructor();
            InstructorListManager.Instance.SelectThisInstructor(0);
            TerrainListManager.Instance.DisEnableSelectedTerrain();
            TerrainListManager.Instance.SelectThisTerrain(0);
            InstructorListManager.Instance.ChangeAllInstructorAnimController(1);

            UnityEngine.SceneManagement.SceneManager.LoadScene((int)SceneName.Welcome);
        }
    }
}