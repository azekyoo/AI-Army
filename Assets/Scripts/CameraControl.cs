using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed of movement
    public float rotationSpeed = 100f; // Speed of rotation
    public bool invertMouseY = false; // Option to invert mouse Y-axis
    public KeyCode ascendKey = KeyCode.Space; // Key to ascend
    public KeyCode descendKey = KeyCode.LeftShift; // Key to descend

    private float yaw = 0f; // Rotation around the Y-axis (horizontal rotation)
    private float pitch = 0f; // Rotation around the X-axis (vertical rotation)

    void Start()
    {
        // Initialize yaw and pitch to match the camera's current rotation
        Vector3 initialRotation = transform.eulerAngles;
        pitch = initialRotation.x;
        yaw = initialRotation.y;
    }

    void Update()
    {
        // Handle rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        yaw += mouseX; // Adjust yaw (horizontal rotation)
        pitch -= mouseY * (invertMouseY ? -1 : 1); // Adjust pitch (vertical rotation), invert if needed
        pitch = Mathf.Clamp(pitch, -89f, 89f); // Clamp pitch to avoid flipping

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f); // Apply rotation

        // Handle movement
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow

        // Forward and right relative to the camera's current orientation
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Remove the vertical component from forward and right to keep movement parallel to the ground
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate movement direction
        Vector3 moveDirection = (horizontal * right + vertical * forward).normalized;

        // Handle ascent and descent
        if (Input.GetKey(ascendKey))
        {
            moveDirection.y += 1f; // Ascend
        }
        if (Input.GetKey(descendKey))
        {
            moveDirection.y -= 1f; // Descend
        }

        // Apply movement
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
