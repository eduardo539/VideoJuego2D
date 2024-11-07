using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida_Level1 : MonoBehaviour
{
    [SerializeField] private int cantidadEnemigos;
    [SerializeField] private int enemigosEliminados;
    private Animator animator;
    private Maximus_Script maximus; // Referencia al script del jugador
    private Aria_Script aria; // Referencia al script del jugador


    void Start()
    {
        maximus = FindObjectOfType<Maximus_Script>();
        aria = FindObjectOfType<Aria_Script>();
        animator = GetComponent<Animator>();
        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemigo").Length;
    }

    public void ActivarSalida()
    {
        if (animator != null)
        {
            animator.SetTrigger("salidaTrigger");
        }
        else
        {
            Debug.LogWarning("La salida no cuenta con animación");
        }
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados += 1;

        if (enemigosEliminados == cantidadEnemigos)
        {
            ActivarSalida();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemigosEliminados == cantidadEnemigos)
        {
            if (maximus != null)
            {
                maximus.AnimacionSalida();
            }
            else if(aria != null)
            {
                aria.AnimacionSalida();
            }
            else
            {
                Debug.LogError("No estan asignados los personajes en el objeto.");
            }
            StartCoroutine(EsperaSalida());
        }
    }


    private IEnumerator EsperaSalida()
    {
        // Espera 1 segundo
        yield return new WaitForSeconds(0.5f);
        aria.DesbloquearControles();
        
        // Cambiar a la escena del menú de niveles
        SceneManager.LoadScene("Menu_Niveles");
    }


}














/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Aqui se llama la siguiente escena

public class Salida_Level1 : MonoBehaviour
{
    [SerializeField] private int cantidadEnemigos;
    [SerializeField] private int enemigosEliminados;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cantidadEnemigos = GameObject.FindGameObjectsWithTag("Enemigo").Length;
    }

    public void ActivarSalida()
    {
        // Comprobar si el Animator tiene el estado de animación asociado con "salidaTrigger"
        if (animator != null)
        {
            animator.SetTrigger("salidaTrigger");
        }
        else
        {
            Debug.LogWarning("La salida no cuenta con animación");
        }
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados += 1;

        if (enemigosEliminados == cantidadEnemigos)
        {
            ActivarSalida();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemigosEliminados == cantidadEnemigos)
        {
            // Cambiar a la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
*/