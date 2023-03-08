using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class GUIManager : MonoBehaviour
{

    // singleton 
    // https://en.wikipedia.org/wiki/Singleton_pattern

    // design pattern que limita la creación de objetos de una clase a solo 1
    // lo hace limitando el acceso al constructor

    // por las restricciones de unity en lugar de constructor privado
    // vamos a borrar nuevas instancias

    private static GUIManager _instance;


    // PROPERTIES
    // mecanismo para dividir quién puede leer / escribir una variable

    // podemos utilizarlos con variables explícitamente declaradas 

    private float _dummy1;

    // escribiendo propiedad
    public float Dummy1 
    {
        get {
            return _dummy1;
        }

        private set {
            _dummy1 = value;
        }
    }

    // NO es necesario declarar ambos get y set 
    public float Dummy1b
    {
        get
        {
            return _dummy1;
        }
    }

    // propiedad con variable anónima
    public float Dummy2 
    {
        get;
        private set;
    }

    public static GUIManager Instance 
    {
        get
        {
            return _instance;
        } 
    }

    [SerializeField]
    public TMP_Text _texto;

    void Awake() 
    {

        // PRUEBAS CON PROPIEDADES
        Dummy1 = 4.2f;
        print(Dummy1);

        // checar si alguien ya pobló la referencia de instancia
        if(_instance != null)
        {
            // ya existía el objeto entonces me borro
            Destroy(gameObject);
            return;
        }


        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_texto, "TEXTO NO PUEDE SER NULO");
        _texto.text = "HOLA DESDE CODIGO";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
