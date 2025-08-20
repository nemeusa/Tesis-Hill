using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera cameraToActivate;
    PlayerMovement _player;
    [SerializeField] float _followCameraSpeed;

    private void OnTriggerEnter(Collider other)
    {
        _player = other.GetComponent<PlayerMovement>();
        if (_player != null)
        {
            Camera[] allCameras = FindObjectsOfType<Camera>();
            foreach (Camera cam in allCameras)
            {
                cam.gameObject.SetActive(false);
            }

            cameraToActivate.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            var dir = _player.transform.position - cameraToActivate.transform.position;

            Quaternion lookPlayer = Quaternion.LookRotation(dir);

            //cameraToActivate.transform.forward = dir * Time.deltaTime * _followCameraSpeed;

            cameraToActivate.transform.rotation = Quaternion.Slerp(cameraToActivate.transform.rotation, lookPlayer, Time.deltaTime * _followCameraSpeed);
        }
    }
}
