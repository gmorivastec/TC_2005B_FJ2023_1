using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Proyectil : MonoBehaviour
{


    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private float _tiempoDeAutodestruccion = 3;

    private GUIManager _gui;
    
    void Start() 
    {

        // NOTA IMPORTANTE
        // si voy a crear objetos dinámicamente
        // es indispensable que tenga al menos 1 estrategia de destrucción
        
        // destroy - destruye game objects completos
        // o componentes
        
        Destroy(gameObject, _tiempoDeAutodestruccion);

        // NOTA- ESTO VA A CAMBIAR
        /*
        GameObject guiGO = GameObject.Find("GUIManager");
        Assert.IsNotNull(guiGO, "no hay GUIManager");

        _gui = guiGO.GetComponent<GUIManager>();
        Assert.IsNotNull(_gui, "GUIManager no tiene componente");
*/
        _gui = GUIManager.Instance;
        //_gui.Dummy1 = 1;
        Assert.IsNotNull(_gui, "GUIManager nulo, verificar que tengas uno en escena");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            0,
            _speed * Time.deltaTime,
            0
        );
    }
 
    // COLISIONES
    // para checar colisiones con física necesitamos:
    // 1. todos los objetos involucrados necesitan collider
    // 2. necesitamos que al menos 1 tenga rigidbody
    // 3. el rigidbody debe estar en un objeto que se mueva

    // TODOS LOS INVOLUCRADOS PUEDEN TENER 
    // SUS RESPECTIVOS MENSAJES DE REACCIÓN

    void OnCollisionEnter(Collision c) 
    {
        // objeto collision que recibimos
        // contiene info de la colisión
        
        // cómo saber qué hacer 
        // 1. filtrar por tag
        // 2. filtrar por layer
        print("ENTER " + c.transform.name);
    }

    void OnCollisionStay(Collision c) 
    {
        print("STAY");
    }

    void OnCollisionExit(Collision c) 
    {
        print("EXIT");
    }

    void OnTriggerEnter(Collider c)
    {
        print("TRIGGER ENTER");
    }

    void OnTriggerStay(Collider c)
    {
        print("TRIGGER STAY");
    }

    void OnTriggerExit(Collider c)
    {
        print("TRIGGER EXIT");
        _gui._texto.text = "SALI " + transform.name;
    }
}
