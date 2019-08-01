namespace TaiChiVR.Utility.ListManager
{
    using System.Collections;
    using TaiChiVR.Utility.Data;
    using TaiChiVR.Utility.Scriptables;
    using UnityEngine;

    public class InstructorListManager : MonoBehaviour
    {
        public static InstructorListManager Instance;

        [SerializeField] InstructorListScriptable instructorListScriptable = null;
        //
        private int totalInstructorNum;

        //
        //
        [Header("Animation Speed Setting")]
        [SerializeField] float _speedupScale = 1.5f;
        [SerializeField] float _slowdownScale = 0.5f;
        public bool isPaused { get; private set; }
        private int totalControllerNum;
        private float savedSpeed = 1;
        private Animator selectedAnimator;
        private RuntimeAnimatorController[] animControllerList;
        //

        #region Properties

        private GameObject[] instructorObjList;
        public GameObject[] InstructorObjList
        {
            get { return instructorObjList; }
            private set { instructorObjList = value; }
        }

        public int selectedIndex { get; private set; }

        private Sprite selectedInstructorPreview;
        public Sprite SelectedInstructorPreview
        {
            get { return instructorListScriptable.instructorList[selectedIndex].instructorPreview; }
            private set { selectedInstructorPreview = value; }
        }

        #endregion

        void Awake() 
        {
            Instance = this;
        }

        public IEnumerable Initialization()
        {
            // Instructor GameObject List Initialization
            selectedIndex = 0;

            totalInstructorNum = instructorListScriptable.instructorList.Length;
            instructorObjList = new GameObject[totalInstructorNum];

            for (int i = 0; i < totalInstructorNum; i++)
            {
                GameObject instructorObj = Instantiate(instructorListScriptable.instructorList[i].instructorPrefab, this.transform);
                instructorObj.AddComponent<IndexInfo>().Index = instructorListScriptable.instructorList[i].instructorIndex;
                // if (!SharedFlag.Instance.isTest)
                // {
                //     instructorObj.SetActive(true);
                //     yield return null;
                //     instructorObj.SetActive(false);
                // }
                instructorObjList[i] = instructorObj;
                yield return null;
            }

            //
            // Instructor Animation List Initialization

            totalControllerNum = instructorListScriptable.animControllerList.Length;
            animControllerList = new RuntimeAnimatorController[totalControllerNum];

            for (int i = 0; i < totalControllerNum; i++)
            {
                animControllerList[i] = instructorListScriptable.animControllerList[i];
                yield return null;
            }

            selectedAnimator = instructorObjList[selectedIndex].GetComponent<Animator>();
        }

        public void SelectThisInstructor(int index)
        {
            selectedIndex = index;
            selectedAnimator = instructorObjList[selectedIndex].GetComponent<Animator>();
        }

        #region Instructor GameObject Position/Rotation Action

        /// <summary>
        /// Dynamically Setting up Instructors' rotation and position, so instructors will be placed around user in a circle instead of in a straight line
        /// </summary>
        public void CircleInstructors(float distance)
        {
            float maxAngle = 150f; //degree  

            float intervalAngle = maxAngle / (totalInstructorNum + 1); //degree
            float startAngle = (180 - maxAngle) / 2; //degree

            //Unity treat Z-axis as Zero-Degree-axis

            for (int i = 0; i < totalInstructorNum; i++)
            {
                //Set up Position and Rotation
                float angle = startAngle + (i + 1) * intervalAngle - 90; //degree
                float angleR = angle * Mathf.PI / 180; //radius

                instructorObjList[i].transform.rotation = Quaternion.Euler(0, angle - 180, 0);
                instructorObjList[i].transform.position = new Vector3(distance * Mathf.Sin(angleR), 0, distance * Mathf.Cos(angleR));
            }
        }

        public void ResetSelectedInstructorPosRot()
        {
            instructorObjList[selectedIndex].transform.position = Vector3.zero;
            instructorObjList[selectedIndex].transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        public void ResetAllInstructor()
        {
            for (int i = 0; i < totalInstructorNum; i++)
            {
                instructorObjList[i].SetActive(false);
                instructorObjList[i].transform.position = Vector3.zero;
                instructorObjList[i].transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }

        #endregion

        #region Instructor GameObject Enable/DisEnable Actions

        public void EnableAllInstructor()
        {
            for (int i = 0; i < totalInstructorNum; i++)
            {
                instructorObjList[i].SetActive(true);
                //instructorObjList[i].GetComponent<CapsuleCollider>().enabled = true;
            }
        }

        public void DisEnableAllInstructor()
        {
            for (int i = 0; i < totalInstructorNum; i++)
            {
                instructorObjList[i].SetActive(false);
                //instructorObjList[i].GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        //Enable instructor with this index, disable current selected instructor, also change selected to this instructor
        public void EnableThisInstructor(int index)
        {
            if (index == selectedIndex)
            {
                return;
            }

            instructorObjList[selectedIndex].SetActive(false);
            SelectThisInstructor(index);
            instructorObjList[selectedIndex].SetActive(true);
        }

        public void DisEnableThisInstructor(int index)
        {
            instructorObjList[index].SetActive(false);
        }

        public void EnableSelectedInstructor()
        {
            instructorObjList[selectedIndex].SetActive(true);
        }

        public void DisEnableSelectedInstructor()
        {
            instructorObjList[selectedIndex].SetActive(false);
        }

        #endregion    

        #region Instructor Animation Actions

        public void ChangeAllInstructorAnimController(int index)
        {
            foreach (var instructor in InstructorListManager.Instance.instructorObjList)
            {
                instructor.GetComponent<Animator>().runtimeAnimatorController = animControllerList[index];
            }
        }

        public void ResetSelectedInstructorAnim()
        {
            NormalSpeedAnimation();
            selectedAnimator.Play("Default", -1, 0f);
            isPaused = false;
        }

        public void NormalSpeedAnimation()
        {
            savedSpeed = 1;
            selectedAnimator.speed = 1f;
        }

        public void SpeedupAnimation()
        {
            savedSpeed = _speedupScale;
            selectedAnimator.speed = _speedupScale;
        }

        public void SlowdownAnimation()
        {
            savedSpeed = _slowdownScale;
            selectedAnimator.speed = _slowdownScale;
        }

        public void PauseAnimation()
        {
            if (!isPaused)
            {
                savedSpeed = selectedAnimator.speed;
                selectedAnimator.speed = 0f;
                isPaused = true;
            }
        }

        public void ResumeAnimation()
        {
            selectedAnimator.speed = savedSpeed;
            isPaused = false;
        }

        public float GetAnimStateNormalizedTime()
        {
            //useful link: https://docs.unity3d.com/ScriptReference/AnimatorStateInfo.html
            return selectedAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        public float GetAnimStateLength()
        {
            //I do not know how Unity internally handle the animation clip length calculation, but apparently the value returned by GetCurrentAnimatorStateInfo().length will be affected by the speed of the animator. For example, a 300 second animation with animator speed = 1, will return 300; a 300 second with animation with animator speed = 3, will return 100;
            return selectedAnimator.GetCurrentAnimatorStateInfo(0).length * savedSpeed;
        }

        #endregion    
    }
}