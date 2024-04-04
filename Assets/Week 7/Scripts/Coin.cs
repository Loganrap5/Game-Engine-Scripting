using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;

namespace Week7
{
    public class Coin : MonoBehaviour
    {
        private float rotateSpeed = 25f;

        //initial position of each coin
        private Vector3 initialPosition;
        private void Start()
        {
            initialPosition = transform.position;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.name == "Player")
            {
                other.GetComponent<Player>().CollectCoin();
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        //subscribe the coins to the Restart Game event 
        private void OnEnable()
        {
            GameManager.restartGame.AddListener(EnableCoin);
        }

        private void OnDisable()
        {
            GameManager.restartGame.RemoveListener(DisableCoin);
        }

        private void EnableCoin()
        {
            gameObject.SetActive(true);
            //move coin back to origianl position
            transform.position = initialPosition;

        }

        private void DisableCoin()
        {
            gameObject.SetActive(false);
            transform.position = initialPosition;
        }




    }
}