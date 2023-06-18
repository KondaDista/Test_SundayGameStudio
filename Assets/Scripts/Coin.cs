using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Material[] firstMaterial;
    [SerializeField] private Material[] secondMaterial;

    public Renderer coinRenderer;
    [SerializeField] private Dropdown dropdownCoin;
    private int modeRedactMaterial = 0;
    private Quaternion originRotation;
    private float angle;
    bool beingHandled;

    [SerializeField] private float speedRotationX;
    [SerializeField] private float speedRotationY;
    [SerializeField] private float speedRotationZ;

    void Start()
    {
        coinRenderer = GetComponent<Renderer>();
        originRotation = transform.rotation;
        Material[] mats = coinRenderer.sharedMaterials;
        mats[0].color = new Color(0.8f, 0.8f,0, 1);
        mats[1].color = new Color(0.6f, 0.6f,0, 1);
        coinRenderer.sharedMaterials = mats;

        ResetMaterial();
    }

    private void FixedUpdate()
    {
        angle++;
        Quaternion rotationX = Quaternion.AngleAxis(angle * speedRotationX, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(angle * speedRotationY, Vector3.up);
        Quaternion rotationZ = Quaternion.AngleAxis(angle * speedRotationZ, Vector3.forward);

        transform.rotation = originRotation * rotationZ * rotationX * rotationY;

        if(!beingHandled )
        {
            StartCoroutine( HandleIt() );
        }
    }
 
    private IEnumerator HandleIt()
    {
        beingHandled = true;
        yield return new WaitForSeconds(1);
        if (coinRenderer.sharedMaterials[0].color.r >= 0.835f)
        {
            Material[] mats = coinRenderer.sharedMaterials;
            mats[0].color = new Color(mats[0].color.r - 0.035f, mats[0].color.g - 0.035f, mats[0].color.b - 0.035f, 1);
            mats[1].color = new Color(mats[1].color.r - 0.035f, mats[1].color.g - 0.035f, mats[1].color.b - 0.035f, 1);

            coinRenderer.sharedMaterials = mats;
        }
        beingHandled = false;
    }

    public void DropdownSample(int indexMode)
    {
        switch (indexMode)
        {
            case 0: modeRedactMaterial = 0; break;
            case 1: modeRedactMaterial = 1; break;
        }
    }

    void SetMaterial()
    {
        int index = Random.Range(0, firstMaterial.Length);
        Material[] mats = coinRenderer.materials;
        
        if (mats[0].color == firstMaterial[index].color)
        {
            switch (index)
            {
                case 0:
                    index++;
                    break;
                case 1:
                    int i = Random.Range(0, 2);
                    switch (i)
                    {
                        case 0 : index++; break;
                        case 1 : index--; break;
                    }
                    break;
                case 2:
                    index--;
                    break;
            }
        }
        mats[0] = firstMaterial[index];
        mats[1] = secondMaterial[index];
        
        coinRenderer.materials = mats;
        Debug.Log("Set Material");
    }

    void WarmMaterial()
    {
        if (coinRenderer.sharedMaterials[0].color.b < 0.95f)
        {
            Material[] mats = coinRenderer.sharedMaterials;
            mats[0].color = new Color(mats[0].color.r + 0.05f, mats[0].color.g + 0.05f, mats[0].color.b + 0.05f, 1);
            mats[1].color = new Color(mats[1].color.r + 0.05f, mats[1].color.g + 0.05f, mats[1].color.b + 0.05f, 1);

            coinRenderer.sharedMaterials = mats;
        }
    }

    public void ResetMaterial()
    {
        firstMaterial[0].color = new Color(0.8f, 0.8f,0, 1);
        secondMaterial[0].color = new Color(0.6f, 0.6f,0, 1);
        
        firstMaterial[1].color = new Color(0.8f, 0f,0, 1);
        secondMaterial[1].color = new Color(0.6f, 0f,0, 1);
        
        firstMaterial[2].color = new Color(0.8f, 0.4f,0, 1);
        secondMaterial[2].color = new Color(0.6f, 0.3f,0, 1);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");

        switch (modeRedactMaterial)
        {
            case 0: SetMaterial(); break;
            case 1: WarmMaterial(); break;
        }
    }
}
