    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multithreading
{
    static class QuickSort
    {

        public static void ParallelSorting(int [] array, int threads)
        {
            int[,] indexes = GetIndexes(array.Length, threads);

            for (int i = 0; i < threads; i++)
            {
                Task t = Task.Run(() => RecursiveQuickSort(array, indexes[i, 0], indexes[i, 1] ));
                t.Wait();
            }
        }

        private static int[,] GetIndexes(int length, int threads)
        {
            int[,] indexes = new int[threads, 2];
            int size = length / threads;
            int right = -1;
            for (int i = 0; i < threads; i++)
            {
                int left = right + 1;
                right = left + size - 1;
                indexes[i, 0] = left;
                indexes[i, 1] = right;
            }
            indexes[threads - 1, 1] = length - 1;
            return indexes;
        }

        private static void RecursiveQuickSort(int[] array, int left, int right)
        {

            if (left < right)
            {
                int pivot = Partition(array, left, right);
                RecursiveQuickSort(array, left, pivot - 1);
                RecursiveQuickSort(array, pivot + 1, right);
            }
        }

        private static int Partition(int[] array, int start, int end)
        {
            int pivot = array[end];

            int pIndex = start;

            for (int i = start; i < end; i++)
            {
                if (array[i] <= pivot)
                {
                    Swap(array, i, pIndex);
                    pIndex++;
                }
            }

            Swap(array, pIndex, end);
            return pIndex;
        }

        private static void Swap(int[] array, int fIndex, int sIndex)
        {
            int temp = array[fIndex];
            array[fIndex] = array[sIndex];
            array[sIndex] = temp;
        }


    }
}
