using System;
using System.ComponentModel;
using Assets.Managers;
using Assets.Scripts.GameStuff.Protos;
using DG.Tweening;
using GameStuff;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MapMan : AbcManager<MapMan>
    {
        public Transform mapCenter;
        public Transform chillPosition;
        
        public Camera gameCamera;

        public float moveToLevelDuration = 0.75f;
        public float moveIntoLevelDuration = 0.25f;

        private float playerColorLighting = 1f/6;

        Player player;
        Level currentLevel;
        Level nextLevel;
        int lastIndex = 0;

        

        protected override void InitializeManager()
        {
            
            
        }

        
        
        
        #region Player stuff
        
        
        void MakePlayer(Level startLevel)
        {
            player = Instantiate(ResMan.Instance.playerPrefab)
                .GetComponent<Player>();
            
            player.transform.position = chillPosition.position; 
            if(currentLevel != null) 
                PaintPlayerToLevelColor(currentLevel);
            MakePlayerStartChill();
            
            
        }

        void MakePlayerStartChill()
        {
            player.transform.parent = gameCamera.transform;
            player.StartChill();
        }

        void MakePlayerStopChill(Transform newParent=null)
        {
            player.transform.parent = newParent;
            player.StopChill();
        }
        
        public void MovePlayerToLevel(Level level, float moveToDuration, float moveIntoDuration)
        {
            player.MoveToLevel(LevelStartPosition(level), moveToDuration, moveIntoDuration);
            PaintPlayerToLevelColor(level);
        }
        
        
        
        
        #endregion


        public void PrepareToStart()
        {
            MakePlayerStopChill();   
        }
        
        
        
        public void StartGame()
        {
            InitLevel(currentLevel);
            MovePlayerToLevel(currentLevel, 
                moveToLevelDuration, 
                moveIntoLevelDuration);
        }
        
        #region Game state control
        
        public void StartStuff()
        {
            currentLevel = MakeLevel(0);
            nextLevel = MakeLevel(1);
            lastIndex = 1;

            MakePlayer(currentLevel);
            //InitLevel(currentLevel);
        }
        
        
        
        public void SwitchLevel()
        {
            KillLevel(currentLevel);

            lastIndex++;
            var newLevel = MakeLevel(lastIndex);
            currentLevel = nextLevel;
            nextLevel = newLevel;

            MovePlayerToLevel(currentLevel, moveToLevelDuration, moveIntoLevelDuration);
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
        
        
        public void MoveCamera(float count=1)
        {
            var newPos = gameCamera.transform.position + Vector3.down * count;
            gameCamera.transform.DOMove(newPos, 1);
        }
        
        
        
        #endregion
        
        
        #region Misc tools
        
        
        
        Color MakeColor()
        {
            var hue = Random.Range(1f/4, 1); 
            //except "light" part, for a contrast
            var sat = 0.66f;
            var val = 0.66f;
            var c = Color.HSVToRGB(hue, sat, val);
            return c;
        }

        void PaintPlayerToLevelColor(Level level)
        {
            player.ChangeColor(LightUp(level.mainColor, playerColorLighting));
        }
        
        public Color LightUp(Color color, float power = 1f/4)
        {
            float hue, sat, val;
            Color.RGBToHSV(color, out hue, out sat, out val);
            sat = Mathf.Min(1, sat + power/4);
            val = Mathf.Min(1, val + power);
            return Color.HSVToRGB(hue, sat, val);
        }
        
        public Color GetCurMainColor()
        {
            return currentLevel.mainColor;
        }
        #endregion
        
        
        # region Level tools
        
        Level MakeLevel(int levelIndex)
        {
            try
            {
                var proto = ProtoCollection.GetRandomProto().Rotated();
                var level = new Level(proto, -levelIndex, MakeColor());
                return level;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load level", e);
            }

        }
        
        
        Vector3 LevelStartPosition(Level level)
        {
            return mapCenter.position +
                   level.startPos +
                   new Vector3(1f, 0, 1f) / 2;
        }
        # endregion
        







    }
}
