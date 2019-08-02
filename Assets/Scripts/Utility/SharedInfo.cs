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
            Instance = this;
        }
    }
}