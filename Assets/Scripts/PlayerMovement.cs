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

    ControllHorizontal _controlHorizontal;
    ControllVertical _controlVertical;

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
            //MoveBack();
        }
        else
        {
            //MoveForward();
        }
        InvertControls();

    }

    private void OnTriggerEnter(Collider other)
    {
        _camTrigger = other.GetComponent<CameraTrigger>();
        if (_camTrigger != null)
        {
            _controlHorizontal = _camTrigger.controlHorizontal;
            _controlVertical = _camTrigger.controlVertical;
            waitForRelease = true;
            Camera[] allCameras = FindObjectsOfType<Camera>();
            foreach (Camera cam in allCameras)
            {
                cam.gameObject.SetActive(false);
            }

            _camTrigger.cameraToActivate.gameObject.SetActive(true);
        }
    }

    public void Move(float horizontal, float vertical)
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        _playerRotation = Quaternion.LookRotation(direction);


        if (direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, rotationSpeed * Time.deltaTime);

            Vector3 moveDir = transform.rotation * Vector3.forward;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    void InvertControls()
    {
        float horizontal = 0;
        float vertical = 0;

        float horizontalOg = Input.GetAxis("Horizontal");
        float horizontalVert = Input.GetAxis("Vertical");
        float horizontalInvert = -Input.GetAxis("Horizontal");
        float horizontalVertInvert = -Input.GetAxis("Vertical");
        float verticalOg = Input.GetAxis("Vertical");
        float verticalHorz = Input.GetAxis("Horizontal");
        float verticalInvert = -Input.GetAxis("Vertical");
        float verticalHorzInvert = -Input.GetAxis("Horizontal");

        _controlHorizontal = _camTrigger.controlHorizontal;
        _controlVertical = _camTrigger.controlVertical;

        if (ControllHorizontal.horizontalOg == _camTrigger.controlHorizontal)
        {
            horizontal = horizontalOg;
        }
        else if (ControllHorizontal.horizontalVert == _camTrigger.controlHorizontal)
        {
            horizontal = horizontalVert;
        }
        else if (ControllHorizontal.horizontalInvert == _camTrigger.controlHorizontal)
        {
            horizontal = horizontalInvert;
        }
        else if (ControllHorizontal.horizontalVertInvert == _camTrigger.controlHorizontal)
        {
            horizontal = horizontalVertInvert;
        }

        if (ControllVertical.verticalOg == _camTrigger.controlVertical)
        {
            vertical = verticalOg;
        }
        else if (ControllVertical.verticalHorz == _camTrigger.controlVertical)
        {
            vertical = verticalHorz;
        }
        else if (ControllVertical.verticalInvert == _camTrigger.controlVertical)
        {
            vertical = verticalInvert;
        }
        else if (ControllVertical.verticalHorzInvert == _camTrigger.controlVertical)
        {
            vertical = verticalHorzInvert;
        }

        Debug.Log(_controlHorizontal + " " + _controlVertical);
        
        Move(horizontal, vertical);
    }

    //public void MoveForward()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    _playerRotation = Quaternion.LookRotation(direction);


    //    if (direction.magnitude >= 0.1f)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, rotationSpeed * Time.deltaTime);

    //        Vector3 moveDir = transform.rotation * Vector3.forward;
    //        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
    //    }
    //}


    //public void MoveBack()
    //{
    //    float horizontal = -Input.GetAxis("Horizontal");
    //    float vertical = -Input.GetAxis("Vertical");

    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    _playerRotation = Quaternion.LookRotation(direction);


    //    if (direction.magnitude >= 0.1f)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, _playerRotation, rotationSpeed * Time.deltaTime);

    //        Vector3 moveDir = transform.rotation * Vector3.forward;
    //        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
    //    }
    //}

}

public enum ControllHorizontal
{
    horizontalOg,
    horizontalVert,
    horizontalInvert,
    horizontalVertInvert
}
public enum ControllVertical
{
    verticalOg,
    verticalHorz,
    verticalInvert,
    verticalHorzInvert
}
