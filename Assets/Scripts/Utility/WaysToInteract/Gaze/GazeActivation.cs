namespace TaiChiVR.Utility.WaysOfInteract.Gaze
{
    using System.Collections;
    using UnityEngine;
    using Zinnia.Action;

    [RequireComponent(typeof(BooleanAction))]
    public class GazeActivation : MonoBehaviour
    {
        BooleanAction action;

        void Start()
        {
            action = GetComponent<BooleanAction>();
            SleepGazePointerForSeconds(1);
        }

        public void SleepGazePointerForSeconds(float second)
        {
            StartCoroutine(SleepOneSecond(second));
        }

        IEnumerator SleepOneSecond(float second)
        {
            action.Receive(false);
            yield return new WaitForSeconds(second);
            action.Receive(true);
        }
    }
}