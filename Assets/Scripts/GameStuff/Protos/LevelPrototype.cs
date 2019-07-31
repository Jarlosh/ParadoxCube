using Assets.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
    }
}
