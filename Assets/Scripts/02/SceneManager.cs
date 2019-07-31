namespace TaiChiVR.Customeize
{
    using System.Collections;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.ListManager;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class SceneManager : MonoBehaviour
    {
        [SerializeField] Image instructorImage = null;
        [SerializeField] Image terrainImage = null;
        [SerializeField] GameObject initPlayerPosition = null;

        void Start()
        {
            VRTKFunc.Instance.ChangePlayerPosition(initPlayerPosition);
            instructorImage.sprite = InstructorListManager.Instance.SelectedInstructorPreview;
            terrainImage.sprite = TerrainListManager.Instance.SelectedTerrainPreview;
            AudioListManager.Instance.PlayDefaultAudio();
        }

        public void ChangeTerrain()
        {
            StartCoroutine(ChangeSceneHandler((int)SceneName.TerrainSelection));
        }

        public void ChangeInstructor()
        {
            StartCoroutine(ChangeSceneHandler((int)SceneName.InstructorSelection));
        }

        public void StartSimulation()
        {
            StartCoroutine(ChangeSceneHandler((int)SceneName.Simulation));
        }

        IEnumerator ChangeSceneHandler(int sceneIndex)
        {
            VRTKFunc.Instance.Fader.Blink();
            yield return new WaitForSeconds(VRTKFunc.Instance.Fader.AddDuration);

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
    }
}