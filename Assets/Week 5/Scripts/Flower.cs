using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]private float nectarRate = 10f;
    [SerializeField]private float nectarCountdown = 10f;

    public bool hasNectar = false;

    

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        nectarCountdown = nectarRate;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateFlowerColor();

    }

   
    void Update()
    {
        if (hasNectar == false)
        {
            //start count
            nectarCountdown -= Time.deltaTime;
            if(nectarCountdown <= 0)
            {
                hasNectar = true;
                nectarCountdown = nectarRate;
                UpdateFlowerColor();
            }
        }
    }

    public bool TakeNectar()
    {
        if(hasNectar == true)
        {
            hasNectar = false;
            UpdateFlowerColor();

            //reset timer to normal
            nectarCountdown = nectarRate;
            return true;
        }
        return false;
    }

    public void UpdateFlowerColor()
    {
        if (hasNectar == true)
        {
            spriteRenderer.color = Color.white;
        }
        if(hasNectar == false)
        {
            spriteRenderer.color = Color.gray;
        }
    }

}
