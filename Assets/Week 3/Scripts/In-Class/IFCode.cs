using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class IFCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
         
    }
    public int apples = 4;
    public int bananas = 2;

    [ContextMenu("Execute If Test")]
    void ExecuteIfTest()
    {
        if (apples == 4 && bananas == 2)
        {
            Debug.Log($"We have {apples} apples. And we have {bananas} bananas.");
        }
        else
        {
            Debug.Log("this is false");
        }
    }

    private void Awake()
    {
        
    }
    private void Update()
    {
        
    }
}
