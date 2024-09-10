using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator characterAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterAnimator.SetTrigger("JumpTrigger");
        }
        if (Input.GetKeyDown("w"))
        {
            characterAnimator.SetBool("Forward", true);
        }
        if (Input.GetKeyUp("w"))
        {
            characterAnimator.SetBool("Forward", false);
        }

        if (Input.GetKeyDown("s"))
        {
            characterAnimator.SetBool("Backward", true);
        }
        if (Input.GetKeyUp("s"))
        {
            characterAnimator.SetBool("Backward", false);
        }

        if (Input.GetKeyDown("a"))
        {
            characterAnimator.SetBool("Left", true);
        }
        if (Input.GetKeyUp("a"))
        {
            characterAnimator.SetBool("Left", false);
        }

        if (Input.GetKeyDown("d"))
        {
            characterAnimator.SetBool("Right", true);
        }
        if (Input.GetKeyUp("d"))
        {
            characterAnimator.SetBool("Right", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            characterAnimator.SetBool("Shift", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            characterAnimator.SetBool("Shift", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            characterAnimator.SetBool("RMB", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            characterAnimator.SetBool("RMB", false);
        }
        
    }
}
