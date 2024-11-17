using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Personaje
    private Transform playerTransform;
    private Rigidbody playerRigidbody;

    public float playerSpeed = 5f; // Velocidad del jugador

    //Camara
    public Transform cameraAxis;
    public Transform cameraTrack;
    private Transform theCamera;

    //Rotaciones de los ejes con la camara
    private float rotY = 0f;
    private float rotX = 0f;

    public float camRotSpeed = 100f; // Ajusta según preferencia
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float cameraSpeed = 10f; // Velocidad de seguimiento de la cámara
    public float mouseSensitivity = 0.5f; // Multiplicador para ajustar la sensibilidad del mouse

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = this.transform;
        playerRigidbody = GetComponent<Rigidbody>();
        theCamera = Camera.main.transform;

        // Asegúrate de que el Rigidbody no use gravedad para evitar problemas con el movimiento
        playerRigidbody.useGravity = true; 
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation; // Evita que el Rigidbody gire por fuerzas externas
    }

    // Update is called once per frame
    void Update()
    {
        CameraLogic();
    }

    // FixedUpdate se usa para la física
    void FixedUpdate()
    {
        MoveLogic();
    }

    public void MoveLogic()
    {
        // Obtener la dirección en función de los inputs
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcular la dirección relativa al jugador
        Vector3 moveDirection = (playerTransform.right * moveX + playerTransform.forward * moveZ).normalized;

        // Aplicar la velocidad al Rigidbody
        playerRigidbody.velocity = moveDirection * playerSpeed + new Vector3(0, playerRigidbody.velocity.y, 0);
    }

    public void CameraLogic()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotX += mouseX * camRotSpeed * Time.deltaTime;
        rotY += mouseY * camRotSpeed * Time.deltaTime;

        // Clamp the vertical rotation to limit the camera angle
        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        // Rotate player only on the Y axis
        playerTransform.rotation = Quaternion.Euler(0, rotX, 0);

        // Rotate camera on the X axis for looking up/down
        cameraAxis.localRotation = Quaternion.Euler(-rotY, 0, 0);

        // Smoothly move and rotate the camera to its target position and rotation
        theCamera.position = Vector3.Lerp(theCamera.position, cameraTrack.position, cameraSpeed * Time.deltaTime);
        theCamera.rotation = Quaternion.Lerp(theCamera.rotation, cameraTrack.rotation, cameraSpeed * Time.deltaTime);
    }
}
