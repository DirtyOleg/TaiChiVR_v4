namespace TaiChiVR.Simulation.MenuPanel
{
    using TaiChiVR.Utility.ListManager;
    using TaiChiVR.Utility.Scriptables;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class TerrainPanelManager : MonoBehaviour
    {
        [SerializeField] TerrainListScriptable terrainListScriptable = null;
        [SerializeField] PanelPageSwitcher pageSwitcher = null;

        private Button[] terrainBtnList;
        private int selectedIndex = 0;

        void Start()
        {
            terrainBtnList = this.GetComponentsInChildren<Button>(true);

            if (terrainListScriptable.terrainList.Length != terrainBtnList.Length)
            {
                Debug.LogError("The number of Terrain Button is not equal to the number of Terrain defined in TerrainListScriptable");
            }

            Initialization();
        }

        void OnDisable()
        {
            pageSwitcher.SwitchPage(0);//when panel is disable, change it back to first page
        }

        private void Initialization()
        {
            for (int i = 0; i < terrainBtnList.Length; i++)
            {
                int tmp = i;
                terrainBtnList[tmp].GetComponent<Image>().sprite = terrainListScriptable.terrainList[tmp].terrainPreview;
                terrainBtnList[tmp].GetComponentInChildren<TextMeshProUGUI>().text = terrainListScriptable.terrainList[tmp].terrainName;

                int index = terrainListScriptable.terrainList[tmp].terrainIndex;
                terrainBtnList[tmp].onClick.AddListener(() => SelectThisBtn(index));
            }

            selectedIndex = TerrainListManager.Instance.selectedIndex;
            terrainBtnList[selectedIndex].interactable = false;
        }

        private void SelectThisBtn(int index)
        {
            TerrainListManager.Instance.EnableThisTerrain(index);
            AudioListManager.Instance.PlayAudio(index);

            terrainBtnList[selectedIndex].interactable = true;
            selectedIndex = index;
            terrainBtnList[selectedIndex].interactable = false;
        }
    }
}