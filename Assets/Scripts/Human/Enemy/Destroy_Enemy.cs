using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Enemy : MonoBehaviour
{
    public void Start()
    {
        Destroy(gameObject, 2.5f);
    }
}
