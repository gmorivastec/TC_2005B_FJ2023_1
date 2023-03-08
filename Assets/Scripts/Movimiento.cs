// estamos usando .NET
// aquí "importamos" namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// OJO 
// con esta directiva obligamos la presencia de un componente en el gameobject
// (todos tienen transform así que este ejemplo es redundante)
[RequireComponent(typeof(Transform))]
public class Movimiento : MonoBehaviour
{

    // va a haber situaciones en donde deba accedr a otro componente
    // voy a necesitar una referencia
    private Transform _transform;
    private IEnumerator _ienumeratorCorrutina;
    private Coroutine _corrutina;

    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private Proyectil _disparoOriginal;

    // ciclo de vida / lifecycle
    // - existen métodos que se invocan en momentos específicos de la vida del script
    
    // Se invoca una vez al inicio de la vida del componente 
    // otra diferencia - awake se invoca aunque objeto esté deshabilitado
    void Awake()
    {
        print("AWAKE");
    }

    // Se invoca una vez después que fueron invocados todos los awakes
    void Start()
    {
        Debug.Log("START");

        // como obtener referencia a otro componente

        // NOTAS:
        // - getcomponent es lento, hazlo la menor cantidad de veces posible
        // - con transform ya tenemos referencia (ahorita lo vemos)
        // - esta operación puede regresar nulo
        _transform = GetComponent<Transform>();
        
        // si tienes require esto ya no es necesario
        Assert.IsNotNull(_transform, "ES NECESARIO PARA MOVIMIENTO TENER UN TRANSFORM");
        Assert.IsNotNull(_disparoOriginal, "DISPARO NO PUEDE SER NULO");
        Assert.AreNotEqual(0, _speed, "VELOCIDAD DEBE SER MAYOR A 0");

        StartCoroutine(CorrutinaDummy());
        //StartCoroutine(DisparoRecurrente());
        _ienumeratorCorrutina = DisparoRecurrente();
    }

    // Update is called once per frame
    // frame? cuadro?
    // fotograma
    // target mínimo - 30 fps
    // ideal - 60+ fps
    void Update(){
        //Debug.LogWarning("UPDATE");

        // SIEMPRE vamos a tratar que este sea lo más magro posible
        // update lo usamos para 2 cosas
        // 1 - entrada de usuario
        // 2 - movimiento

        // ahorita - vamos a hacer polling por dispositivo
        
        // true - cuando en el cuadro anterior estaba libre
        // y en este está presionada
        if(Input.GetKeyDown(KeyCode.Z))
        {
            print("KEY DOWN: Z");
        }

        // true - cuando en el cuadro anterior estaba presionada
        // y en el actual sigue presionada
        if(Input.GetKey(KeyCode.Z))
        {
            print("KEY: Z");
        }

        // true - estaba presionada
        // ya está libre
        if(Input.GetKeyUp(KeyCode.Z))
        {
            print("KEY UP: Z");
        }

        if(Input.GetMouseButtonDown(0))
        {
            print("MOUSE BUTTON DOWN");
        }

        if(Input.GetMouseButton(0))
        {
            print("MOUSE BUTTON");
        }

        if(Input.GetMouseButtonUp(0))
        {
            print("MOUSE BUTTON UP");
        }
        

        // vamos a usar ejes (después)
        // - mapeo de hardware a un valor abstracto llamado eje
        // rango [-1, 1]

        // hacemos polling a eje en lugar de hacerlo a hardware específico
        //float horizontal = Input.GetAxisRaw("Horizontal");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //print(horizontal + " " + vertical);

        // como mover objetos 
        // 4 opciones 
        // 1 - directamente con su transform
        // 2 - por medio de character controller
        // 3 - por medio del motor de física
        // 4 - por medio de navmesh (AI)

        transform.Translate(
            horizontal * _speed * Time.deltaTime, 
            vertical * _speed * Time.deltaTime, 
            0, 
            Space.World
        );

        // se pueden usar ejes como botones
        if(Input.GetButtonDown("Jump"))
        {
            // print("JUMP");
            
            // para detener una corrutina hay que tener referencia a ella en la construccion
            
            // método 1 - EL FEO
            // usando strings
            //StartCoroutine("DisparoRecurrente");
            
            // método 2 - con un IEnumerator
            // StartCoroutine(_ienumeratorCorrutina);

            // método 3 - referencia a corrutina
            _corrutina = StartCoroutine(DisparoRecurrente());
        }

        if(Input.GetButtonUp("Jump"))
        {
            //StopAllCoroutines();

            //StopCoroutine("DisparoRecurrente");
            StopCoroutine(_corrutina);
        }
    }

    // fixed? - fijo
    // update que corre en intervalo fijado en la configuración del proyecto
    // NO puede correr más frecuentemente que update
    void FixedUpdate()
    {
        //Debug.LogError("FIXED UPDATE");
    }

    // corre todos los cuadros
    // una vez que los updates están terminados
    void LateUpdate()
    {
        //print("LATE UPDATE");
    }

    // CÓDIGO MUY ÚTIL
    // HOLA ESTOY EN EL REPO!

    // CORRUTINAS 
    // cuando tenemos la necesidad de hacer código "concurrente" vamos a utilizar corrutinas
    // se comportan como hilos (pero no son)

    // CASO DE USO 1 - CUANDO QUEREMOS CORRER ALGO CON UN RETRASO
    // también pueden usar invoke pero se recomienda corrutina
    IEnumerator CorrutinaDummy()
    {

        yield return new WaitForSeconds(2);
        
        print("HOLA");
    }

    // CASO DE USO 2 - LÓGICA RECURRENTE
    IEnumerator DisparoRecurrente()
    {
        while(true)
        {
            // se pueden hacer game objects vacíos
            // GameObject objeto = new GameObject();

            // si queremos un game object predefinido para clonar
            // usamos instantiate
            Instantiate(
                _disparoOriginal, 
                transform.position, 
                transform.rotation
            );

            yield return new WaitForSeconds(0.3f);
            
        }
    }


    // CASO DE USO 3 (no mostrado) - al esperar respuesta de código asíncrono (async / await)
}
