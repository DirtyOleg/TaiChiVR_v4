namespace TaiChiVR.Simulation.MenuPanel
{
    using TaiChiVR.Simulation.ControlPanel;
    using TaiChiVR.Utility.ListManager;
    using TaiChiVR.Utility.Scriptables;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class InstructorPanelManager : MonoBehaviour
    {
        [SerializeField] InstructorListScriptable instructorListScriptable = null;
        [SerializeField] ControlPanelBtnManager controlBtnManager = null; // when user change instructor, also reset anim button interactable
        [SerializeField] PanelPageSwitcher pageSwitcher = null;

        private Button[] instructorBtnList;
        private int selectedIndex = 0;

        void Start()
        {
            instructorBtnList = this.GetComponentsInChildren<Button>(true);

            if (instructorListScriptable.instructorList.Length != instructorBtnList.Length)
            {
                Debug.LogError("The number of Instructor Button is not equal to the number of Instructor defined in InstructorListScriptable");
            }

            Initialization();
        }

        void OnDisable()
        {
            pageSwitcher.SwitchPage(0);//when panel is disable, change it back to first page
        }

        private void Initialization()
        {
            for (int i = 0; i < instructorBtnList.Length; i++)
            {
                int tmp = i;
                instructorBtnList[tmp].GetComponent<Image>().sprite = instructorListScriptable.instructorList[tmp].instructorPreview;
                instructorBtnList[tmp].GetComponentInChildren<TextMeshProUGUI>().text = instructorListScriptable.instructorList[tmp].instructorName;

                int index = instructorListScriptable.instructorList[i].instructorIndex;
                instructorBtnList[tmp].onClick.AddListener(() => SelectThisBtn(index));
            }

            selectedIndex = InstructorListManager.Instance.selectedIndex;
            instructorBtnList[selectedIndex].interactable = false;
        }

        private void SelectThisBtn(int index)
        {
            InstructorListManager.Instance.EnableThisInstructor(index);
            InstructorListManager.Instance.ResetSelectedInstructorPosRot();
            InstructorListManager.Instance.ResetSelectedInstructorAnim();
            InstructorListManager.Instance.PauseAnimation();
            controlBtnManager.ResetBtnValuesForPause();

            instructorBtnList[selectedIndex].interactable = true;
            selectedIndex = index;
            instructorBtnList[selectedIndex].interactable = false;
        }
    }
}