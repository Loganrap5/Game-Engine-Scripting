using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;

public class Key : MonoBehaviour
{
    public float rotateSpeed = 25f;

    private void OnTriggerEnter(Collider other)
    {   
         if (other.transform.name == "Player")
         {
             other.GetComponent<Player>().CollectKey();
         }
         gameObject.SetActive(false);
        
    }

    private void Update()
    {
        transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
    }

    //subscribe the keys to the Restart Game event 
    private void OnEnable()
    {
        GameManager.restartGame.AddListener(EnableKey);
    }

    private void OnDisable()
    {
        GameManager.restartGame.RemoveListener(DisableKey);
    }


    private void EnableKey()
    {
        gameObject.SetActive(true);
    }

    private void DisableKey()
    {
        gameObject.SetActive(false);
    }
}
