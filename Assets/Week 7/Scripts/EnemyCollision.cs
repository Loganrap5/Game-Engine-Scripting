using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCollision : MonoBehaviour
{
    public Material materialDamaged;
    public Material materialNormal;

    private MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    //for listenting to triggers
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Bullet")
        //{
            mr.material = materialDamaged;

            DOVirtual.DelayedCall(0.1f, () =>
            {
                mr.material = materialNormal;
            });
        //}
    }

    //this is for listening to physics
    private void OnCollisionEnter(Collision collision)
    {
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        Debug.Log("The bullet damaged me!!");
    }
}
