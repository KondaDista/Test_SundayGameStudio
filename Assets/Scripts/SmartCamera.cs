using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
  [SerializeField] private Transform player;
  private Vector3 _target;

  [SerializeField] private float trakingSpeed = 1.5f;
  [SerializeField] private float height = 10f;
  [SerializeField] private float distance = 10f;

  private void Update()
  {
    if (player)
    {
      Vector3 currentPosition = Vector3.Lerp(transform.position, _target, trakingSpeed * Time.deltaTime);
      transform.position = currentPosition;

      var position = player.transform.position;
      _target = new Vector3(position.x, position.y + height, position.z + distance);
    }
  }
}
