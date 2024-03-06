using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

//add using directive thats on line 4 

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] InputAction moveAction;
        [SerializeField] InputAction jumpAction;

        //from the mappings file I created
        PlayerControllerMappings playerControls;
        Rigidbody rb;

        private float mouseDeltaX = 0f;
        private float mouseDeltaY = 0f;
        private float cameraRotX = 0f;
        private int rotDir = 0;
        private bool grounded;


        //public PlayerController playerControls;

        [SerializeField] float jumpForce = 200f;
        const float speed = 5.5f;

        private InputAction move;
        private InputAction look;
        private InputAction fire;

        private void Awake()
        {
            playerControls = new PlayerControllerMappings();

            moveAction = playerControls.Player.Move;
            jumpAction = playerControls.Player.Jump;

            jumpAction.performed += Jump;

            rb = GetComponent<Rigidbody>();

            move = playerControls.Player.Move;

        }

        private void OnEnable()
        {
            moveAction.Enable();
            jumpAction.Enable();

            fire.Enable();
            look.Enable();

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

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
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

        void HandleHorizontalRotation()
        {
            //mouseDeltaX = LookAtConstraint.ReadValue<Vector2>.x
        }
    }
}
