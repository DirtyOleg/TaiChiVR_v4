namespace TaiChiVR.Utility
{
    using UnityEngine;

    public class SharedInfo : MonoBehaviour
    {
        public static SharedInfo Instance;

        public bool isTest = false;
        public bool isGaze = true;

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
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("SharedInfo Instance Initialization Failed!!");
            }
        }

    }
}