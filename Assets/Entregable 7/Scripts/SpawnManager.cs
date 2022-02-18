using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float startDelay = 2f;
    private float repeatRate = 1f;

    public GameObject[] Obstacles;
    


    void Start()
    {
        //Buscamos al playerController y accedemos a su script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //Invocamos los objetos en un tiempo determinado (se van llamando cada cieto tiempo)
        InvokeRepeating("SpawnObstacles", startDelay, repeatRate);

    }

   
    //Funcion que guarda su posicion aleatoria en Y & en el limite derecho
    public Vector3 RandomSpawnPosition1()
    {
        return new Vector3(12,Random.Range(2, 14), 0);
    }
    //Funcion que guarda su posicion aleatoria en Y & en el limite izquierdo
    public Vector3 RandomSpawnPosition2()
    {
        return new Vector3(-12,Random.Range(2, 14), 0);
    }
    //Funcion que se encargara de instanciar los objetos 
    public void  SpawnObstacles()
    {
        //Variable que guarda de forma random la posicion de los objetos cuando aparecen
        float randomNumber = Random.Range(0, 2);
        //Variable que guarda de forma random si aparece un objeto u otro dependiendo de la longitud de la lista de los objetos
        int randomIndex = Random.Range(0, Obstacles.Length);
        //Se instanciaran los obstaculos siempre que el player este vivo
        if (!playerControllerScript.GameOver)
        {
            //Solo se instanciaran los obstaculos en el limite derecho siempre y cuando la variable que hemos declarado sea igual a 1
            if (randomNumber == 1 && !playerControllerScript.GameOver)
            {
                //Instanciamos los obstaculos que apafrecen en el limite derecho y los rotamos para que vayan en direccion contraria
                Instantiate(Obstacles[randomIndex], RandomSpawnPosition1(), Quaternion.Euler(new Vector3(0, 180, 0)));
            }
            else
            {
                //Solo se instanciaran los obstaculos en el limite izquierdo siempre y cuando la variable que hemos declarado sno sea igual a 1
                Instantiate(Obstacles[randomIndex], RandomSpawnPosition2(),Obstacles[randomIndex].transform.rotation);
            }
        }
    }
}
