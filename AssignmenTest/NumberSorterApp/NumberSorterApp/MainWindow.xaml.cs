using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumberSorterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello World");
            quickSortResult.Items.Clear();
            var inputString = inputNumber.Text;

            
            int [] arr = convertStringToArray(inputString);
            var lastPosition = arr.Length - 1;

            // Quick Sort
            var quickSortwatch = System.Diagnostics.Stopwatch.StartNew();
            var quickSortArray = quickSort(arr, 0, lastPosition);
            foreach (int value in quickSortArray)
                quickSortResult.Items.Add(value);
            quickSortwatch.Stop();
            var quickSortMs = quickSortwatch.ElapsedMilliseconds;
            quickSortTime.Text = Convert.ToString(quickSortMs) + " ms";

            // Bubble Sort
            var bubbleSortwatch = System.Diagnostics.Stopwatch.StartNew();
            var bubbleSortArray = bubbleSort(arr, 0, lastPosition);
            foreach (int value in bubbleSortArray)
                bubbleSortResult.Items.Add(value);
            bubbleSortwatch.Stop();
            var bubbleSortms = bubbleSortwatch.ElapsedMilliseconds;
            bubbleSortTime.Text = Convert.ToString(bubbleSortms) + " ms";

            // Merger Sort
            var MergerSortwatch = System.Diagnostics.Stopwatch.StartNew();
            var MergerSortArray = mergeSort(arr);
            foreach (int value in MergerSortArray)
                mergerSortResult.Items.Add(value);
            MergerSortwatch.Stop();
            var MergerSortms = MergerSortwatch.ElapsedMilliseconds;
            mergerSortTime.Text = Convert.ToString(MergerSortms) + " ms";

            // Gcf Sort
            var gcfSortwatch = System.Diagnostics.Stopwatch.StartNew();
            var gcfSortArray = gcf(arr);
            foreach (int value in gcfSortArray)
                gcfSortResult.Items.Add(value);
            gcfSortwatch.Stop();
            var gcfSortms = gcfSortwatch.ElapsedMilliseconds;
            GcfSortTime.Text = Convert.ToString(gcfSortms) + " ms";
        }

        private static int gcf(int a, int b)
        {
            while (b > 0)
            {
                int temp = b;
                b = a % b; // % is remainder
                a = temp;
            }
            return a;
        }
        private static int[] gcf(int[] input)
        {
            var collection = new Dictionary<int, int>() { };
            var data = new Dictionary<int, Dictionary<int, int>>();
            var result = new int[] { };
            for (int i = 0; i < input.Count(); i++)
            {
                int number1 = input[i];
                int gdcNew = gcf(number1, input.Length + 1);
                collection.Add(gdcNew, number1);
                collection = collection.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);
                data.Add(i, collection);
                int[] newArr = (new List<int>(collection.Values)).ToArray();
                result = newArr;
            }
            return result;
        }

        private static int[] convertStringToArray(string input)
        {
            int[] nums = input.Split(',').Select(int.Parse).ToArray();
            return nums;
        }

        static public int Partition(int[] arr, int left, int right)
        {
            int pivot;
            pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    int temp = arr[right];
                    arr[right] = arr[left];
                    arr[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        static public int[] quickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    quickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    quickSort(arr, pivot + 1, right);
                }
            }
            return arr;

        }

        static public int[] bubbleSort(int[] arr, int left, int right)
        {
            int temp;
            for (int j = 0; j <= arr.Length - 2; j++)
            {
                for (int i = 0; i <= arr.Length - 2; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
            return arr;
        }

        public static int[] mergeSort(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];
            if (array.Length <= 1)
                return array;
            int midPoint = array.Length / 2;
            left = new int[midPoint];
            if (array.Length % 2 == 0)
                right = new int[midPoint];
            else
                right = new int[midPoint + 1];
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];
            int x = 0;
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            left = mergeSort(left);
            right = mergeSort(right);
            result = merge(left, right);
            return result;
        }

        public static int[] merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            //
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }
    }
}
