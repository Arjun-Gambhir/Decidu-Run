using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float gravity = 100f;

    private CharacterController controller = null;
    private Transform mainCameraTransform = null;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        Vector3 movement = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical"),
        }.normalized;

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 calculatedMovement = (forward * movement.z + right * movement.x).normalized;
        calculatedMovement *= movementSpeed;
        calculatedMovement.y -= gravity * Time.deltaTime;

        controller.Move(calculatedMovement * Time.deltaTime);
    }
}
