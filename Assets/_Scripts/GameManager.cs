﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] public GameObject[] health;
    [SerializeField] private int score;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private ScoreUI scoreCounter;
    [SerializeField] private GameObject gameOverScreen;

    private int currentBrickCount;
    private int totalBrickCount;

    void Start(){
        gameOverScreen.SetActive(false);
    }

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        CameraShake.Shake(0.3f, 0.5f);
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        score++;
        scoreCounter.UpdateScore(score);
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();

    }

    public void KillBall()
    {
        maxLives--;
        if(maxLives < 3){
            Destroy(health[2].gameObject);
        }
        if(maxLives < 2){
            Destroy(health[1].gameObject);
        }
        if(maxLives < 1){
            Destroy(health[0].gameObject);
        }

        if(maxLives < 0){
            Debug.Log("GameOver met!");
            GameOver();
            return;
        }
        
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }

    public void GameOver(){
        gameOverScreen.SetActive(true);
        Debug.Log("GameOver screen activated!");
        Time.timeScale = 0;
        StartCoroutine(ReturnMenu());
    }

    private IEnumerator ReturnMenu(){
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
        Debug.Log("Main Menu incoming...");
        SceneHandler.Instance.LoadMenuScene();
        gameOverScreen.SetActive(false);
    }

}
