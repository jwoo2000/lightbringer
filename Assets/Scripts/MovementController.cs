using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float speed = 1f;
    public Transform player;
    public Transform camera;
    public Transform aimCamera;
    public Transform aimLookAt;

    public GameObject thirdPersonCam;
    public GameObject aimCam;
    public GameObject flashLight;

    public Transform stamina;

    public CameraStyle currentStyle;



    public enum CameraStyle
    {
        Basic,
        Aim
    }
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        aimCam.SetActive(false);
        flashLight.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {



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

        if (Input.GetKey(KeyCode.LeftShift) && stamina.position.y > 0) {
            player.position = player.position + (dir * speed * Time.deltaTime * 3);
        }else {
            player.position = player.position + (dir * speed * Time.deltaTime);
        }

    }

    private void SwitchCameraStyle(CameraStyle newStyle) 
    {
        thirdPersonCam.SetActive(false);
        aimCam.SetActive(false);
        flashLight.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Aim) 
        {
            aimCam.SetActive(true);
            flashLight.SetActive(true);
        }

        currentStyle = newStyle;

    }
}
