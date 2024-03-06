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
         Destroy(this.gameObject);   
    }

    private void Update()
    {
        transform.Rotate(Vector3.down, rotateSpeed * Time.deltaTime);
    }
}
