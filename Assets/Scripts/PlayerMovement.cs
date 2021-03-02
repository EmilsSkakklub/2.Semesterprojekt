using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    public LayerMask groundMask;

    Vector3 velocity;

    private float speed = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        GroundCheck();
    }
    void Update()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Invoke("Jump", 0.305f);
            isGrounded = false;
        }

        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.C))
        {
            controller.Move(move * speed * -0.5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * speed * 1.5f * Time.deltaTime);
        }
    }

    void Jump() {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    void GroundCheck() {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.07f + 0.1f)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }
}
