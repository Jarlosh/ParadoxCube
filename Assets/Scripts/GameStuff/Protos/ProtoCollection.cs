using Assets.GameStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

namespace Assets.Scripts.GameStuff.Protos
{
    public class ProtoCollection
    {



        static string[] levels = new string[]
        {
        //
        @"
        111111111
        100000001
        100000001
        101111111
        100012001
        101011101
        111000001
        111111111",
        //@"            
        //311111111
        //310000001
        //310111101
        //310100001
        //120111111
        //111133333",
        @"
        111111111
        100010001
        100000001
        101110001
        100000011
        111111001
        100011001
        121000001
        111111111",
        @"
        111111111
        100100001
        100101101
        100101101
        110000001
        100000011
        101101113
        101101333
        120001333
        111111333",
        @"
        111111111
        100000001
        101111101
        101000101
        101010101
        101000001
        121111111
        111333333"
        //
        };
        public static TileType Parse(char ch)
        {
            switch(ch)
            {
                case '0': return TileType.Floor;
                case '1': return TileType.Wall;
                case '2': return TileType.Player;
                case '3': return TileType.Empty;
                default: throw new FormatException(string.Format("'{0}'", ch));
            }
        }

        
        
        
        public static TileType[,] StringLevelToProtoMap(string level)
        {
            var strs = level.Split('\n');
            var lines = new List<string>(strs)
                .Select(s => s.Trim())
                .Where(s => s.Length > 1)
                .ToList();

            var height = lines.Count;
            var width = lines[0].Length;
            var result = new TileType[width, height];
                for (var y = 0; y < height; y++)
                {
                    var row = lines[y];
                    for (var x = 0; x < width; x++)
                    {
                        result[x, y] = Parse(row[x]);
                        //result[x, y] = (TileType)int.Parse(row[x].ToString()); 
                    }
                }
            return result;
        }

        public static LevelPrototype StringLevelToProto(string level, int index) =>
            new LevelPrototype(StringLevelToProtoMap(level), index);

        public static LevelPrototype GetRandomProto()
        {
            int index = Random.Range(0, levels.Length);
            return GetProtoByIndex(index);
        }

        public static LevelPrototype GetProtoByIndex(int index)
        {
            //Debug.Log(index);
            if (index < 0 || index >= levels.Length)
                throw new ArgumentException();

            var level = levels[index];
            return StringLevelToProto(level, index);
        }

        
    }
}
