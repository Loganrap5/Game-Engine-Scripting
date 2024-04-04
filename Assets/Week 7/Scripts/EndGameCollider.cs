using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week7
{
    public class EndGameCollider : MonoBehaviour
    {
        [SerializeField] GameObject playerObject;

        private void Awake()
        {
            //make the trigger invisible
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("entered");
            Player player = playerObject.GetComponent<Player>();
            if (other.transform.name == "Player")
            {
                //invoke the endgame event
                GameManager.EndGame();
            }
            
        }
    }
}
