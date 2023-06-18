using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisAtack : MonoBehaviour
{
    public Transform spawnPositionBullet;
    public GameObject bullet;

    public float waitFireFloat;
    public bool fireRel = false;
    public Animator ch;

    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    public void Fire()
    {
        if (_enemyController.Fire && fireRel)
        {
            fireRel = false;
            _enemyController.Fire = false;
            
            StartCoroutine(WaitFire());
        }
    }

    public IEnumerator WaitFire()
    {
        ch.SetBool("Attack", true);
        yield return new WaitForSeconds(waitFireFloat);
        ch.SetBool("Attack", false);

        Instantiate(bullet, spawnPositionBullet.position, spawnPositionBullet.rotation);

        yield return new WaitForSeconds(waitFireFloat);
        _enemyController.MoveRN();
        _enemyController.FireReload = false;
    }
}
