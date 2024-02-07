using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayCode : MonoBehaviour
{
    [SerializeField]
    int[][] scoresArray = new int[4][];

    [ContextMenu("Execute Test")]
    void Execute()
    {
        scoresArray[0] = new int[4] { 1, 2, 3, 4 };
        scoresArray[1] = new int[4] { 5, 6, 7, 8 };
        scoresArray[2] = new int[4] { 9, 10, 11, 12 };
        scoresArray[3] = new int[4] { 13, 14, 15, 16 };

        //for (int i = 0; i < scoresArray.Length; i++)
        //{
        //    Debug.LogFormat("The number is {0}. Noice", scoresArray[i]);
        //}

        //for each nested array in our arrays
        for (int i = 0; i < scoresArray.Length; i++)
        {
            for (int j = 0; i < scoresArray[i].Length; j++)
            {
                Debug.LogFormat("The Number is {0}", scoresArray[i][j]);
            }
        }
    }
}
