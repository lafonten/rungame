using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    run,
    jump,
    death,
    winner
}

public class Player : MonoBehaviour
{

    [Space(20,order = 2)]
    [Header("PLAYER FEATURES",order = 2)]
    public float player_speed;
    public float player_horizontal_speed;
    public Vector3 direction;
    public static Player playerInstance;
    public PlayerState playerState = PlayerState.idle;
    public bool CanRun = false;
    public float jump_power;
    public Animator MyAnimator;
    private Rigidbody rb;
    public float move_horizontal;

    void Start()
    {
        playerInstance = this;
        MyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (CanRun == true)
        {
            move();
        }

        UpdateAnimations();
    }

    public void StartGame()
    {
        CanRun = true;
        playerState = PlayerState.run;
        Debug.Log("start players");
    }

    public void GameOver()
    {
        CanRun = false;
        playerState = PlayerState.death;
    }

    public void move()
    {
        direction = transform.forward;
        transform.position += direction * Time.deltaTime * player_speed;
    }

    public void MoveHorizontal(float horizontal_speed)
    {
        rb.velocity = new Vector3(horizontal_speed /* Time.deltaTime*/, 0, 0);
    }

    public void Jump()
    {
        rb.velocity = new Vector3(0, jump_power, 0);
    }

    public void UpdateAnimations()
    {
        if (playerState == PlayerState.idle)
        {
            MyAnimator.SetTrigger("idle");
        }

        if (playerState == PlayerState.run)
        {
            MyAnimator.SetTrigger("run");
        }

        if (playerState == PlayerState.death)
        {
            MyAnimator.SetTrigger("death");
        }

        if (playerState == PlayerState.jump)
        {
            MyAnimator.SetTrigger("jump");
            playerState = PlayerState.run;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("winner"))
        {
            playerState = PlayerState.winner;
            this.transform.rotation = new Quaternion(0,180,0,0);
            MyAnimator.SetTrigger("victory");
            MyAnimator.SetTrigger("idle");
            Player.playerInstance.enabled = false;
            Debug.Log("oldu");
        }
    }
}
