using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTN.BITS.FGTRACK.DAL
{
    public static class ArrayOf<T>
    {
        public static T[] Create(int size, T initialValue)
        {
            T[] array = (T[])Array.CreateInstance(typeof(T), size);
            for (int i = 0; i < array.Length; i++)
                array[i] = initialValue;
            return array;
        }
    }
}
