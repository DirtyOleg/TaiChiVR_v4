namespace TaiChiVR.Utility
{
    using TaiChiVR.Utility.EventDispatcher;
    using UnityEngine;
    using VRTK.Prefabs.Locomotion.Teleporters;
    using Zinnia.Tracking.Modification;
    using Zinnia.Visual;

    //This script will expose VRTK objects and methods for all other scenes in the project to use.
    //Because VRTK is not initialized in every scene, instead that it is initialized in the first two scene and being passed on in the following scene (because VRTK objects are under DoNotDestroy parent object, they will not be destroyed when scene changes). In order for the following scene to use the functionality/method implemented by VRTK, I have to create a static variable and expose those method.
    public class VRTKFunc : MonoBehaviour
    {
        public static VRTKFunc Instance;

        private TransformPropertyApplier transfoemApplier;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            transfoemApplier = teleporter.GetComponentInChildren<TransformPropertyApplier>(true);
            transfoemApplier.Target = playAreaAlias;
        }

        #region Properties

        [SerializeField] GameObject playAreaAlias = null;
        public GameObject PlayAreaAlias
        {
            get { return playAreaAlias; }
            private set { value = playAreaAlias; }
        }

        [SerializeField] GameObject headsetAlias = null;
        public GameObject HeadsetAlias
        {
            get { return headsetAlias; }
            private set { value = headsetAlias; }
        }

        [SerializeField] CameraColorOverlay fader = null;
        public CameraColorOverlay Fader
        {
            get { return fader; }
            private set { value = fader; }
        }

        [SerializeField] TeleporterFacade teleporter = null;
        public TeleporterFacade Teleporter
        {
            get { return teleporter; }
            private set { value = teleporter; }
        }

        [SerializeField] TeleDestEventDispatcher teleportDispatcher = null;
        public TeleDestEventDispatcher TeleportDispatcher
        {
            get { return teleportDispatcher; }
            private set { value = teleportDispatcher; }
        }

        #endregion

        #region Methods

        //use TransformPropertyApplier of VRTK to handle PlayArea position changes
        public void ChangePlayerPosition(GameObject source)
        {
            transfoemApplier.SetSource(source);
            transfoemApplier.Apply();
        }

        #endregion
    }
}