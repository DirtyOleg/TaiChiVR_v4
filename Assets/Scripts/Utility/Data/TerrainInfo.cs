namespace TaiChiVR.Utility.Data
{
    using UnityEngine;
    using System;

    [Serializable]
    public class TerrainInfo
    {
        public int terrainIndex;
        public string terrainName;
        public Sprite terrainPreview;
        public GameObject terrainPrefab;
        public AudioClip backgroundMusic;
    }
}