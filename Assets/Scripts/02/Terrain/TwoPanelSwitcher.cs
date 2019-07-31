namespace TaiChiVR.Customeize.Terrain
{
    using UnityEngine;

    //This script defines method, which will be used in dealing with panel switch between TerrainSelection and InteractPanel
    public class TwoPanelSwitcher : MonoBehaviour
    {
        [SerializeField] GameObject terrainPanelObj = null;
        [SerializeField] GameObject interactPanelObj = null;

        private bool isTerrainPanelOn = true;

        public void SwitchPanel()
        {
            terrainPanelObj.SetActive(!isTerrainPanelOn);
            interactPanelObj.SetActive(isTerrainPanelOn);
            isTerrainPanelOn = !isTerrainPanelOn;
        }
    }
}
