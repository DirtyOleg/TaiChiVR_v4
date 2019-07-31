namespace TaiChiVR.Customeize.Terrain
{
    using TaiChiVR.Utility.Scriptables;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using TaiChiVR.Utility.ListManager;

    public class TerrainBtnInitHandler : MonoBehaviour
    {
        private Button[] terrainBtnList;
        [SerializeField] TerrainListScriptable terrainListScriptable = null;

        void Start()
        {
            terrainBtnList = this.GetComponentsInChildren<Button>(true);

            if (terrainListScriptable.terrainList.Length != terrainBtnList.Length)
            {
                Debug.LogError("The number of Terrain Button is not equal to the number of Terrain defined in TerrainListScriptable");
            }

            Initialization();
        }

        private void Initialization()
        {
            for (int i = 0; i < terrainBtnList.Length; i++)
            {
                terrainBtnList[i].GetComponent<Image>().sprite = terrainListScriptable.terrainList[i].terrainPreview;
                terrainBtnList[i].GetComponentInChildren<TextMeshProUGUI>().text = terrainListScriptable.terrainList[i].terrainName;

                int index = terrainListScriptable.terrainList[i].terrainIndex;
                terrainBtnList[i].onClick.AddListener(() => SelectThisBtn(index));
            }
        }

        private void SelectThisBtn(int index)
        {
            TerrainListManager.Instance.EnableThisTerrain(index);
            AudioListManager.Instance.PlayAudio(index);
        }
    }
}