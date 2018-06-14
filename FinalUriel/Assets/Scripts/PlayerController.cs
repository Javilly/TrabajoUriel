using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float jumpSpeed = 8.0f;
    [SerializeField]
    private float gravity = 20.0f;
    [SerializeField]
    private int maxJumpCap = 1;

    private int currentJumpCount = 0;

    private Vector3 moveDirection = new Vector3(0, 0, 0);
    private CharacterController controller;
    private float inputAxisX;
    private float inputAxisY;
    private float inputAxisZ;

    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] Animator animator;
    [SerializeField] GameObject enemy;

    [SerializeField] private GameObject victoryPanel;



    private

    void Awake()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        if (!controller)
        {
            Debug.LogError("Failed on get the Player controller");
        }
        animator = GetComponent<Animator>();
        enemy = GetComponent<GameObject>();
        videoPlayer = GetComponent<VideoPlayer>();
        victoryPanel.SetActive(false);
    }


    void FixedUpdate()
    {
        GetImput(ref inputAxisX, ref inputAxisY, ref inputAxisZ);
        Move();
        ResetJumpCount();


        /*
        if (Input.anyKey == false && controller.isGrounded)
        {
            animator.SetBool("Fall", false);
            animator.SetBool("Run fast", false);
            animator.SetBool("Run", true);
        }
        else if(!controller.isGrounded)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Run fast", false);
            animator.SetBool("Fall", true);
        }
        else if (Input.GetButtonDown("Vertical") && controller.isGrounded)
        {
            animator.SetBool("Fall", false);
            animator.SetBool("Run", false);
            animator.SetBool("Run fast", true);
        }
        */

        if (controller.isGrounded)
        {
            animator.SetBool("Fall", false);
            if(Input.anyKey == false)
            {
                animator.SetBool("Run fast", false);
                animator.SetBool("Run", true);
            }
            else if(Input.GetAxis("Vertical") > 0)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Run fast", true);
            }
        }
        else if (!controller.isGrounded)
        {
            animator.SetBool("Run fast", false);
            animator.SetBool("Run", false);
            animator.SetBool("Fall", true);
        }
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("Juego", LoadSceneMode.Single);
        }

        if(transform.position.x == enemy.transform.position.x - 20)
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
            videoPlayer.Play();
        }

    }


    private void ResetJumpCount()
    {
        if (controller.isGrounded)
        {
            currentJumpCount = 0;
        }
    }

    private void Move()
    {
        moveDirection = new Vector3(inputAxisX, 0, inputAxisZ);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        //transform.position = new Vector3(Mathf.Clamp(transform.position.y, -5f, 5f), transform.position.y, transform.position.z);

        if (!controller.isGrounded)
        {
            inputAxisY -= gravity * Time.deltaTime;
        }
            
        moveDirection.y = inputAxisY;

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void GetImput(ref float inputX, ref float inputY, ref float inputZ)
    {

        inputX = Input.GetAxis("Horizontal");

        inputZ = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            Jump(ref inputY);
        }
    }

    private void Jump(ref float inputY)
    {
        if (currentJumpCount < maxJumpCap)
        {
            inputY = jumpSpeed;
            currentJumpCount++;
        }
    }


    /*
    void OnCollisionEnter(Collider target)
    {
        if (target.gameObject.tag.Equals("Wall") == true)
        {
            animator.SetBool("Run fast", false);
            animator.SetBool("Run", false);
            animator.SetBool("Fall", false);
            animator.SetBool("Wall run", true);
        }
    }
    */
}