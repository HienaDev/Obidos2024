using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalMouseSensitivity;
    [SerializeField] private float _verticalMouseSensitivity;
    [SerializeField] private float _maxHeadUpAngle;
    [SerializeField] private float _minHeadDownAngle;

    private bool _moving;
    private bool _hasSpeed;

    private CharacterController _characterController;
    private Transform           _head;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _head = GetComponentInChildren<Camera>().transform;

        _moving = true;
        _hasSpeed = false;

    }

    private void OnEnable()
    {
        HideCursor();
    }

    private void OnDisable()
    {
        ShowCursor();
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
        if (_moving)
        {
            UpdatePlayerRotation();
            UpdateHeadRotation();
            Move();
        }
        else
            _hasSpeed = false;
        
    }

    private void UpdatePlayerRotation()
    {
        float rotation = Input.GetAxis("Mouse X") * _horizontalMouseSensitivity;

        transform.Rotate(0f, rotation, 0f);
    }

    private void UpdateHeadRotation()
    {
        Vector3 rotation = _head.localEulerAngles;

        rotation.x -= Input.GetAxis("Mouse Y") * _verticalMouseSensitivity;

        if (rotation.x < 180)
            rotation.x = Mathf.Min(rotation.x, _maxHeadUpAngle);
        else
            rotation.x = Mathf.Max(rotation.x, _minHeadDownAngle);

        _head.localEulerAngles = rotation;
    }

    private void Move()
    {
        float x = Input.GetAxis("Forward") * 5f  * Time.deltaTime;
        float z = Input.GetAxis("Strafe")  * 5f * Time.deltaTime;

        if (x != 0 || z != 0)
        {
            _hasSpeed = true;
        }
        else
            _hasSpeed = false;

        Vector3 move = transform.right * z + transform.forward * x;

        _characterController.Move(move);
    }

    public void SetSensitivity(float sensitivity)
    {
        _horizontalMouseSensitivity = sensitivity;
        _verticalMouseSensitivity   = sensitivity;
    }

    public void EnableMovement() => _moving = true;

    public void DisableMovement() => _moving = false;

    public float GetSensitivity()
    {
        return _horizontalMouseSensitivity;
    }

    public float GetVerticalSensitivity()
    {
        return _verticalMouseSensitivity;
    }

    public bool GetHasSpeed() => _hasSpeed;

}
