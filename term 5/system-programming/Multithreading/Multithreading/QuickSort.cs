using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    public class QuickSort
    {
        private int[] mInts;

        Thread myThread = null;
        int numThreads = 0;
        int maxThreads = 10;

        public void QuickSorting()
        {
            QuickSorting(mInts, 0, mInts.Length - 1);
        }

        public QuickSort(int[] ints, int numThreads)
        {
            mInts = ints;
            this.numThreads = numThreads;
        }

        private int Partition(int[] ints, int left, int right)
        {
            int pivotIndex = (right + left) / 2;
            int pivotValue = ints[pivotIndex];

            Swap(ints, right, pivotIndex);

            int storeIndex = left;

            for (int i = left; i <= right - 1; i++)
            {
                if (ints[i] < pivotValue)
                {
                    Swap(ints, storeIndex, i);
                    storeIndex++;
                }
            }

            Swap(ints, storeIndex, right);

            return storeIndex;
        }

        private void Swap(int[] ints, int x, int y)
        {
            int temp = ints[x];
            ints[x] = ints[y];
            ints[y] = temp;
        }

        private void QuickSorting(int[] ints, int left, int right)
        {
            if (left < right)
            {
                int newIndex = Partition(ints, left, right);

                if (numThreads < maxThreads)
                {
                    numThreads++;
                    myThread = new Thread(new ParameterizedThreadStart(startSort));
                    myThread.Start(new SortParameters(this, ints, left, newIndex - 1));
                }
                else
                {
                    QuickSorting(ints, left, newIndex - 1);
                }
                QuickSorting(ints, newIndex + 1, right);
            }
        }

        static void startSort(Object obj)
        {
            SortParameters sortParams = (SortParameters)obj;
            sortParams.instance.QuickSorting(sortParams.ints, sortParams.left, sortParams.right);
        }

        public class SortParameters
        {
            public QuickSort instance;
            public int[] ints;
            public int left;
            public int right;

            public SortParameters(QuickSort instance, int[] ints, int left, int right)
            {
                this.instance = instance;
                this.ints = ints;
                this.left = left;
                this.right = right;
            }
        }
    }
}
