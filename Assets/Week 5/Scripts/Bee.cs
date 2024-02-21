using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bee : MonoBehaviour
{
    private Hive hive;

    public void Init(Hive _hive)
    {
        this.hive = _hive;
    }

    public void Start()
    {
        //make sure to start with the bees checking flowers 
        //so they start to move
        CheckAnyFlower();
    }

    public void CheckAnyFlower()
    {
        //store flower types in an array, have bees move around them and give nectar if flowers ready
        Flower[] flower = FindObjectsOfType<Flower>();
        if(flower.Length > 0)
        {
            Flower _flower = flower[Random.Range(0, flower.Length)];
            transform.DOMove(_flower.transform.position, 1f).OnComplete(() =>
            {
                if(_flower.TakeNectar())
                {
                    transform.DOMove(hive.transform.position, 1f).OnComplete(() =>
                    {
                        hive.GiveNectar();
                        CheckAnyFlower();
                    });
                }
                else 
                { 
                    CheckAnyFlower(); 
                }


            }).SetEase(Ease.Linear);
        }
    }
}
