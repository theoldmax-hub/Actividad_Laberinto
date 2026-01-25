//Script de movimiento del player
//using UnityEngine;

//public class Movement : MonoBehaviour
//{
//    public float rotationSpeed = 65f;
//    public float moveSpeed = 3f;
//    public float gravity = 9.81f;
    
//    private float verticalVelocity = 0f;
//    private CharacterController controller;

    
//    void Start()
//    {
//        controller = GetComponent<CharacterController>();

//        Cursor.visible = false;

//    }

    
//    void Update()
//    {
//        float rotation = Input.GetAxis("Horizontal");
//        float movement = Input.GetAxis("Vertical");

        

//        transform.Rotate(0f, rotation * rotationSpeed * Time.deltaTime, 0f);

//if (controller.isGrounded) { 
//        verticalVelocity = -0.1f;
//        }
//        else
//        {
//            verticalVelocity -= gravity * Time.deltaTime;
//        }

//        Vector3 move = new Vector3(0f, verticalVelocity * Time.deltaTime, movement * moveSpeed * Time.deltaTime);

//        move = transform.TransformDirection(move);

        
//        controller.Move(move);

//    }
//}
