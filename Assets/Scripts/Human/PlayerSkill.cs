using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private Rig[] _rigsArm;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private GameObject FireBallSpawn;
    [SerializeField] private float _speedAttack;
    private float FireBallCD = 0f;
    private float FireBallAttack = 0f;
    private bool isFireBallCD = false;
    private bool isFireBallAttack = false;

    private void Update()
    {
        if (isFireBallCD)
        {
            FireBallCD += Time.deltaTime * _speedAttack;
            _rigsArm[0].weight = 1 - FireBallCD;
            _rigsArm[1].weight = 1 - FireBallCD;
            if (FireBallCD >= 1)
            {
                FireBallCD = 0;
                isFireBallCD = false;
            }
        }
        else if(isFireBallAttack)
        {
            FireBallAttack += Time.deltaTime * _speedAttack;
            _rigsArm[0].weight = FireBallAttack;
            _rigsArm[1].weight = FireBallAttack;
            if (FireBallAttack >= 1)
            {
                FireBallAttack = 0;
                AttackFireBall();
            }
        }
    }

    public void ButtonAttack()
    {
        isFireBallAttack = true;
    }

    void AttackFireBall()
    {
        if (!isFireBallCD)
        {
            isFireBallAttack = false;
            isFireBallCD = true;
            GameObject Bullet = Instantiate(FireBall, FireBallSpawn.transform.position, FireBallSpawn.transform.rotation);
            Destroy(Bullet, 5f);
        }
    }
}


