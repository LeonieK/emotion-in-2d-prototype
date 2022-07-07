using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObstacleCollider : MonoBehaviour
{
    [SerializeField] private GameObject respawnPosition;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject[] hearts;
    public int life;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            if(life > 1){
                TakeDamage(1, other.gameObject.transform.GetChild(0).transform.position.x,other.gameObject.transform.GetChild(0).transform.position.y);
            } else{
                Destroy(hearts[0].gameObject);
                Die();
            }
            
        }
    }

    private void Die()
    {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(int damage, float x, float y)
    {
        life -= damage;
        Destroy(hearts[life].gameObject);
        transform.position = new Vector2(x,y);
    }

}
