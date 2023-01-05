using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntp_bomb
{
    public static class PredicateMulti
    {
        public static bool AllMulti<T>(this T[,] arr, Predicate<T> predicate)
        {
            for (int i = 0; i < arr.Rank; i++)
            {
                for (int j = 0; j < arr.GetLength(i); j++)
                {
                    if (!predicate(arr[i, j]))
                        return false;
                }
            }

            return true;
        }

        public static void ResetStates<T>(this T[,] arr)
        {
            for (int i = 0; i < arr.Rank; i++)
            {
                for (int j = 0; j < arr.GetLength(i); j++)
                {
                    arr[i, j] = default;
                }
            }
        }
        public static T[,] Transform<T>(this T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }
    }
}
