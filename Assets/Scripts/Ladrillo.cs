using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ladrillo : MonoBehaviour
{

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // al presionar barra espaciadora detonar explosión
        if(Input.GetButtonDown("Jump")){

            _rigidbody.AddExplosionForce(
                10000, 
                new Vector3(433.69f, 206.7f, -1.08f),
                10);
        }
        
    }
}
