namespace TaiChiVR.Simulation.MenuPanel
{
    using UnityEngine;
    using UnityEngine.UI;

    public class MenuPanelManager : MonoBehaviour
    {
        [SerializeField] GameObject PanelObj = null;
        [SerializeField] ButtonPanelBuddy[] BtnPanelList = null;
        [SerializeField] Button closeBtn = null;

        private Button[] btnList;
        private GameObject[] panelObjList;

        private int selectedIndex = 0;

        void Start()
        {
            int length = BtnPanelList.Length;
            btnList = new Button[length];
            panelObjList = new GameObject[length];

            for (int i = 0; i < length; i++)
            {
                int temp = i;
                btnList[temp] = BtnPanelList[temp].btn;
                btnList[temp].onClick.AddListener(() => { SwitchToThisPanel(temp); });
                panelObjList[temp] = BtnPanelList[temp].panelObj;
            }

            closeBtn.onClick.AddListener(() => DisablePanelObj());
        }

        private void SwitchToThisPanel(int index)
        {
            btnList[selectedIndex].interactable = true;
            panelObjList[selectedIndex].SetActive(false);
            selectedIndex = index;
            btnList[selectedIndex].interactable = false;
            panelObjList[selectedIndex].SetActive(true);
        }

        public void EnablePanelObj()
        {
            PanelObj.SetActive(true);
        }

        public void DisablePanelObj()
        {
            PanelObj.SetActive(false);
            SwitchToThisPanel(0);
        }
    }

    [System.Serializable]
    public class ButtonPanelBuddy
    {
        public Button btn = null;
        public GameObject panelObj = null;
    }
}