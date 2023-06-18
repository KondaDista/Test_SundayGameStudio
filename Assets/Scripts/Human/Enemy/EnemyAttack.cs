using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    private GameObject player;
    private PlayerController playerController;
    private EnemyHealth enemyHealth;
    private EnemyController enemyController;
    private bool playerInRange;
    private float timer;
    public float waitFireFloat = 1;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            enemyHealth.anim.SetBool("Attack", false);
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange )
        {
            StartCoroutine(Attack());
        }
        else
        {
            enemyController.MoveRN();
        }
    }

    IEnumerator Attack()
    {
        timer = 0f;
        enemyHealth.anim.SetBool("Attack", true);
        yield return new WaitForSeconds(waitFireFloat);
        enemyHealth.anim.SetBool("Attack", false);
        if (playerController.currentHealth > 0)
        {
            playerController.TakeDamage(attackDamage);
        }
    }
}

