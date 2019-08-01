using System;
using System.Collections.Generic;
using Assets.GameStuff;
using Assets.Managers;
using GameStuff.Tiles;
using Managers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameStuff
{
    public class Level
    {
        private int levelIndex;
        private int xSize;
        private int zSize;
        private Vector3 levelSizeVect;


        List<WallLife> walls = new List<WallLife>();
        List<FloorLife> floors = new List<FloorLife>();
        List<FakeWallLife> fakeWalls = new List<FakeWallLife>();

        List<GameObject> shadowBorder;

        private Dictionary<TileType, Action<int, int>> tileResolvers;
        public Vector3 startPos;
        public Color mainColor;


        int activatedFloorsCount = 0;
        private GameObject container;

        public void ResolveActivation()
        {
            activatedFloorsCount++;
            if(activatedFloorsCount >= floors.Count)
            {
                MapMan.Instance.SwitchLevel();
            }
                
        }


        #region Initialization
        public Level(LevelPrototype proto, int levelIndex, Color mainColor)
        {
            this.levelIndex = levelIndex;
            this.mainColor = mainColor;
            MakeContainer();
            InitializeResolvers();
            ApplyMapPrototype(proto);
            
        }

        public void SetLevelSize(int levelDx, int levelDy, int levelDz)
        {
            xSize = levelDx;
            zSize = levelDz;
            levelSizeVect = new Vector3(levelDx, levelDy, levelDz);
            //floorOffset = levelIndex - (BlockSize.y / 2);
        }

        public void ApplyMapPrototype(LevelPrototype proto)
        {
            var map = proto.map;
            SetLevelSize(map.GetLength(0), 0, map.GetLength(1));

            for (var z = 0; z < zSize; z++)
                for (var x = 0; x < xSize; x++)
                    tileResolvers[map[x, z]](x, z);

            shadowBorder = MakeShadowCrutches();
        }

        public void MakeContainer()
        {
            var levelName = string.Format("Level {0}", levelIndex);
            container = new GameObject(levelName);
            container.transform.position = MapMan.Instance.mapCenter.position;
            container.SetActive(false);
        }
        #endregion

        #region Resolvers
        void InitializeResolvers()
        {
            tileResolvers = new Dictionary<TileType, Action<int, int>>
            {
                { TileType.Empty, ResolveEmpty },
                { TileType.Floor, ResolveFloor },
                { TileType.Wall, ResolveWall },
                { TileType.Player, ResolvePlayer}
            };
        }

        void ResolveEmpty(int x, int z)
        {
            var block = SpawnAt(ResMan.Instance.fakeWallPrefab, x, levelIndex, z);
            fakeWalls.Add(block.GetComponent<FakeWallLife>());
        }

        void ResolveWall(int x, int z)
        {
            ResolveEmpty(x, z);
            //var block = SpawnAt(ResMan.Instance.wallPrefab, x, levelIndex, z);
            //walls.Add(block.GetComponent<WallLife>());
        }

        void ResolveFloor(int x, int z)
        {
            var block = SpawnAt(ResMan.Instance.floorPrefab, x, levelIndex - 1, z);
            var blockLife = block.GetComponent<FloorLife>();
            blockLife.activeColor = mainColor;
            blockLife.onActivate += ResolveActivation;
            floors.Add(blockLife);
        }

        void ResolvePlayer(int x, int z)
        {
            ResolveFloor(x, z);
            NoticePlayerStartPos(x, z);
        }
        #endregion

        public void ShowUp()
        {
            container.SetActive(true);
            floors.ForEach(f => f.ShowUp());

        }

        public void CompleteLevel()
        {
            // Meh, no animation looks betta than any --Jarl
            #region Unused "fancy" level change
            //foreach (var floor in floors)
            //{
            //    floor.transform.parent = null;
            //    floor.Salvage();
            //}

            ////var playerTransform = MapMan.Instance.player.transform;
            //foreach (var wall in walls)
            //{
            //    wall.transform.parent = null;
            //    //wall.Salvage(playerTransform);
            //    //wall.AlternativeSalvage();
            //    wall.HardSalvage();
            //}

            //foreach (var fwall in fakeWalls)
            //{
            //    fwall.transform.parent = null;
            //    fwall.Salvage();
            //}
            #endregion
            //KillShadowBorder();
            Object.Destroy(container);
        }







        private void NoticePlayerStartPos(int x, int z)
        {
            startPos = new Vector3(x, levelIndex, z) - levelSizeVect/2;
        }




        #region Prefab Spawn
        GameObject SpawnAt(GameObject prefab, float x, float z) =>
            SpawnAt(prefab, x, levelIndex, z, Quaternion.identity);

        GameObject SpawnAt(GameObject prefab, float x, float y, float z) =>
            SpawnAt(prefab, x, y, z, Quaternion.identity);

        GameObject SpawnAt(GameObject prefab, float x, float y, float z, Quaternion quaternion)
        {
            var pos = GetBlockPos(x, y, z);
            var obj = Object.Instantiate(prefab, pos, quaternion, container.transform);
            return obj;
        }
        #endregion

        readonly Vector3 centerOfBlock = new Vector3(1, 0, 1) / 2;

        Vector3 GetBlockPos(float x, float y, float z)
        {
            return new Vector3(x, y, z) + centerOfBlock - levelSizeVect / 2 + container.transform.position;
        }


        #region Shadow Border crutch

        float shadowCrutchXsize = 5;
        float shadowCrutchZsize = 5;

        void KillShadowBorder() => 
            shadowBorder.ForEach(Object.Destroy);

        List<GameObject> MakeShadowCrutches()
        {
            var a = xSize / 2;
            var b = zSize / 2;
            var c = shadowCrutchXsize / 2;
            var d = shadowCrutchZsize / 2;

            List<GameObject> crutches = new List<GameObject>();
            var deltas = new int[] { -1, 1 };
            var cornerCrutchSize = new Vector3(2 * c, 1, 2 * d);

            var borderY = levelIndex + ResMan.Instance.wallPrefab.transform.localScale.y / 2;
            foreach (var dx in deltas)
                foreach (var dz in deltas)
                {
                    Vector3 position = new Vector3(dx * (a + c), borderY, dz * (b + d));
                    crutches.Add(SpawnCrutch(position, cornerCrutchSize));
                }

            var upper = SpawnBorderCrutch(borderY, 0, (b + d), 2 * a, 2 * d);
            var lower = SpawnBorderCrutch(borderY, 0, -(b + d), 2 * a, 2 * d);
            
            var right = SpawnBorderCrutch(borderY, (a + c), 0, 2 * c, 2 * b);
            var left = SpawnBorderCrutch(borderY, -(a + c), 0, 2 * c, 2 * b);
            crutches.AddRange(new[] { upper, lower, right, left });
            return crutches;
        }

        GameObject SpawnBorderCrutch(float y, float x, float z, float xCrutchSize, float zCrutchSize)
            => SpawnCrutch(new Vector3(x, y, z), new Vector3(xCrutchSize, 1, zCrutchSize));
        GameObject SpawnCrutch(Vector3 position, Vector3 size)
        {
            var crutch = GameObject.CreatePrimitive(PrimitiveType.Plane);
            crutch.transform.position = position + MapMan.Instance.mapCenter.position;
            crutch.transform.localScale = size/10;
            crutch.transform.parent = container.transform;
            return crutch;
        }

        #endregion
    }
}
