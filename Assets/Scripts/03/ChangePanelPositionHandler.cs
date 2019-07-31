namespace TaiChiVR.Simulation
{
    using UnityEngine;
    using Zinnia.Data.Type;

    //This script will handle the position of two panels, control panel and menu panel, after teleported. 
    public class ChangePanelPositionHandler : MonoBehaviour
    {
        [SerializeField] GameObject controlPanelObj = null;
        [SerializeField] GameObject menuPanelObj = null;
        [SerializeField] GameObject OppositeTelePanelObj = null;

        public void ChangePanelPosition(TransformData data)
        {
            //in order to make two canvas always face user, we need to change the position of canvas every time after user teleported. However, because canvas is a child of a room GameObject, so we could just rotate the parent GameObject to achieve the changing of canvas's position .
            Quaternion rotation = data.Rotation;
            controlPanelObj.transform.rotation = rotation;
            menuPanelObj.transform.rotation = rotation;
            OppositeTelePanelObj.transform.rotation = rotation;
        }
    }
}