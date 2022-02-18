using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displacement : MonoBehaviour
{
    private float derecha = 20f;
    private float izquierda = -20f;
    public float speed = 10f;
    private PlayerController playerControllerScript;
   
    void Start()
    {
        //Buscamos al playerController y accedemos a su script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Los obstaculos se mueven constantemente hacia la derecha,siempre que este vivo el player
        if (!playerControllerScript.GameOver)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        //Todos los objectos que  crucen el limite derecho se deestruyen
        if (transform.position.x > derecha)
        {
            Destroy(gameObject);
        }
        //Todos los objetos que crucen el limite izquierdo se destruyen
        if (transform.position.x < izquierda)
        {
            Destroy(gameObject);
        }


            
    }
   
}
