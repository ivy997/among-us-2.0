using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidBody;
    private Vector3 position;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        //UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (position != Vector3.zero)
        {
            animator.SetFloat("moveX", position.x);
            animator.SetFloat("moveY", position.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void MoveCharacter()
    {
        position = Vector3.zero;
        position.x = Input.GetAxisRaw("Horizontal");
        position.y = Input.GetAxisRaw("Vertical");

        if (position.magnitude > 1)
        {
            position = Vector3.ClampMagnitude(position, 1);
        }

        rigidBody.MovePosition(transform.position + position * speed * Time.deltaTime);
    }
}
