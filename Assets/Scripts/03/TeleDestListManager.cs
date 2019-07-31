namespace TaiChiVR.Simulation
{
    using TaiChiVR.Utility;
    using UnityEngine;
    using VRTK.Prefabs.Locomotion.DestinationLocations;
    using Zinnia.Data.Type;

    public class TeleDestListManager : MonoBehaviour
    {
        [SerializeField] GameObject TeleportLocations = null;        

        void Awake()
        {
            DestinationLocationFacade[] locations = TeleportLocations.GetComponentsInChildren<DestinationLocationFacade>(true);
            foreach (var location in locations)
            {
                location.Activated.AddListener(VRTKFunc.Instance.Teleporter.Teleport);
            }
        }

        public void EnableLocations()
        {
            TeleportLocations.SetActive(true);
        }

        public void DisEnableLocations()
        {
            TeleportLocations.SetActive(false);
        }

        public void ManualTeleportToThere(GameObject there)
        {
            // TODO: simplies the following code for manual teleporting 
            SurfaceData data = new SurfaceData();
            data.Transform = this.gameObject.transform;
            RaycastHit hit;
            Physics.Raycast(Vector3.zero, there.transform.forward * -1, out hit);
            data.CollisionData = hit;
            VRTKFunc.Instance.TeleportDispatcher.Enter(data);
            VRTKFunc.Instance.TeleportDispatcher.Select(data);
        }

        public void ManualTeleportToOpposite()
        {
            SurfaceData data = new SurfaceData();
            data.Transform = this.gameObject.transform;
            RaycastHit hit;
            Physics.Raycast(Vector3.zero, VRTKFunc.Instance.TeleportDispatcher.SelectedLocation.transform.parent.transform.forward, out hit);
            data.CollisionData = hit;
            VRTKFunc.Instance.TeleportDispatcher.Enter(data);
            VRTKFunc.Instance.TeleportDispatcher.Select(data);
        }
    }
}