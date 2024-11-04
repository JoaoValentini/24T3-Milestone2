using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrayBubbleSort : MonoBehaviour
{
    int[] numberArray = { 5, 3, 8, 4, 2 };

    void Start()
    {
        print("Original array: " + ArrayToString(numberArray));
        for (int i = 1; i < numberArray.Length; i++)
        {
           numberArray = Sort(numberArray, i);
        }

        print("Finished sorting array: " + ArrayToString(numberArray));
    }

    int[] Sort(int[] array, int indexToCheck)
    {
        if(indexToCheck <= 0 || indexToCheck >= array.Length) // check if the index is valid
            return array;

        // store the current value and the value to compare
        int value = array[indexToCheck];
        int previousValue = array[indexToCheck - 1];

        if(value < previousValue) // compare values
        {
            // if value is lower, switch them and call this method again 
            // subtracting 1 from the index so it keeps comparing until
            // the value is in the correct position, meaning every value
            // before is lower

            array[indexToCheck] = previousValue;
            array[indexToCheck - 1] = value;
            
            // Print the array so we can see each comparisson
            print(ArrayToString(array));
            array = Sort(array, indexToCheck - 1);
        }

        return array;
    }

    string ArrayToString(int[] array)
    {
        return string.Join(", ", array);
    }

   
}
