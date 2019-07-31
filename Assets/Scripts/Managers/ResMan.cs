using System;
using UnityEngine;

namespace Assets.Managers
{
    public class ResMan : AbcManager<ResMan>
    {
        public GameObject wallPrefab;
        public GameObject fakeWallPrefab;
        public GameObject floorPrefab;
        public GameObject playerPrefab;

        [Range(0, 1)]
        public float playerShellOpacity = 0.5f;




        protected override void InitializeManager()
        {
        }
    }
}
