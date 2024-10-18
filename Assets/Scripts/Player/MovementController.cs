using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool controlsActive;

    public float speed;
    public float sprintMulti;
    public Transform player;
    public new Transform camera;
    public Transform aimCamera;
    public Transform aimLookAt;

    public GameObject thirdPersonCam;
    public GameObject aimCam;

    public Stamina staminaController;

    public CameraStyle currentStyle;

    public float currSpeed;
    public bool isMoving;

    public void setMovespeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void setSprintMulti(float newMulti)
    {
        sprintMulti = newMulti;
    }

    public enum CameraStyle
    {
        Basic,
        Aim
    }
    private void Awake()
    {
        controlsActive = true;
        isMoving = false;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        aimCam.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z); // lock player y so they dont fly around
        if (controlsActive)
        {
            isMoving = IsMoving();
            Vector3 direction = player.position - new Vector3(camera.position.x, player.position.y, camera.position.z);

            float horizontal = Input.GetAxis("Horizontal") * speed;
            float vertical = Input.GetAxis("Vertical") * speed;


            //Switching CameraStyles
            if (Input.GetMouseButtonDown(1))
            {
                SwitchCameraStyle(CameraStyle.Aim);
            }
            if (Input.GetMouseButtonUp(1))
            {
                SwitchCameraStyle(CameraStyle.Basic);
            }

            if (currentStyle == CameraStyle.Aim)
            {
                Vector3 dirToAimLookAt = aimLookAt.position - new Vector3(aimCamera.position.x, aimLookAt.position.y, aimCamera.position.z);
                player.forward = dirToAimLookAt.normalized;
            }

            else if (currentStyle == CameraStyle.Basic) 
            {
                if ((Mathf.Abs(horizontal) + Mathf.Abs(vertical)) > 0) 
                {
                    player.forward = direction.normalized;
                }
            }


            //Player Movement
            Vector3 inputDir = player.forward * vertical + player.right * horizontal;

            Vector3 forward = player.forward;
            Vector3 right = player.right;
            Vector3 dir = forward * vertical + right * horizontal;
            dir = dir.normalized;

            if (Input.GetKey(KeyCode.LeftShift) && (staminaController.stamina > 0.0f)) {
                currSpeed = speed * sprintMulti;
                player.position = player.position + (currSpeed * Time.deltaTime * dir);
            }else {
                currSpeed = speed;
                player.position = player.position + (currSpeed * Time.deltaTime * dir);
            }
        } else
        {
            isMoving = false;
        }
    }

    public bool IsMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f);
    }

    private void SwitchCameraStyle(CameraStyle newStyle) 
    {
        thirdPersonCam.SetActive(false);
        aimCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Aim) 
        {
            aimCam.SetActive(true);
        }

        currentStyle = newStyle;

    }
}
