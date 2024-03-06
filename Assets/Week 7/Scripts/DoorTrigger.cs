using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Week7;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject playerObject;

    Vector3 origin;
    Vector3 target;
    bool isOpening;
    float alpha;

    bool unlocked = false;

    private void Awake()
    {
        origin = transform.position;
        target = origin + (Vector3.up * 5);

        //make the trigger invisible
        gameObject.GetComponent<Renderer>().enabled = false;

    }

    private void Update()
    {
        alpha += isOpening ? Time.deltaTime : -Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        door.transform.position = Vector3.Lerp(origin, target, alpha);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = playerObject.GetComponent<Player>();
        if (unlocked == true)
        {
            isOpening = true;
        }
        else
        {
            if (player.keys > 0)
            {
                isOpening = true;
                unlocked = true;
                player.keys--;
                Debug.Log("Door opened");
            }
            else if (player.keys == 0)
            {
                Debug.Log("Locked");
            }
        }


        //door.gameObject.SetActive(false);
        //door.transform.position = transform.position + (Vector3.up * 10);


    }



    private void OnTriggerExit(Collider other)
    {
        isOpening = false;
        //door.gameObject.SetActive(true);
        //door.transform.position = origin;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
