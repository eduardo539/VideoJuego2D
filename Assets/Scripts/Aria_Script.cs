using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aria_Script : MonoBehaviour
{
    
    public float Speed = 1.5f; // Velocidad de movimiento ajustable
    public float FuerzaSalto = 200f; // Fuerza de salto ajustable

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool puedeDobleSalto = false; // Control para doble salto
    private bool Suelo = false;
    [SerializeField] private Vector2 velocidadRebote;
    private Aria_Ataque ataqueAria;

    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

    private bool controlesBloqueados = false; // Flag para bloquear los controles



    // Se ejecuta al inicio
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        ataqueAria = GetComponent<Aria_Ataque>();

    }


    void Update()
    {

        if (controlesBloqueados) return; // Evita actualizar controles si están bloqueados


        Horizontal = Input.GetAxisRaw("Horizontal");

        // Animación de correr
        Animator.SetBool("running", Horizontal != 0.0f);
        // Animación de saltar
        Animator.SetBool("salto", !Suelo);
        // Animación de segundo salto
        Animator.SetBool("segundoSalto", !puedeDobleSalto);

        HandleAttackInput();// Detectar entrada de ataque

        SpeedHorizontal();// Detectar si avanza a la derecha o izquierda

        DetectSuelo();// Detectar si Maximus está tocando el suelo

        Salto();// Saltar al presionar la tecla W
        

    }


    private void SpeedHorizontal()
    {
        // Cambiar la dirección del sprite según el movimiento
        if (Horizontal < 0.0f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void DetectSuelo()
    {
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.4f))
        {
            Suelo = true;
            puedeDobleSalto = true; // Permitir doble salto al tocar el suelo
        }
        else
        {
            Suelo = false;
        }
    }

    private void Salto()
    {
        if(Input.GetKeyDown(KeyCode.W) && (Suelo || puedeDobleSalto))
        {
            // Reiniciar la velocidad vertical para evitar acumulación de fuerza
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0f);
            
            // Aplicar la misma fuerza de salto tanto para el salto normal como para el doble salto
            Rigidbody2D.AddForce(Vector2.up * FuerzaSalto);
            
            if (!Suelo)
            {
                puedeDobleSalto = false; // Inhabilitar doble salto después de usarlo
            }
        }
    }


    private void FixedUpdate()
    {
        if (controlesBloqueados) return; // Evita movimiento si los controles están bloqueados

        // Movimiento horizontal
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void HandleAttackInput()
    {
        if (controlesBloqueados) return; // Evita ataque si los controles están bloqueados

        if(tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        // Verificar si el jugador presiona la tecla "K" para atacar
        if (Input.GetKeyDown(KeyCode.K) && tiempoSiguienteAtaque <= 0)
        {
            if (Mathf.Abs(Horizontal) > 0.0f) // Verifica si hay movimiento horizontal
            {
                Animator.SetTrigger("attackRunTrigger");
                ataqueAria.DisparoMagia();
            }
            else
            {
                Animator.SetTrigger("attackTrigger");
                StartCoroutine(DispararConRetraso(0.25f)); // Retrasa el ataque solo en este caso

            }
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }

    }

    // Corutina que ejecuta el ataque después de un retraso
    private IEnumerator DispararConRetraso(float retraso)
    {
        yield return new WaitForSeconds(retraso); // Espera el tiempo especificado
        ataqueAria.DisparoMagia(); // Ejecuta el ataque después de la espera
    }


    public void Muerte(){
        Animator.SetTrigger("muerteTrigger");
        StartCoroutine(EsperaMuerte());
    }

    private IEnumerator EsperaMuerte()
    {
        // Espera 1 segundo
        yield return new WaitForSeconds(1f);
        
        // Destruye el objeto
        Destroy(gameObject);
    }

    public void Empujar(Vector2 puntoDeContacto)
    {
        Rigidbody2D.velocity = new Vector2(-velocidadRebote.x * puntoDeContacto.x, velocidadRebote.y);
    }

    public void AnimacionSalida()
    {
        Animator.SetTrigger("salirTrigger");
        controlesBloqueados = true; // Bloquea los controles durante la animación de salida
    }

    public void DesbloquearControles()
    {
        controlesBloqueados = false; // Método para desbloquear controles al entrar en el nuevo nivel
    }

    
}
