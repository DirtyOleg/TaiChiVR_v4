namespace TaiChiVR.Customeize.Instructor
{
    using TaiChiVR.Utility.Ables;
    using UnityEngine;

    public class InstructorPointable : MonoBehaviour, IPointable
    {
        public GameObject outline;
        public SelectInstructorHandler selectionHandler;
        public int index;

        public void SelectAction()
        {
            selectionHandler.Selected(index);
        }

        public void EnterAction()
        {
            outline.SetActive(true);
        }

        public void ExitAction()
        {
            outline.SetActive(false);
        }
    }
}