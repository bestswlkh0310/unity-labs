using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    public bool isGameOver;

    private void Start()
    {
        var playerGameObject = GameObject.FindWithTag("Player");
        _player = playerGameObject.GetComponent<Player>();
    }

    private void Update()
    {
        HandleGameOver();
    }
    
    private void HandleGameOver()
    {
        if (_player.Hp <= 0 && !isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0;
        }
    } 
}
