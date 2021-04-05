namespace CryptoGetAndSet.Helpers
{
    using System;

    internal static class ArrayExtensions
    {
        internal static T[] SubArray<T>(this T[] Array, int Index, int Length)
        {
            var Result = new T[Length];
            System.Array.Copy(Array, Index, Result, 0, Length);
            return Result;
        }
    }
}
