using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsEffect : MonoBehaviour
{
    [SerializeField] private GameObject _footstepsEffect;
    [SerializeField] private Transform[] _spawnPositionsFootstepsEffect;

    public void FootstepsEffectSpawn(int SpawnIndex)
    {
        /*GameObject FootEffect = Instantiate(_footstepsEffect, _spawnPositionsFootstepsEffect[SpawnIndex].position, _spawnPositionsFootstepsEffect[SpawnIndex].rotation);
        Destroy(FootEffect, 5f);*/
        
        _spawnPositionsFootstepsEffect[SpawnIndex].GetComponent<ParticleSystem>().Play();
    }

    public void FootstepsEffectStop()
    {
        _spawnPositionsFootstepsEffect[0].GetComponent<ParticleSystem>().Stop();
        _spawnPositionsFootstepsEffect[1].GetComponent<ParticleSystem>().Stop();
    }
}
