using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private NavMeshHit _navHit;
    private Vector3 _navVector;
    private int _enemyVector;
    
    public GameObject player;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _moveDistance = 10f;

    public bool Fire = false;
    public bool FireReload = false;
    [SerializeField] private float _smooth;
    [SerializeField] private bool _isMelee;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        MoveRN();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("Blend", _navMeshAgent.velocity.magnitude / _movementSpeed);
        
        Vector3 direction = player.transform.position - this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion current = transform.localRotation;
        transform.rotation = Quaternion.Lerp(current, rotation, _smooth * Time.deltaTime);

        _navVector = _navHit.position;
        _enemyVector = (int)transform.position.x;
    }

    Vector3 RandomNavSphere(float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += transform.position;

        NavMesh.SamplePosition(randomDirection, out _navHit, distance, -1);

        return _navHit.position;
    }

    public IEnumerator WaitMove()
    {
        yield return new WaitForSeconds(3f);
        if (((int)_navHit.position.normalized.x == (int)this.transform.position.normalized.x) && 
            ((int)_navHit.position.normalized.z == (int)this.transform.position.normalized.z) && 
            Fire == false && FireReload == false)
        {
            Fire = true;
            FireReload = true;
            GetComponent<EnemyDisAtack>().fireRel = true;
            GetComponent<EnemyDisAtack>().Fire();
        }
        else
            StartCoroutine(WaitMove());
    }
    
    public void MoveRN()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        if (!_isMelee)
        {
            _navMeshAgent.SetDestination(RandomNavSphere(_moveDistance));
            StartCoroutine(WaitMove());
        }
        else
        {
            _navMeshAgent.SetDestination(player.transform.position);
        }
    }
}

