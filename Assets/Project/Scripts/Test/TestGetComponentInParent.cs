using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetComponentInParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var component = GetComponentInParent<TestList>();

        if(component != null)
        {
            Debug.Log($"{ nameof(TestList)} is not null on {component.name}");
        }
        else
        {
            Debug.Log($"{ nameof(TestList)} is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
