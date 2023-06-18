using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 20;
    public float speed = 15;
    public GameObject effectDeath;
    public bool isDark = false;

    public void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("HardFloor") || col.CompareTag("Player"))
        {
            if (isDark && col.CompareTag("Player"))
            {
                col.GetComponent<PlayerController>().TakeDamage(damage);
            }

            speed = 0;
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            effectDeath.SetActive(true);
            
            Destroy(this.gameObject, 1);
        }
    }
}