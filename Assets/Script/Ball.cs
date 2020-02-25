using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Rigidbody2D hook;
    public float releaseTime = 0.15f;
    public float maxDragDistance = 2f;
    private bool IsPressed = false;
    [SerializeField] private GameObject nextBall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hook = GameObject.FindGameObjectWithTag("Hook").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance) {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            }
            else { 
                rb.position = mousePos;
            }
            //{
            //   
            //    Debug.Log("else Part " + rb.position);
            //}  
        }
       
    }

    private void OnMouseDown()
    {
        IsPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        IsPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
    }
    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
       // is most important springJoint2D is false
        GetComponent<SpringJoint2D>().enabled = false;
        // this script is enebled false
        this.enabled = false;
        // after 2 seconds next ball object is active;
        yield return new WaitForSeconds(2f);
        // check the ball is null or not? if not then nextBall object is setactive
        if (nextBall != null)
        {
            //  nextBall object is setactive so all component is active like scripts
            nextBall.SetActive(true);
        }
        else
        {
            //  when Enemy script send EnemiesAlive variable is 0 then load next scence;
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
