using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject deadEffect;
    public float health = 4f;
    public static int EnemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.relativeVelocity.magnitude > health)
        {
            die();
        }
    }
    void die()
    {
        GameObject dead = Instantiate(deadEffect, transform.position, Quaternion.identity) as GameObject;
        EnemiesAlive--;
        Destroy(gameObject);
        Destroy(dead, 0.5f);
        if (EnemiesAlive <= 0)
        {
          int CurrentScence = SceneManager.sceneCountInBuildSettings;
            if (CurrentScence > SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                print("Scene Count: " + SceneManager.sceneCountInBuildSettings);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
           
        }
            
    }
}