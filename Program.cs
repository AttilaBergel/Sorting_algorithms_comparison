using System;
using System.Diagnostics;

class SortingAlgorithms
{

    static int[] array = new int[] { 4,7,9,6,0,1,3,8,2,5 };

    public static void Main(string[] args)
    {
        Console.WriteLine("Bubble Sort:");
        TestAndPrintResults(BubbleSort);

        Console.WriteLine("Selection Sort:");
        TestAndPrintResults(SelectionSort);

        Console.WriteLine("Insertion Sort:");
        TestAndPrintResults(InsertionSort);

        Console.WriteLine("Merge Sort:");
        TestAndPrintResults(MergeSort);

        Console.WriteLine("Quick Sort:");
        TestAndPrintResults(QuickSort);

        Console.WriteLine("Heap Sort:");
        TestAndPrintResults(HeapSort);

        Console.ReadLine();
    }

    private static void TestAndPrintResults(Action<int[]> sortingAlgorithm)
    {
        int[] copiedArray = new int[array.Length];
        Array.Copy(array, copiedArray, array.Length);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        sortingAlgorithm(copiedArray);
        stopwatch.Stop();

        PrintResults(copiedArray, stopwatch);
    }

    private static void TestAndPrintResults(Action<int[], int, int> sortingAlgorithm)
    {
        int[] copiedArray = new int[array.Length];
        Array.Copy(array, copiedArray, array.Length);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        sortingAlgorithm(copiedArray, 0, copiedArray.Length - 1);
        stopwatch.Stop();

        PrintResults(copiedArray, stopwatch);
    }

    private static void PrintResults(int[] copiedArray, Stopwatch stopwatch)
    {
        Console.WriteLine("Disordered array: " + string.Join(", ", array));
        Console.WriteLine("Sorted array:     " + string.Join(", ", copiedArray));
        Console.WriteLine("Time elapsed: {0} ns, {1} ticks", stopwatch.ElapsedTicks * 100, stopwatch.ElapsedTicks);
        Console.WriteLine();
    }

    public static void BubbleSort(int[] arr)
    {
        // Bubble sort implementation
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (arr[j] > arr[j + 1])
                {
                    // swap arr[j] and arr[j+1]  
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
    }

    public static void SelectionSort(int[] arr)
    {
        // Selection sort implementation
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
                if (arr[j] < arr[minIndex])
                    minIndex = j;
            // Swap the found minimum element with the first element  
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }

    public static void InsertionSort(int[] arr)
    {
        // Insertion sort implementation
        int n = arr.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;
            // Move elements of arr[0..i-1], that are greater than key, to one position ahead of their current position  
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }

    public static void MergeSort(int[] arr, int left, int right)
    {
        // Merge sort implementation
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }
    private static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;
        int[] L = new int[n1];
        int[] R = new int[n2];
        for (int i = 0; i < n1; ++i)
            L[i] = arr[left + i];
        for (int j = 0; j < n2; ++j)
            R[j] = arr[mid + 1 + j];
        int k = left;
        int i1 = 0, i2 = 0;
        while (i1 < n1 && i2 < n2)
        {
            if (L[i1] <= R[i2])
            {
                arr[k] = L[i1];
                i1++;
            }
            else
            {
                arr[k] = R[i2];
                i2++;
            }
            k++;
        }
        while (i1 < n1)
        {
            arr[k] = L[i1];
            i1++;
            k++;
        }
        while (i2 < n2)
        {
            arr[k] = R[i2];
            i2++;
            k++;
        }
    }

    public static void QuickSort(int[] arr, int low, int high)
    {
        // Quick sort implementation
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }
    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);
        for (int j = low; j <= high - 1; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                // swap arr[i] and arr[j]  
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        // swap arr[i + 1] and arr[high])  
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;
        return (i + 1);
    }

    public static void HeapSort(int[] arr)
    {
        // Heap sort implementation
        int n = arr.Length;
        // Build heap (rearrange array)  
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);
        // One by one extract an element from heap  
        for (int i = n - 1; i >= 0; i--)
        {
            // Move current root to end  
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            // call max heapify on the reduced heap  
            Heapify(arr, i, 0);
        }
    }
    private static void Heapify(int[] arr, int n, int i)
    {
        int largest = i; // Initialize largest as root  
        int l = 2 * i + 1; // left = 2*i + 1  
        int r = 2 * i + 2; // right = 2*i + 2  
                           // If left child is larger than root  
        if (l < n && arr[l] > arr[largest])
            largest = l;
        // If right child is larger than largest so far  
        if (r < n && arr[r] > arr[largest])
            largest = r;
        // If largest is not root  
        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;
            // Recursively heapify the affected sub-tree  
            Heapify(arr, n, largest);
        }
    }
}
