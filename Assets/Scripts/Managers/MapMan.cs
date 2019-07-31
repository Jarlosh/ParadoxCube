using System;
using Assets.GameStuff;
using Assets.Scripts.GameStuff;
using Assets.Scripts.GameStuff.Protos;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Managers
{
    public class MapMan : AbcManager<MapMan>
    {
        public Transform mapCenter;
        public Transform salvageTarget;
        public Player player;
        public Camera gameCamera;

        //List<Level> levels = new List<Level>();
        Level currentLevel;
        Level nextLevel;
        int lastIndex = 0;

        public Color GetCurMainColor()
        {
            return currentLevel.mainColor;
        }

        protected override void InitializeManager()
        {

        }

        void MakePlayer(Level startLevel)
        {
            player = Instantiate(ResMan.Instance.playerPrefab)
                .GetComponent<Player>();
            player.transform.position = mapCenter.position + new Vector3(0, 10, 0);
            MovePlayerToLevel(currentLevel);
        }


        Vector3 LevelStartPosition(Level level)
        {
            return mapCenter.position +
                level.startPos +
                new Vector3(1f, 0, 1f) / 2;
        }

        Color MakeColor()
        {
            var hue = Random.Range(1f/4, 1); 
            //except "light" part, for a contrast
            var sat = 0.66f;
            var val = 0.66f;
            var c = Color.HSVToRGB(hue, sat, val);
            return c;
        }

        Level MakeLevel(int levelIndex)
        {
            try
            {
                var proto = ProtoCollection.GetRandomProto();
                Debug.LogFormat("Proto {0} loaded", proto.index);
                var level = new Level(proto, -levelIndex, MakeColor());
                return level;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load level", e);
            }

        }

        public void SwitchLevel()
        {
            KillLevel(currentLevel);

            lastIndex++;
            var newLevel = MakeLevel(lastIndex);
            currentLevel = nextLevel;
            nextLevel = newLevel;

            MovePlayerToLevel(currentLevel);
            InitLevel(currentLevel);

            MoveCamera();
        }

        public void KillLevel(Level level)
        {
            level.CompleteLevel();
        }

        public void InitLevel(Level level)
        {
            level.ShowUp();
        }

        public void MovePlayerToLevel(Level level)
        {
            player.MoveToLevel(LevelStartPosition(level));
            player.ChangeColor(LightUp(level.mainColor, 1f/6));
        }

        public Color LightUp(Color color, float power = 1f/4)
        {
            float hue, sat, val;
            Color.RGBToHSV(color, out hue, out sat, out val);
            sat = Mathf.Min(1, sat + power/4);
            val = Mathf.Min(1, val + power);
            return Color.HSVToRGB(hue, sat, val);
        }


        public void MoveCamera(float count=1)
        {
            var newPos = gameCamera.transform.position + Vector3.down * count;
            gameCamera.transform.DOMove(newPos, 1);
        }

        public void StartStuff()
        {
            currentLevel = MakeLevel(0);
            nextLevel = MakeLevel(1);
            lastIndex = 1;

            MakePlayer(currentLevel);
            InitLevel(currentLevel);
        }
    }
}
