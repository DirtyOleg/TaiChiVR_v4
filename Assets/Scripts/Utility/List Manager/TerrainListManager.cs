namespace TaiChiVR.Utility.ListManager
{
    using System.Collections;
    using TaiChiVR.Utility.Scriptables;
    using UnityEngine;

    public class TerrainListManager : MonoBehaviour
    {
        public static TerrainListManager Instance;
        [SerializeField] TerrainListScriptable terrainListScriptable = null;

        private GameObject[] terrainObjList;
        private int totalTerrainNum;

        void Awake() 
        {
            Instance = this;
        }

        public IEnumerable Initialization()
        {
            selectedIndex = 0;

            totalTerrainNum = terrainListScriptable.terrainList.Length;
            terrainObjList = new GameObject[totalTerrainNum];

            for (int i = 0; i < totalTerrainNum; i++)
            {
                GameObject terrainObj = Instantiate(terrainListScriptable.terrainList[i].terrainPrefab, this.transform);

                terrainObjList[i] = terrainObj;
                yield return null;
            }
        }

        #region Properties

        public int selectedIndex { get; private set; }

        private Sprite selectedTerrainPreview;
        public Sprite SelectedTerrainPreview
        {
            get { return terrainListScriptable.terrainList[selectedIndex].terrainPreview; }
            private set { selectedTerrainPreview = value; }
        }

        #endregion

        public void SelectThisTerrain(int index)
        {
            selectedIndex = index;
        }

        public void EnableThisTerrain(int index)
        {
            if (index == selectedIndex)
            {
                return;
            }

            if (selectedIndex >= 0)
            {
                terrainObjList[selectedIndex].SetActive(false);
            }

            selectedIndex = index;
            terrainObjList[selectedIndex].SetActive(true);
        }

        public void EnableSelectedTerrain()
        {
            terrainObjList[selectedIndex].SetActive(true);
        }

        public void DisEnableSelectedTerrain()
        {
            terrainObjList[selectedIndex].SetActive(false);
        }
    }
}