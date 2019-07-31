namespace TaiChiVR.Simulation.ControlPanel
{
    using TaiChiVR.Utility.ListManager;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ProgressIndicatorManager : MonoBehaviour
    {
        public static bool clipStarted = false;

        [SerializeField] Image progressBarImage = null;
        [SerializeField] TextMeshProUGUI timerText = null;

        private InstructorListManager instructorListManager;
        private float totalDuration;
        private float normalizedTime;
        private string temp;

        void Start()
        {
            instructorListManager = InstructorListManager.Instance;
        }

        void Update()
        {
            //TODO: super inefficient method. Also, need to find an effective to generate string, maybe stringbuilder or others
            if (clipStarted)
            {
                totalDuration = instructorListManager.GetAnimStateLength();
                System.TimeSpan totolTime = System.TimeSpan.FromSeconds(totalDuration);
                temp = totolTime.ToString(@"mm\:ss");
                clipStarted = false;
            }
            normalizedTime = instructorListManager.GetAnimStateNormalizedTime();
            System.TimeSpan elapsedTime = System.TimeSpan.FromSeconds(totalDuration * normalizedTime);

            timerText.text = string.Format("{0} / {1}", elapsedTime.ToString(@"mm\:ss"), temp);
            progressBarImage.fillAmount = normalizedTime;
        }
    }
}