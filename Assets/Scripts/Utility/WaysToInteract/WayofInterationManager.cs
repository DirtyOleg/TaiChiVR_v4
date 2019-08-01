namespace TaiChiVR.Utility.WaysOfInteract
{
    using System.Collections;
    using TaiChiVR.Utility.Exposed;
    using UnityEngine;

    public class WayofInterationManager : MonoBehaviour
    {
        public static WayofInterationManager Instance;

        [SerializeField] GameObject gazePrefab = null;
        [SerializeField] GameObject controllerPrefab = null;
        [SerializeField] CurvedUIInputModule inputModel = null;

        private GameObject currentWay;

        void Awake()
        {
            //Singleton 
            if (Instance == null)
            {
                Instance = this;
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
            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            yield return null;
            ChangeWay(false);
            yield return null;
            ChangeWay(true);
        }

        public void ChangeWay(bool isGaze)
        {
            if (currentWay != null)
            {
                GameObject temp = currentWay;
                currentWay = null;
                Destroy(temp);
            }

            if (isGaze)
            {
                currentWay = Instantiate(gazePrefab, this.transform);
                inputModel.GazeTimedClickProgressImage = currentWay.GetComponent<ProgressImage>().progressBar;
                CurvedUIInputModule.ControlMethod = CurvedUIInputModule.CUIControlMethod.GAZE;
                inputModel.GazeUseTimedClick = true;
            }
            else
            {
                currentWay = Instantiate(controllerPrefab, this.transform);
                CurvedUIInputModule.ControlMethod = CurvedUIInputModule.CUIControlMethod.CUSTOM_RAY;
                inputModel.GazeUseTimedClick = false;
            }
        }
    }
}