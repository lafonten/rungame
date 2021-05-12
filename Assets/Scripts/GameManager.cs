using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public bool permission = false;
    public GameObject player;
    public int line = 2;

    void Start()
    {
        GameManagerInstance = this;
    }

    
    void Update()
    {
        CheckUserInput();
    }

    public void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartingGame();
        }

        if (permission == true)
        {
            if (Input.GetKeyDown(KeyCode.A) && line > 1)
            {
                Left();
                line -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.D) && line < 3)
            {
                Right();
                line += 1;
            }else if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.playerInstance.playerState = PlayerState.jump;
                Jumping();
            }
        }
    }

    public void StartingGame()
    {
        Player.playerInstance.StartGame();
        permission = true;
        Debug.Log("game manager start");
    }

    public void Left()
    {
        float speed = Player.playerInstance.player_horizontal_speed;
        Player.playerInstance.MoveHorizontal(-speed);
    }

    public void Right()
    {
        float speed = Player.playerInstance.player_horizontal_speed;
        Player.playerInstance.MoveHorizontal(speed);
    }

    public void Jumping()
    {
        Player.playerInstance.playerState = PlayerState.jump;
        Player.playerInstance.Jump();
    }
}
