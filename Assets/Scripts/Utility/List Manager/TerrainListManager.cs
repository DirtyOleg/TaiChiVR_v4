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
            //Singleton 
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("TerrainListManager Instance Initialization Failed!!");
            }
        }

        void Start()
        {
            //Initialization
            StartCoroutine(Initialization());
        }

        IEnumerator Initialization()
        {
            selectedIndex = 0;
            
            if (SharedFlag.Instance.isTest)
            {

            }
            else
            {
                // The following wait is to avoid InstructorList, TerrainList, AudioList are starting initialization in the same frame  
                yield return null;
            }

            totalTerrainNum = terrainListScriptable.terrainList.Length;
            terrainObjList = new GameObject[totalTerrainNum];

            for (int i = 0; i < totalTerrainNum; i++)
            {
                GameObject terrainObj = Instantiate(terrainListScriptable.terrainList[i].terrainPrefab, this.transform);
                // following three lines of code is to force Unity loads Terrain Texture into Memory. Without these codes, there will be noticeable hiccup/freeze in the TerrainSelection Scene when user chooses a terrain. The reason caused that freeze is that Unity is loading Texutre and caching shader, because Unity will only load Texture into memory when that GameObject is visible to Camera the first time. To prevent that freeze happens, when initializing the Terrain GameObject, at same time, force Unity loads all necessary Texture and caches all shader. The drawback is also huge, because all terrain texture will persist in memory the whole time, even though user may never choose some of terrains, the textures of those terrains will still in memory.
                // if (!SharedFlag.Instance.isTest)
                // {
                //     terrainObj.SetActive(true);
                //     yield return null;
                //     terrainObj.SetActive(false);
                // }
                //
                terrainObjList[i] = terrainObj;
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