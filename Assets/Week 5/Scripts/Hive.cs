using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hive : MonoBehaviour
{
    private float honeyRate = 15f;

    //bee prefab
    [SerializeField] public Bee beePrefab;

    private int startingBeeAmount = 1;

    [SerializeField] private int honeyStored;
    [SerializeField] private int nectarStored;

    [SerializeField]private float honeyCountdown;

    //list of bees to have for the hive
    private List<Bee> bees = new List<Bee>();

    //start the game and instantiate Bees to the list
    //as well as set the countdown to the rate
    void Start()
    {
        honeyCountdown = honeyRate;
        for(int i = 0; i < startingBeeAmount; i++)
        {
            Bee newBee = Instantiate(beePrefab);
            newBee.Init(this);
            bees.Add(newBee);
        }
    }
    
    void Update()
    {
        //try to count down if it has an nectar stored
       if(nectarStored > 0)
        {
            honeyCountdown -= Time.deltaTime;
            if(honeyCountdown <= 0f)
            {
                nectarStored--;
                honeyStored++;
                honeyCountdown = honeyRate;
            }
            
        }
    }
    
    public void GiveNectar()
    {
        nectarStored++;
        if (honeyCountdown >= honeyRate)
        {
            honeyCountdown = honeyRate;
        }
    }
}
