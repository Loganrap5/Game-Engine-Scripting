using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;

public class Coin : MonoBehaviour
{
    private float rotateSpeed = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            other.GetComponent<Player>().CollectCoin();
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

}
