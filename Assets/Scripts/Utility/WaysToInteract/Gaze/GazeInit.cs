namespace TaiChiVR.Utility.WaysOfInteract.Gaze
{
    using UnityEngine;

    public class GazeInit : MonoBehaviour
    {
        [SerializeField] GameObject resetFillCircle = null;

        private void OnEnable()
        {
            resetFillCircle.SetActive(false);
        }

        private void OnDisable()
        {
            resetFillCircle.SetActive(true);
        }
    }
}