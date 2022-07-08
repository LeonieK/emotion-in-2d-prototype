using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObstacleCollider : MonoBehaviour
{
    [SerializeField] private float invincibilityDurationSeconds = 3f;
    [SerializeField] private float invincibilityDeltaTime = 0.15f;
    private Animator anim;
    private bool isInvincible = false;
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
            if(life > 1 && !isInvincible){
                TakeDamage(1);
                transform.position = new Vector2(other.gameObject.transform.GetChild(0).transform.position.x,
                    other.gameObject.transform.GetChild(0).transform.position.y);
                StartCoroutine(GetInvulnerable());
            } else if (life <= 1 && !isInvincible){
                hearts[0].gameObject.GetComponent<Animator>().SetTrigger("destroy");
                Destroy(hearts[0].gameObject, 0.7f);
                Die();
            }
            
        } else if (other.gameObject.CompareTag("Enemy")){
             if(life > 1 && !isInvincible){
                TakeDamage(1);
                //transform.position = new Vector2(other.transform.position.x, other.transform.position.y);
                StartCoroutine(GetInvulnerable());
            } else if (life <= 1 && !isInvincible){
                hearts[0].gameObject.GetComponent<Animator>().SetTrigger("destroy");
                Destroy(hearts[0].gameObject, 0.7f);
                Die();
            }
        }
    }

    IEnumerator GetInvulnerable(){
        isInvincible = true;
        anim.SetTrigger("invincibility");
        for(float i = 0; i < invincibilityDurationSeconds; i+=invincibilityDeltaTime)
        {
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        
        isInvincible = false;
        anim.SetTrigger("normal");
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

    public void TakeDamage(int damage)
    {
        life -= damage;
        hearts[life].gameObject.GetComponent<Animator>().SetTrigger("destroy");
        Destroy(hearts[life].gameObject, 0.7f);
        
    }

}
