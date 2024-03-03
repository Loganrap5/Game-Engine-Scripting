using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//add using directive thats on line 4 
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;

    //from the mappings file i created
    PlayerControllerMappings mappings;
    Rigidbody rb;

    [SerializeField] float jumpForce = 200f;
    const float speed = 5.5f;

    private void Awake()
    {
        mappings = new PlayerControllerMappings();

        moveAction = mappings.Player.Move;
        jumpAction = mappings.Player.Jump;

        jumpAction.performed += Jump;

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();

        jumpAction.performed += Jump;
    }


    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();

        jumpAction.performed -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() == false)
        {
            return;
        }
        //returns a vector2 with values of the format (x,y) where
        //x represents our input from A and D
        //y represents our input from W and S
        //on a range from -1 to +1
        Vector2 input = moveAction.ReadValue<Vector2>();
        input *= speed;

        //transform.position = new Vector3(transform.position.x + input.x
        //                                    ,transform.position.y
        //                                    ,transform.position.z + input.y);

        rb.velocity = new Vector3(input.x, rb.velocity.y, input.y);
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded() == false) 
        {
            return;
        }
        rb.AddForce(Vector3.up * jumpForce);
    }

    bool IsGrounded()
    {
        int layerMask = 1 << 3;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            return false;
        }
    }
}
