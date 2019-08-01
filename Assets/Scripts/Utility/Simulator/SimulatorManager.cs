namespace TaiChiVR.Utility.Simulator
{
    using System.Collections;
    using TaiChiVR.Utility.ListManager;
    using UnityEngine;

    public class SimulatorManager : MonoBehaviour
    {
        public InstructorListManager myInstructor;
        public TerrainListManager myTerrain;
        public AudioListManager myAudio;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(myInstructor.Initialization().GetEnumerator());
            StartCoroutine(myTerrain.Initialization().GetEnumerator());
            StartCoroutine(myAudio.Initialization().GetEnumerator());
        }
    }
}