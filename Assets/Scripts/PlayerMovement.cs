using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;
    private Quaternion _playerRotation;
    public int moveAngle = 1;
    public bool invertControlsY;
    public bool waitForRelease;
    CameraTrigger _camTrigger;

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
        if (Input.GetKeyDown(KeyCode.R)) invertControlsY = !invertControlsY;

        if (waitForRelease && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            invertControlsY = _camTrigger.invertPlayerControls;
            waitForRelease = false;
        }

        if (invertControlsY)
        {
            MoveBack();
        }
        else
        {
            MoveForward();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _camTrigger = other.GetComponent<CameraTrigger>();
        if (_camTrigger != null)
        {
            waitForRelease = true;
            Camera[] allCameras = FindObjectsOfType<Camera>();
            foreach (Camera cam in allCameras)
            {
                cam.gameObject.SetActive(false);
            }

            _camTrigger.cameraToActivate.gameObject.SetActive(true);
        }
    }


    public void MoveForward()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        ////if (waitForRelease && Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f))
        //if (waitForRelease && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        //{
        //    invertControls = !invertControls;
        //    waitForRelease = false;
        //}

        //if (invertControls)
        //{
        //    horizontal *= -1;
        //    vertical *= -1;
        //}
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        _playerRotation = Quaternion.LookRotation(direction);


        if (direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, rotationSpeed * Time.deltaTime);

            Vector3 moveDir = transform.rotation * Vector3.forward;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }


    public void MoveBack()
    {
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");


        ////if (waitForRelease && Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f))
        //if (waitForRelease && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        //{
        //    invertControls = !invertControls;
        //    waitForRelease = false;
        //}

        //if (invertControls)
        //{
        //    horizontal *= -1;
        //    vertical *= -1;
        //}
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        _playerRotation = Quaternion.LookRotation(direction);


        if (direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, rotationSpeed * Time.deltaTime);

            Vector3 moveDir = transform.rotation * Vector3.forward;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

}
