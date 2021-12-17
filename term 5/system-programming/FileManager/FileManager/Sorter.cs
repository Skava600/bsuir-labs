using System;
using System.Threading.Tasks;

namespace FileManager
{
    /// <summary>
    /// Class for quick sort operations.
    /// </summary>
    public static class Sorter
    {
        /// <summary>
        /// Quick sort using standart algorithm.
        /// </summary>
        /// <typeparam name="T">Type of array being sorted.</typeparam>
        /// <param name="arr">Array being sorted.</param>
        /// <param name="left">Extreme left element.</param>
        /// <param name="right">Extreme right element.</param>
        public static void QuicksortSequential<T>(T[] arr, int left, int right)
            where T : IComparable<T>
        {
            if (right > left)
            {
                int pivot = Partition(arr, left, right);
                QuicksortSequential(arr, left, pivot - 1);
                QuicksortSequential(arr, pivot + 1, right);
            }
        }

        /// <summary>
        /// Quick sort with parallel threading.
        /// </summary>
        /// <typeparam name="T">Type of array being sorted.</typeparam>
        /// <param name="arr">Array being sorted.</param>
        /// <param name="left">Extreme left element.</param>
        /// <param name="right">Extreme right element.</param>
        public static void QuicksortParallelOptimised<T>(T[] arr, int left, int right) where T : IComparable<T>
        {
            const int SEQUENTIAL_THRESHOLD = 2048;
            if (right > left)
            {
                if (right - left < SEQUENTIAL_THRESHOLD)
                {
                    QuicksortSequential(arr, left, right);
                }
                else
                {
                    int pivot = Partition(arr, left, right);
                    Parallel.Invoke(
                        () => QuicksortParallelOptimised(arr, left, pivot - 1),
                        () => QuicksortParallelOptimised(arr, pivot + 1, right));
                }
            }
        }

        private static int Partition<T>(T[] arr, int low, int high) where T : IComparable<T>
        {
            int pivotPos = (high + low) / 2;
            T pivot = arr[pivotPos];
            ArraySwap(arr, low, pivotPos);

            int left = low;
            for (int i = low + 1; i <= high; i++)
            {
                if (arr[i].CompareTo(pivot) < 0)
                {
                    left++;
                    ArraySwap(arr, i, left);
                }
            }

            ArraySwap(arr, low, left);
            return left;
        }

        private static void ArraySwap<T>(T[] arr, int i, int j)
        {
            T tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
}
