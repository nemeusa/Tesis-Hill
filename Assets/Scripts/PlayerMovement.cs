using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    private Quaternion _playerRotation;
    public int moveAngle = 1;
    public bool invertControls;
    public bool waitForRelease;

    [Header("Physics")]
    private Rigidbody rb;
    private Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) invertControls = !invertControls;

        Move();
    }

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        //if (waitForRelease && Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f))
        if (waitForRelease && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            invertControls = !invertControls;
            waitForRelease = false;
        }

        if (invertControls)
        {
            horizontal *= -1;
            vertical *= -1;
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        _playerRotation = Quaternion.LookRotation(direction);


        if (direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, rotationSpeed * Time.deltaTime);

            Vector3 moveDir = transform.rotation * Vector3.forward;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    //public int GetMoveAngle(int angle)
    //{
    //    if ( horizontal <= 0.1 || vertical <= 0.1)
    //    {
    //        int moveAngle = angle;
    //    }

    //        return moveAngle;
    //}
}
