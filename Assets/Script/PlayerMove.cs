using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    private PlayerInput playerInput;
    private Rigidbody prb;
    private Animator Panimator;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        prb = GetComponent<Rigidbody>();
        Panimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
        //Rotate();
        //Move();
        //Panimator.SetFloat("Move", playerInput.move);
        Panimator.SetBool("Shoot", true);

        if(h != 0.0f || v != 0.0f)
        {
            Vector3 dir = h * Vector3.forward + v*Vector3.left;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            Panimator.SetBool("bMove", true);
        }
        else
        {
            Panimator.SetBool("bMove", false);
        }



    }

    private void Move()
    {
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        prb.MovePosition(prb.position + moveDistance);
    }
    //private void Rotate()
    //{
    //    float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
    //    prb.rotation = prb.rotation * Quaternion.Euler(0, turn, 0f);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            GameManager.instance.DoorOpen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            GameManager.instance.DoorClose();
        }
    }

    void Update()
    {
        
    }
}
