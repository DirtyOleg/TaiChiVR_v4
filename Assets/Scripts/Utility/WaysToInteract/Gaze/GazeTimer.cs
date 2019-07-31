namespace TaiChiVR.Utility.WaysOfInteract.Gaze
{
    using UnityEngine;
    using UnityEngine.UI;
    using Zinnia.Action;
    using Zinnia.Extension;
    using Zinnia.Rule;
    using static Zinnia.Pointer.ObjectPointer;

    //The code in this script is copied from CurvedUIRaycaster.cs of CurvedUI and is modified to meet our project's need
    [RequireComponent(typeof(BooleanAction))]
    public class GazeTimer : MonoBehaviour
    {
        public RuleContainer SourceValidity;
        [SerializeField] Image _gazeTimedClickProgressImage = null;

        float timeForDelay; //time between user starting to look at one object and the time starting counting for click
        float timeForClick; //time need to be considered as a click
        float objectsUnderGazeLastChangeTime;
        bool gazeClickExecuted = false;
        BooleanAction Selected;
        bool isEntered = false;

        void Start()
        {
            timeForDelay = CurvedUIInputModule.Instance.GazeClickTimerDelay;
            timeForClick = CurvedUIInputModule.Instance.GazeClickTimer;

            Selected = GetComponent<BooleanAction>();
            Selected.Receive(false);
        }

        public void Entered(EventData data)
        {
            if (data == null || !SourceValidity.Accepts(data.CollisionData.collider.gameObject))
            {
                return;
            }

            isEntered = true;
            objectsUnderGazeLastChangeTime = Time.time;
        }

        public void Exited(EventData data)
        {
            if (data == null || !SourceValidity.Accepts(data.CollisionData.collider.gameObject))
            {
                return;
            }
            
            Selected.Receive(false);
            isEntered = false;
            ResetGazeTimedClick();
        }

        void Update()
        {
            if (isEntered)
            {
                ProcessGazeTimedClick();
            }
        }

        //Following 3 methods are copied from CurvedUIRaycaster.cs of CurvedUI and modified for our use.
        void ResetGazeTimedClick()
        {
            _gazeTimedClickProgressImage.fillAmount = 0;
            gazeClickExecuted = false;
        }

        void ProcessGazeTimedClick()
        {
            float passedTime = Time.time - objectsUnderGazeLastChangeTime;
            //Check if time is done and we havent executed the click yet
            if (!gazeClickExecuted && passedTime > timeForClick + timeForDelay)
            {
                Selected.Receive(true);
                gazeClickExecuted = true;
            }
            else
            {
                //Animate progress bar
                _gazeTimedClickProgressImage.fillAmount = RemapAndClamp(passedTime);
            }
        }

        float RemapAndClamp(float value)
        {
            if (value <= timeForDelay)
            {
                return 0;
            }
            else if (value > timeForClick + timeForDelay)
            {
                return 1;
            }
            else
            {
                return (value - timeForDelay) / timeForClick;
            }
        }
    }
}