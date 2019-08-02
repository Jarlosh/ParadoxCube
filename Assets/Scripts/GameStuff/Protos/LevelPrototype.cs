using Assets.Misc;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.GameStuff
{
    public enum TileType
    {
        Empty = 3,
        Floor = 0,
        Wall = 1,
        Player = 2,
        
    }


    public class LevelPrototype
    {
        public TileType[,] map;
        public int index;
        private int maxWidth = 9;
        
        public LevelPrototype(int[,] readed, int index = 0)
        {
            map = Tools.CastArr(readed, v => (TileType)v);
            this.index = index;
        }

        public LevelPrototype(TileType[,] map, int index)
        {
            this.map = map;
            this.index = index;
        }


        static readonly int[,] smallLevel =
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1},
        };

        static readonly int[,] defaultLevel =
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 2, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 1, 3, 3, 3, 3, 3, 3},
            {1, 1, 1, 3, 3, 3, 3, 3, 3}
        };


        public static LevelPrototype GetDefaultProto() => new LevelPrototype(defaultLevel);
        public static LevelPrototype GetSmallProto() => new LevelPrototype(smallLevel);

        public static LevelPrototype GenerateLevel()
        {
            throw new NotImplementedException();
        }

        public static LevelPrototype ReadLevel()
        {
            throw new NotImplementedException();

        }

        public LevelPrototype Rotated(int nightyDegreesCoeff=-1)
        {
            RotateMe(nightyDegreesCoeff);
            return this;
        }

        public void RotateMe(int nightyDegreesCoeff=-1)
        {
            if (nightyDegreesCoeff == -1)
            {
                var height = map.GetLength(1);
                var canBeTransponed = height <= maxWidth;

                if (canBeTransponed)
                    nightyDegreesCoeff = Random.Range(0, 4);
                else
                    nightyDegreesCoeff = 0;
            }
            map = RotateLevel(map, nightyDegreesCoeff);
        }
        
        #region Rotating levels
        public static TileType[,] RotateLevel(TileType[,] level, int nightyDegreesCoeff)
        {
            switch (nightyDegreesCoeff % 4)
            {
                case 0: return level;
                case 1: return TransponeLevel(FlipLevelVert(level));
                case 2: return FlipLevelVert(FlipLevelHor(level));
                case 3: return TransponeLevel(FlipLevelHor(level));;
                default: throw new ArgumentException(); 
            }
        }

        public static T[,] FlipLevelHor<T>(T[,] level)
        {
            var width = level.GetLength(0);
            var height = level.GetLength(1);
            var res = new T[width, height];
            for(var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    res[x, y] = level[width - x - 1, y];
            return res;
        }
        public static T[,] FlipLevelVert<T>(T[,] level)
        {
            var width = level.GetLength(0);
            var height = level.GetLength(1);
            var res = new T[width, height];
            for(var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)    
                    res[x, y] = level[x, height - 1 - y];
            return res;
        }

        public static T[,] TransponeLevel<T>(T[,] level)
        {
            var width = level.GetLength(0);
            var height = level.GetLength(1);
            var res = new T[height, width];
            for(var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    res[y, x] = level[x, y];
            return res;
        }
        #endregion
    }
}
