using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListInsertionSort : MonoBehaviour
{
    List<int> numberList = new List<int> { 7, 1, 9, 6, 0 };

    void Start()
    {
        print($"Original List: {ListToString(numberList)}");
        Sort(numberList);
        print($"List Sorted Finished: {ListToString(numberList)}");
    }
    string ListToString(List<int> list) 
    {
        return string.Join(", ", list);
    }


    void Sort(List<int> list)
    {
        // Loop through each index, starting with 1,
        // dont need to compare the first index
        
        for (int i = 1; i < list.Count; i++) 
        {
            int indexToCheck = i; // store the index to check

            int currentValue = list[indexToCheck]; // store the current value
            int previousValue = list[indexToCheck - 1]; // store the previous value

            // check if current value is lower than the previous one
            while (currentValue < previousValue)
            {
                // if it is lower, switch them
                list[indexToCheck] = previousValue;
                list[indexToCheck - 1] = currentValue;

                // print to see each sorting step
                print($"List Sorted: {ListToString(numberList)}");

                // reduce 1 from the index, so we keep checking until the current
                // value is higher than the previous one
                indexToCheck--;
                
                // check if got to the start of the list and exit if we did
                if(indexToCheck <= 0)
                    break;
                
                //set the new previous value, so we can compare it
                previousValue = list[indexToCheck - 1];

            }
        }
    }

}
