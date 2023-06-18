using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int startHealth;
    public int currentHealth;
    [SerializeField] private Slider sliderHealth;

    [SerializeField] private GameObject _uiPlayer;
    [SerializeField] private GameObject _uiDeath;
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float jumpForce;
    
    [SerializeField] private Transform _groundCheker;
    [SerializeField] private LayerMask _nonPlayerMask;

    private void Start()
    {
        currentHealth = startHealth;
        sliderHealth.value = currentHealth;
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            sliderHealth.value = 0;
            _animator.SetBool("Death",true);
            _uiPlayer.SetActive(false);
            _uiDeath.SetActive(true);
        }
        else
        {
            CharacterMove();
        }
    }

    void CharacterMove()
    {
        Vector3 moveVector = new Vector3(_joystick.Horizontal, 0.0f, _joystick.Vertical);
        _rigidbody.velocity = new Vector3(moveVector.x * _moveSpeed,_rigidbody.velocity.y,moveVector.z * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
        }

        if (ChekerGround())
        {
            _animator.SetFloat("BlendMove", _rigidbody.velocity.magnitude / _moveSpeed);
            _animator.SetBool("Falling",false);
        }
        else
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Falling",true);
        }
    }

    public void JumpLogic(Button button)
    {
        if (ChekerGround())
        {
            button.interactable = false;
            _animator.SetBool("Jump", true);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            button.interactable = true;
        }
        else
        {
            Debug.Log("Did not find ground");
        }
    }

    private bool ChekerGround()
    {
        if (Physics.CheckSphere(_groundCheker.position, 0.3f, _nonPlayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        sliderHealth.value = currentHealth;
    }
}
