using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Misc
{
    static class Tools
    {
        public delegate R ExplicitCaster<S, R>(S source);

        public static R[,] CastArr<S, R>(S[,] source, ExplicitCaster<S, R> castFunc)
        {
            var width = source.GetLength(0);
            var height = source.GetLength(1);
            return CastArr(source, width, height, castFunc);
        }

        public static R[,] CastArr<S, R>(S[,] source, int width, int height, ExplicitCaster<S, R> caster)
        {
            var result = new R[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    result[x, y] = caster(source[x, y]);
            return result;
        }
    }
}
