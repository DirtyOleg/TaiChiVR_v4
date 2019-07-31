namespace TaiChiVR.Simulation
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class PanelPageSwitcher : MonoBehaviour
    {
        [SerializeField] GameObject[] panelList = null;
        [SerializeField] Button prevBtn = null;
        [SerializeField] Button nextBtn = null;
        [SerializeField] TextMeshProUGUI pageText = null;

        private int totalNumofPages = 0;
        private int currentPanelIndex = 0;

        void Start()
        {
            prevBtn.onClick.AddListener(() => SwitchPage(-1));
            nextBtn.onClick.AddListener(() => SwitchPage(1));
            totalNumofPages = panelList.Length;
            SetPageText();
        }

        public void SwitchPage(int direction)
        {
            //direction = 0 move to First Page; direction = -1 move to Previous Page; direction = +1 move to Next Page
            int nextPanelIndex;
            if (direction == 0)
            {
                nextPanelIndex = 0;
            }
            else
            {
                nextPanelIndex = currentPanelIndex + direction;
            }

            if (currentPanelIndex == nextPanelIndex)
            {
                return;
            }

            panelList[currentPanelIndex].gameObject.SetActive(false);
            panelList[nextPanelIndex].gameObject.SetActive(true);
            currentPanelIndex = nextPanelIndex;

            if (currentPanelIndex == 0)
            {
                nextBtn.interactable = true;
                prevBtn.interactable = false;
            }
            else if (currentPanelIndex == totalNumofPages - 1)
            {
                nextBtn.interactable = false;
                prevBtn.interactable = true;
            }
            else
            {
                nextBtn.interactable = true;
                prevBtn.interactable = true;
            }

            SetPageText();
        }

        private void SetPageText()
        {
            pageText.text = (currentPanelIndex + 1) + " / " + totalNumofPages;
        }
    }
}