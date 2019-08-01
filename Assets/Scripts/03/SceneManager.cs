namespace TaiChiVR.Simulation
{
    using System.Collections;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.ListManager;
    using TMPro;
    using UnityEngine;

    //SceneManager script will handle all action needed when entering or exiting this scene
    public class SceneManager : MonoBehaviour
    {
        [Header("Countdown Settings")]
        [SerializeField] int countdownTime = 3;
        [SerializeField] GameObject canvasObj = null;
        [SerializeField] TextMeshProUGUI textBox = null;

        [Header("Scene Settings")]
        [SerializeField] TwoPanelSwitcher panelHandler = null;
        [SerializeField] TeleDestListManager teleDestManager = null;
        [SerializeField] GameObject FrontTeleDest = null;

        void Start()
        {
            teleDestManager.ManualTeleportToThere(FrontTeleDest);
            teleDestManager.DisEnableLocations();

            if (SharedInfo.Instance.isTest)
            {
                StartCoroutine(TestInit());
            }
            else
            {
                TerrainListManager.Instance.EnableSelectedTerrain();
                AudioListManager.Instance.PlayAudio(TerrainListManager.Instance.selectedIndex);
                InstructorListManager.Instance.ChangeAllInstructorAnimController(0);
                InstructorListManager.Instance.ResetSelectedInstructorPosRot();
                InstructorListManager.Instance.ResetSelectedInstructorAnim();
                InstructorListManager.Instance.EnableSelectedInstructor();
                InstructorListManager.Instance.PauseAnimation();
            }

            StartCoroutine(CountDownHandler());
        }

        IEnumerator CountDownHandler()
        {
            yield return new WaitForSeconds(5f);
            while (true)
            {
                textBox.text = countdownTime.ToString();
                yield return new WaitForSeconds(1.5f);
                countdownTime--;

                if (countdownTime < 1)
                {
                    break;
                }
            }

            Destroy(canvasObj);
            panelHandler.SwitchPanel();
            teleDestManager.EnableLocations();

            InstructorListManager.Instance.NormalSpeedAnimation();
        }

        IEnumerator TestInit()
        {
            yield return new WaitForSeconds(1f);

            TerrainListManager.Instance.EnableSelectedTerrain();
            AudioListManager.Instance.PlayAudio(TerrainListManager.Instance.selectedIndex);
            InstructorListManager.Instance.ResetSelectedInstructorPosRot();
            InstructorListManager.Instance.ChangeAllInstructorAnimController(0);
            InstructorListManager.Instance.ResetSelectedInstructorAnim();
            InstructorListManager.Instance.EnableSelectedInstructor();
            InstructorListManager.Instance.PauseAnimation();
        }
    }
}