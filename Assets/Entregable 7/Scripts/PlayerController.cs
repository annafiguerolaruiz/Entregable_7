using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    [SerializeField] private float jumpForce = 5f;

    private int Counter;

    private AudioSource PlayerAudioSource;

    public AudioClip Boing;
    public AudioClip Blip;
    public AudioClip Boom;

    public bool GameOver;
   

    public ParticleSystem ExplosionParticlesSystem;
    public ParticleSystem FireworkParticlesSystem;
   

    void Start()
    {
        //Al empezar el juego el player esta vivo = GameOver = FALSE
        GameOver = false;
        //Accedemos a la componente Rigidbody 
        playerRigidbody = GetComponent<Rigidbody>();
        //Accedemos al Audio Source del player
        PlayerAudioSource = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        //Podemos seguir pulsando la tecla ESPACIO para impulsarnos siempre que el player esté vivo
        if (Input.GetKeyDown(KeyCode.Space) && !GameOver)
        {
            //Para impulsarnos añadimos una fuerza en el eje Y con una fuerza que aplicamos al momento
            playerRigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            //Cuando aplicamos ese salto suena un clip de audio
            PlayerAudioSource.PlayOneShot(Boing, 1f);
        }
        
    }
    //Funcion con la que controlamos las colisiones que sufre el player
    private void OnCollisionEnter(Collision otherCollider)
    {
        //Estas colisiones solo las sufre cuando esta vivo
        if (!GameOver)
        {
            //Si el player colisiona contra el suelo GAME OVER
            if (otherCollider.gameObject.CompareTag("Ground"))
            {
                GameOver = true;
            }
            //Si el player colisiona contra la bomba GAME OVER
            if (otherCollider.gameObject.CompareTag("Bomb"))
            {
                GameOver = true;
                Destroy(otherCollider.gameObject);
                //Audio de explosion contra la Bomba
                PlayerAudioSource.PlayOneShot(Boom, 1f);
                //Instanciamos la explision y activamos particulas
                Instantiate(ExplosionParticlesSystem, transform.position, ExplosionParticlesSystem.transform.rotation);
                ExplosionParticlesSystem.Play();
            }
            //Si el player colisiona contra la moneda, la recoje, sumandole 1 cada vez
            if (otherCollider.gameObject.CompareTag("Money"))
            {
               //Destruye la moneda la recojerla
                Destroy(otherCollider.gameObject);
                //Suma 1 cada vez al contador
                Counter += 1;
                //Audio de recojida de moneda
                PlayerAudioSource.PlayOneShot(Blip, 1f);
               //Instanciamos los fuegos artificialess y activamos particulas
                Instantiate(FireworkParticlesSystem, transform.position, FireworkParticlesSystem.transform.rotation);
                FireworkParticlesSystem.Play();
            }
        }
    }
}
