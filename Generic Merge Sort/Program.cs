using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Generic_Merge_Sort
{
    class Program
    {


        // Some improvements that I'm not going to go through and set up now:
        // doubling the size of the list and using indicies rather than creating sublist objects.
        //Using a better sort for small lists rather than merge sort

        /// <summary>
        /// Merges two comparable lists in reverse order to their CompareTo method
        /// </summary>
        /// <param name="listA">First list to merge</param>
        /// <param name="listB">Second list to merge</param>
        /// <returns>A new merged list containing ListA and ListB</returns>
        static List<IComparable> mergeLists(List<IComparable> listA, List<IComparable> listB)
        {
            List<IComparable> retList = new List<IComparable>();
            while ((listA.Count != 0) && (listB.Count != 0))//While one list has values
            {
                if (listA[0].CompareTo(listB[0]) > 0)//If A belongs before B, add A to retList
                {
                    retList.Add(listA[0]);
                    listA.RemoveAt(0);
                }
                else //Else add B to ret list
                {
                    retList.Add(listB[0]);
                    listB.RemoveAt(0);
                }
            }
            if (listB.Count == 0) { retList.AddRange(listA); }
            else { retList.AddRange(listB); }//Append remaining list info after one list becomes empty
            return retList;

        }

        /// <summary>
        /// Sorts a given comparable list via merge sort.
        /// </summary>
        /// <param name="list">The list to sort</param>
        /// <returns>A new sorted list containing the same values as the initial list.</returns>
        static List<IComparable> genericMergeSort(List<IComparable> list)
        {
            if (list.Count <= 1) { return list; }//List is sorted;
            else
            {
                int middleIndex = (list.Count) / 2;//Find middle
                List<IComparable> listA = list.GetRange(0, middleIndex);
                List<IComparable> listB = list.GetRange(middleIndex, list.Count - middleIndex);//Divide list in 2
                listA = genericMergeSort(listA);
                listB = genericMergeSort(listB);//sort sublists
                return mergeLists(listA, listB);//merge sublists and return;
            }
           
        }

        static void Main(string[] args)
        {
            Boolean errorOccurred = false;
            List<Int32> listInts = new List<Int32> { 8, 12, 4, 6, 9 };
            List<Double> listDoubles= new List<Double> { 8, 12, 4, 6, 9 };//Init values
           
            List<IComparable> comparableList = (listInts.Cast<IComparable>()).ToList<IComparable>();//Complicated cast to get this to work. This probably is inefficient as anything, but interesting it works.
            comparableList = genericMergeSort(comparableList);//Merge sort it
            listInts = (comparableList.Cast<Int32>()).ToList<Int32>();//Return list Ints to standard form.
            if ((listInts[0] != 4) && (listInts[1] != 6) && (listInts[2] != 8) && (listInts[3] != 9) && (listInts[4] != 12)) { Console.WriteLine("listInts failed to sort in decending order"); errorOccurred = true; }

            comparableList = (listDoubles.Cast<IComparable>()).ToList<IComparable>();
            comparableList = genericMergeSort(comparableList);
            listDoubles = (comparableList.Cast<Double>()).ToList<Double>();
            if ((listDoubles[0] != 4) && (listDoubles[1] != 6) && (listDoubles[2] != 8) && (listDoubles[3] != 9) && (listDoubles[4] != 12)) { Console.WriteLine("listDoubles failed to sort in decending order"); errorOccurred = true; }
            
            if (!errorOccurred) { Console.WriteLine("No errors!"); }
            Console.ReadKey();

            
        }
    }
}
