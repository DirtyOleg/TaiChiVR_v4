namespace TaiChiVR.Utility.Scriptables
{
    using UnityEngine;
    using TaiChiVR.Utility.Data;

    [CreateAssetMenu(fileName = "New TerrainList", menuName = "TerrainList", order = 51)]
    public class TerrainListScriptable : ScriptableObject
    {
        public TerrainInfo[] terrainList;
    }
}