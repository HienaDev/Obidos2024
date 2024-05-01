using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalMouseSensitivity;
    [SerializeField] private float verticalMouseSensitivity;
    [SerializeField] private float maxHeadUpAngle;
    [SerializeField] private float minHeadDownAngle;
    [SerializeField] private float gravity;

    private bool moving = true;
    private bool hasSpeed;

    private CharacterController characterController;
    private Transform           head;



    private void Awake()
    {
        moving = true;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        head = GetComponentInChildren<Camera>().transform;

        
        hasSpeed = false;

    }

    private void OnEnable()
    {
        HideCursor();
    }

    private void OnDisable()
    {
        //ShowCursor();
    }

    public void HideCursor()
    {
            Cursor.lockState = CursorLockMode.Locked;

       
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (moving)
        {
            UpdatePlayerRotation();
            UpdateHeadRotation();
            Move(); 
        }
        else
            hasSpeed = false;
        
    }

    private void UpdatePlayerRotation()
    {
        float rotation = Input.GetAxis("Mouse X") * horizontalMouseSensitivity;

        transform.Rotate(0f, rotation, 0f);
    }

    private void UpdateHeadRotation()
    {
        Vector3 rotation = head.localEulerAngles;

        rotation.x -= Input.GetAxis("Mouse Y") * verticalMouseSensitivity;

        if (rotation.x < 180)
            rotation.x = Mathf.Min(rotation.x, maxHeadUpAngle);
        else
            rotation.x = Mathf.Max(rotation.x, minHeadDownAngle);

        head.localEulerAngles = rotation;
    }

    private void Move()
    {
        float x = Input.GetAxis("Forward") * 5f  * Time.deltaTime;
        float z = Input.GetAxis("Strafe")  * 5f * Time.deltaTime;

        if (x != 0 || z != 0)
        {
            hasSpeed = true;
        }
        else
            hasSpeed = false;

        Vector3 move = transform.right * z + transform.forward * x + transform.up * gravity * Time.deltaTime;

        characterController.Move(move);
    }

    public void SetSensitivity(float sensitivity)
    {
        horizontalMouseSensitivity = sensitivity;
        verticalMouseSensitivity   = sensitivity;
    }

    public void EnableMovement()
    {
        HideCursor();
        moving = true;
    }

    public void DisableMovement()
    {
        ShowCursor();
        moving = false;
    }

    public float GetSensitivity()
    {
        return horizontalMouseSensitivity;
    }

    public float GetVerticalSensitivity()
    {
        return verticalMouseSensitivity;
    }

    public bool GetHasSpeed() => hasSpeed;

}
