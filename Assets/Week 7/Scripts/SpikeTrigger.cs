using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;


public class SpikeTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        //get player component and give damage of 10 to health
        if (other.transform.name == "Player")
        {
            other.GetComponent<Player>().TakeDamage(10);
        }
        
    }

    //private void OnEnable()
    //{
    //    GameManager.endGame.AddListener(ResetTrap);
    //}

    //private void OnDisable()
    //{
    //    GameManager.endGame.RemoveListener(ResetTrap);
    //}

    //private void ResetTrap()
    //{
    //    gameObject.SetActive(true);
    //}


}
