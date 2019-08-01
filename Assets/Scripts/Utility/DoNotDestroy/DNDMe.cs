namespace TaiChiVR.Utility.DoNotDestroy
{
    using UnityEngine;

    //DoNotDestroyMe
    public class DNDMe : MonoBehaviour
    {
        void Awake()
        {
            if (SharedInfo.Instance.isTest)
            {
                return;
            }

            DontDestroyOnLoad(this.gameObject);
        }
    }
}

