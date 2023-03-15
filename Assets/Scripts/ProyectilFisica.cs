using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProyectilFisica : MonoBehaviour
{


    [SerializeField]
    private float _fuerza = 10;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // vectores de referencia que siempre tenemos en el transform
        // transform.up
        // transform.right
        // transform.forward
        // estos vectores SIEMPRE están en espacio del mundo
        // estos vectores siempre están normalizados

        _rigidbody.AddForce(
            transform.up * _fuerza,
            ForceMode.Impulse
        );
    }

    void OnCollisionEnter(Collision c)
    {
        print("LAYER: " + c.gameObject.layer);
        print("TAGS: " + c.transform.tag);
    }
}
