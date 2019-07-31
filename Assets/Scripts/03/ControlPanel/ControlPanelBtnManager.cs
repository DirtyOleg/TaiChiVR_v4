namespace TaiChiVR.Simulation.ControlPanel
{
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class ControlPanelBtnManager : MonoBehaviour
    {
        [SerializeField] Button slowdownBtn = null;
        [SerializeField] Button speedupBtn = null;
        [SerializeField] Button pauseResumeBtn = null;
        [SerializeField] Button resetBtn = null;
        [SerializeField] Sprite resumeSprite = null;
        [SerializeField] Sprite pauseSprite = null;

        private bool _isPause = false; // Is instructor currently paused        
        private bool _isSpeedup = false; // Is instructor currently speeding up
        private bool _isSlowdown = false; // Is instructor currently slowing down

        void Start()
        {
            slowdownBtn.onClick.AddListener(SlowdownHandler);
            speedupBtn.onClick.AddListener(SpeedupHandler);
            pauseResumeBtn.onClick.AddListener(PauseOrResumeHandler);
            resetBtn.onClick.AddListener(ResetHandler);
        }

        #region Button OnClick Handler

        private void SpeedupHandler()
        {
            if (slowdownBtn.interactable)
            {
                InstructorListManager.Instance.SpeedupAnimation();
                speedupBtn.interactable = false;
                _isSpeedup = true;
            }
            else
            {
                InstructorListManager.Instance.NormalSpeedAnimation();
                slowdownBtn.interactable = true;
                _isSlowdown = false;
            }
        }

        private void SlowdownHandler()
        {
            if (speedupBtn.interactable)
            {
                InstructorListManager.Instance.SlowdownAnimation();
                slowdownBtn.interactable = false;
                _isSlowdown = true;
            }
            else
            {
                InstructorListManager.Instance.NormalSpeedAnimation();
                speedupBtn.interactable = true;
                _isSpeedup = false;
            }
        }

        private void PauseOrResumeHandler()
        {
            if (_isPause)
            {
                // When pause/resume button is hit and instructor currently is paused, means user want to resume animation
                InstructorListManager.Instance.ResumeAnimation();

                pauseResumeBtn.GetComponent<Image>().sprite = pauseSprite;
                speedupBtn.interactable = !_isSpeedup;
                slowdownBtn.interactable = !_isSlowdown;
            }
            else
            {
                // User want to pause animation
                InstructorListManager.Instance.PauseAnimation();

                pauseResumeBtn.GetComponent<Image>().sprite = resumeSprite;
                speedupBtn.interactable = false;
                slowdownBtn.interactable = false;
            }

            _isPause = !_isPause;
        }

        private void ResetHandler()
        {
            InstructorListManager.Instance.ResetSelectedInstructorPosRot();
            InstructorListManager.Instance.ResetSelectedInstructorAnim();
            ResetBtnValues();
        }

        //Reset Button values assumes that instructor's animation will autoplay after reset.
        public void ResetBtnValues()
        {
            pauseResumeBtn.GetComponent<Image>().sprite = pauseSprite;
            speedupBtn.interactable = true;
            slowdownBtn.interactable = true;
            _isPause = false;
            _isSlowdown = false;
            _isSpeedup = false;
        }

        //Reset Button values assumes that instructor's animation will be paused after reset.
        public void ResetBtnValuesForPause()
        {
            pauseResumeBtn.GetComponent<Image>().sprite = resumeSprite;
            speedupBtn.interactable = true;
            slowdownBtn.interactable = true;
            _isPause = true;
            _isSlowdown = false;
            _isSpeedup = false;
        }

        #endregion
    }
}