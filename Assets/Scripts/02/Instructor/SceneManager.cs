namespace TaiChiVR.Customeize.Instructor
{
    using System.Collections;
    using TaiChiVR.Utility;
    using TaiChiVR.Utility.Data;
    using TaiChiVR.Utility.Exposed;
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;

    public class SceneManager : MonoBehaviour
    {
        [SerializeField] SelectInstructorHandler selectionHandler = null;
        [SerializeField] GameObject initPlayerPosition = null;

        void Start()
        {
            VRTKFunc.Instance.ChangePlayerPosition(initPlayerPosition);
            
            if (SharedFlag.Instance.isTest)
            {
                StartCoroutine(WaitOneSecond());
            }
            else
            {
                InstructorListManager.Instance.CircleInstructors(3);
                InstructorListManager.Instance.EnableAllInstructor();
                AddPointable();
            }
        }

        void OnDisable() 
        {
            RemovePointable();
        }

        IEnumerator WaitOneSecond()
        {
            yield return new WaitForSeconds(1f);
            InstructorListManager.Instance.CircleInstructors(3);
            InstructorListManager.Instance.EnableAllInstructor();
            AddPointable();
        }

        private void AddPointable()
        {
            foreach (GameObject instructor in InstructorListManager.Instance.InstructorObjList)
            {
                InstructorPointable pointable = instructor.AddComponent<InstructorPointable>();
                pointable.outline = instructor.GetComponent<OutlineObj>().Outline;
                pointable.index = instructor.GetComponent<IndexInfo>().Index;
                pointable.selectionHandler = selectionHandler;
                instructor.layer = 0;
            }
        }

        private void RemovePointable()
        {
            foreach (GameObject instructor in InstructorListManager.Instance.InstructorObjList)
            {
                InstructorPointable pointable = instructor.GetComponent<InstructorPointable>();
                instructor.layer = 5; //TODO: 5 is UI layer, need to change to another one 
                Destroy(pointable);

                instructor.GetComponent<OutlineObj>().Outline.SetActive(false);
            }
        }
    }
}