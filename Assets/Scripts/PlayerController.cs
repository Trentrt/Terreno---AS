using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        // Obtener la referencia al CharacterController adjunto al cubo
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Obtener la entrada del jugador (W/S para avance, A/D para rotación)
        float moveInput = Input.GetAxis("Vertical");   // W/S o Flechas Arriba/Abajo
        float turnInput = Input.GetAxis("Horizontal"); // A/D o Flechas Izquierda/Derecha

        // 2. Rotar el objeto sobre el eje Y
        transform.Rotate(0, turnInput * rotationSpeed * Time.deltaTime, 0);

        // 3. Aplicar gravedad para mantenerse pegado al suelo
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Un pequeño valor para asegurar contacto con el suelo
        }
        verticalVelocity += gravity * Time.deltaTime;

        // 4. Calcular la dirección de movimiento relativa al frente del cubo
        Vector3 moveDirection = transform.forward * moveInput * moveSpeed;
        moveDirection.y = verticalVelocity;

        // 5. Mover el cubo a través del CharacterController
        controller.Move(moveDirection * Time.deltaTime);
    }
}