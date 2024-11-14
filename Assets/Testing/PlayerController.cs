using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float sprintMultiplier = 1.2f;
    CharacterController controller;
    float verticalVelocity = 0;
    float currentSpeed = 0;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        ApplyGravity();
        ApplyMovement();
    }

    void ApplyGravity()
    {
        if(controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    void ApplyMovement()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed * sprintMultiplier;
        }
        else
        {
            currentSpeed = speed;
        }

        controller.Move(CalculateMovement() * currentSpeed * Time.deltaTime);
    }


    Vector3 CalculateMovement()
    {
        Transform mainCamera = Camera.main.transform;

        Vector3 right = (Input.GetAxisRaw("Horizontal") * mainCamera.right).normalized;
        right.y = 0;

        Vector3 forward = (Input.GetAxisRaw("Vertical") * mainCamera.forward).normalized;
        forward.y = 0;

        return right + forward;
    }
}
