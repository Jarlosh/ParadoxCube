using Assets.GameStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.GameStuff.Protos
{
    public class ProtoCollection
    {



        static string[] levels = new string[]
        {
        
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
        111333333",
        @"
        111111111
        100010001
        101010101
        100000001
        111010111
        100010001
        101111101
        120000001
        111111111",
        @"
        111111111
        100011001
        111000011
        100001011
        100000001
        111111101
        120000001
        111111111",
        @"
        111111111
        100112111
        110010011
        100011011
        101000001
        100000001
        111111111",
        @"
        111111111
        110000001
        120000001
        100111001
        100111001
        100000001
        100000011
        111111111",
//        @" bugged
//        111111111
//        120010001
//        101010101
//        101010101
//        100000001
//        111010111
//        100000001
//        101010101
//        101010101
//        100010001
//        111111111",
        @"
        111111111
        100010001
        101010101
        100000001
        111010111
        111010111
        100010001
        121111101
        100000001
        111111111",
        @"
        111111111
        100000001
        100111011
        100000001
        110000001
        100000011
        100000001
        100111101
        120000001
        111111111",
        @"
        111111111
        110000011
        100010001
        100111001
        100111001
        101111101
        100111001
        100010001
        120010001
        110000011
        111111111",
        @"
        111111111
        110000001
        100010101
        100000101
        101000001
        100001101
        121110001
        110000111
        110100001
        110011111
        111010001
        111000111
        111111111",
        @"
        111111111
        100100001
        100101011
        100000011
        110000001
        100000001
        101111111
        101000001
        100021101
        101111101
        100000001
        111111111",
        @"
        111111111
        100111001
        100010001
        101010101
        101010101
        101010101
        101010101
        100000021
        111111111",
        @"
        111111111
        110000001
        100000001
        100110101
        100010101
        100000101
        101011101
        121000001
        111111111",
        @"
        111111111
        110000111
        100000011
        100000001
        110111011
        110000001
        100000001
        100000021
        111111111"
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
            Debug.Log(index);
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
