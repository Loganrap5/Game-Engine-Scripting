using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Week7
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float speed = 5.0f;
        [SerializeField] float rotation = 5.0f;
        [SerializeField] float jumpForce = 10f;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform bulletSpawnTransform;

        [SerializeField] TextMeshProUGUI healthText;
        [SerializeField] TextMeshProUGUI keysText;
        [SerializeField] TextMeshProUGUI coinsText;


        [SerializeField] public int health;
        [SerializeField] public int keys;
        [SerializeField] public int coins;

        public PlayerControls playerControls;

        private float mouseDeltaX = 0f;
        private float mouseDeltaY = 0f;
        private float cameraRotX = 0f;
        private int rotDir = 0;
        private bool grounded;

        private InputAction move;
        private InputAction look;
        private InputAction jump;
        private InputAction fire;

        Rigidbody rb;

        //inital position of player for restarting
        private Vector3 initialPosition;
        private void Start()
        {
            initialPosition = transform.position;
        }

        private void Awake()
        {
            playerControls = new PlayerControls();

            rb = GetComponent<Rigidbody>();
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;

            move = playerControls.Player.Move;
            jump = playerControls.Player.Jump;
            look = playerControls.Player.Look;
            fire = playerControls.Player.Fire;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if(health <= 0)
            {
                GameManager.EndGame();
            }
        }

        public void CollectKey()
        {
            keys++;
        }

        public void CollectCoin()
        {
            coins++;
        }

        private void ResetPlayer()
        {
            health = 100;
            keys = 0;
            coins = 0;

            //unpause the characters movement
            move.Enable();
            look.Enable();
            jump.Enable();
            fire.Enable();

            //move player back to beginning
            transform.position = initialPosition;
        }

        //puase player function so it doesnt allow movement while the menu is enabled
        private void PausePlayer()
        {
            move.Disable();
            look.Disable();
            jump.Disable();
            fire.Disable();
        }

        private void OnEnable()
        {
            move.Enable();
            look.Enable();
            fire.Enable();
            jump.Enable();
            jump.performed += Jump;
            fire.performed += Fire;
            
            //Events for pausing game as well as restarting
            GameManager.restartGame.AddListener(ResetPlayer);

            GameManager.endGame.AddListener(PausePlayer);
        }

        private void OnDisable()
        {
            move.Disable();
            jump.Disable();
            look.Disable();
            fire.Disable();

            //Events for pausing game as well as restarting
            GameManager.restartGame.RemoveListener(ResetPlayer);

            GameManager.endGame.RemoveListener(PausePlayer);
        }

        private void PlayerUI()
        {
            healthText.text = $"Health : {health}";
            keysText.text = $"Keys : {keys}";
            coinsText.text = $"Coins : {coins}";
        }

        private void Update()
        {
            HandleHorizontalRotation();
            HandleVerticalRotation();
            //handle player UI
            PlayerUI();
        }

        private void FixedUpdate()
        {
            grounded = IsGrounded();

            HandleMovement();
        }

        void HandleMovement()
        {
            if (grounded == false) return;

            Vector2 axis = move.ReadValue<Vector2>();

            Vector3 input = (axis.x * transform.right) + (transform.forward * axis.y);

            input *= speed;

            rb.velocity = new Vector3(input.x, rb.velocity.y, input.z);
        }

        bool IsGrounded()
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 3;

            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
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

            mouseDeltaX = look.ReadValue<Vector2>().x;

            if (mouseDeltaX != 0)
            {
                rotDir = mouseDeltaX > 0 ? 1 : -1;

                transform.eulerAngles += new Vector3(0, rotation * Time.deltaTime * rotDir, 0);
            }
        }

        void HandleVerticalRotation()
        {
            mouseDeltaY = look.ReadValue<Vector2>().y;

            if (mouseDeltaY != 0)
            {
                rotDir = mouseDeltaY > 0 ? -1 : 1;

                cameraRotX += rotation * Time.deltaTime * rotDir;
                cameraRotX = Mathf.Clamp(cameraRotX, -45f, 45f);

                var targetRotation = Quaternion.Euler(Vector3.right * cameraRotX);


                //Vector3 angle = new Vector3(rotation * Time.deltaTime * rotDir, 0, 0);

                //Debug.Log(Camera.main.transform.localRotation.x);

                Camera.main.transform.localRotation = targetRotation;
                //Camera.main.transform.Rotate(angle, Space.Self);

            }
        }

        void Jump(InputAction.CallbackContext context)
        {
            if (grounded == false) return;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        void Fire(InputAction.CallbackContext context)
        {
            Debug.Log("I fired");
            Instantiate(bulletPrefab, transform.position, Camera.main.transform.rotation);
        }

        

        
        

    }
}
