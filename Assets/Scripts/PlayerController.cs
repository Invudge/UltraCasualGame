using UnityEngine;
using UnityEngine.InputSystem; // <-- Добавили новую библиотеку

public class PlayerController : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float horizontalSpeed = 15f;
    [SerializeField] private float maxHorizontalDistance = 4f;

    private float targetX;

    private void Update()
    {
        MoveForward();
        HandleHorizontalInput();
        ApplyHorizontalMovement();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);
    }

    private void HandleHorizontalInput()
    {
        // мышь
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            float delta = Mouse.current.delta.ReadValue().x;
            targetX += delta * horizontalSpeed * Time.deltaTime * 0.05f;
            targetX = Mathf.Clamp(targetX, -maxHorizontalDistance, maxHorizontalDistance);
        }

        // тачпад
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            float delta = Touchscreen.current.primaryTouch.delta.ReadValue().x;
            targetX += delta * horizontalSpeed * Time.deltaTime * 0.05f;
            targetX = Mathf.Clamp(targetX, -maxHorizontalDistance, maxHorizontalDistance);
        }
    }

    private void ApplyHorizontalMovement()
    {
        float currentX = transform.position.x;
        float newX = Mathf.Lerp(currentX, targetX, Time.deltaTime * 10f);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}