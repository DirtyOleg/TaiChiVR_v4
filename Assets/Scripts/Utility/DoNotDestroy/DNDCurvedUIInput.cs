namespace TaiChiVR.Utility.DoNotDestroy
{
    using UnityEngine;

    public class DNDCurvedUIInput : MonoBehaviour
    {
        [SerializeField] bool isTest = false;

        static DNDCurvedUIInput Instance;

        void Awake()
        {
            if (isTest)
            {
                Instance = this;
                return;
            }

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
                Debug.LogError("DNDCurvedUIInput Instance Initialization Failed!!");
            }
        }
    }
}