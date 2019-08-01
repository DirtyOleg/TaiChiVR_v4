namespace TaiChiVR.Customeize.Terrain
{
    using TaiChiVR.Utility;
    using UnityEngine;
    using System.Collections;
    using TaiChiVR.Utility.ListManager;

    //SceneManager script will handle all action needed when entering or exiting this scene
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] GameObject initPlayerPosition = null;

        void Start()
        {
            if (SharedInfo.Instance.isTest)
            {
                StartCoroutine(TestInit());
            }
            else
            {
                VRTKFunc.Instance.ChangePlayerPosition(initPlayerPosition);
                TerrainListManager.Instance.EnableSelectedTerrain();
                AudioListManager.Instance.PlayAudio(TerrainListManager.Instance.selectedIndex);
            }
        }

        IEnumerator TestInit()
        {
            yield return new WaitForSeconds(1f);

            VRTKFunc.Instance.ChangePlayerPosition(initPlayerPosition);
            TerrainListManager.Instance.EnableSelectedTerrain();
            AudioListManager.Instance.PlayAudio(TerrainListManager.Instance.selectedIndex);
        }

        public void ExitScene()
        {
            StartCoroutine(ExitActionHandler());
        }

        IEnumerator ExitActionHandler()
        {
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);

            TerrainListManager.Instance.DisEnableSelectedTerrain();

            UnityEngine.SceneManagement.SceneManager.LoadScene((int)SceneName.Customize);
        }
    }
}