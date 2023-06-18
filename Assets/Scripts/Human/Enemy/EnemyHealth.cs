using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 10;
    
    public Animator anim;
    public GameObject Ragdoll;
    public GameObject Cam;

    bool isDead;                                
    bool isSinking;

    private GameObject destroyer;
    public float Force;
    private void Start()
    {
        currentHealth = startingHealth;
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Cam.GetComponent<SpawnEnemy>().EnemyScore += scoreValue;
            Destroy(gameObject, 2f);

            gameObject.SetActive(false);

            var ragdoll = Instantiate(Ragdoll, transform.position, transform.rotation);

            var vectorFromDestroyer = transform.position - destroyer.transform.position;
            vectorFromDestroyer.Normalize();
            vectorFromDestroyer.y += 1;

            ragdoll.GetComponent<Ragdoll>().ApplyForce(vectorFromDestroyer * Force);
            scoreValue = 0;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("FireBall"))
        {
            currentHealth -= 50;
            StartCoroutine(WaitDamage());
        }
       
        destroyer = col.gameObject;
    }

    IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Damage", false);
    }
}
