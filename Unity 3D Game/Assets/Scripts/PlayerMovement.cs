using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; //how fast the turn should be
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity; //stores the rotation

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // FixedUpdate is used for Physics operations
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); //move horizontally
        float vertical = Input.GetAxis("Vertical"); //move vertically

        m_Movement.Set(horizontal, 0f, vertical); //assign values to horizontal and vertical movement from the vert and horiz float
        m_Movement.Normalize(); //keep the direction of the vector the same but the magnitude is changed to 1

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f); //determines whether there is a horizontal input
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f); //determines whether there is a vertical input
        bool isWalking = hasHorizontalInput || hasVerticalInput; //determines whether there is a horizontal input
        m_Animator.SetBool("isWalking", isWalking);

        //The character's forward vector. Time.deltaTime is the time since the previous frame 
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward); //creates a rotation based on the direction in the parameter
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
