namespace TaiChiVR.Utility
{
    using UnityEngine;

    public class SharedFlag : MonoBehaviour
    {
        public static SharedFlag Instance;

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
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("SharedFlag Instance Initialization Failed!!");
            }
        }
    }
}