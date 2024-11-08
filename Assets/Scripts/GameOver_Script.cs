using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameOver_Script : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;

    private Vida_Player vidaMaximus;

    private Vida_Aria vidaAria;

    private Puntaje_Script puntaje;

    void Start()
    {
        puntaje = FindObjectOfType<Puntaje_Script>();
        menuGameOver.SetActive(false);
        StartCoroutine(EsperarYAsignarJugador());
    }

    private IEnumerator EsperarYAsignarJugador()
    {
        yield return new WaitForSeconds(1f);  // Espera un breve momento para que el personaje sea instanciado

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            vidaMaximus = player.GetComponent<Vida_Player>();
            if (vidaMaximus != null)
            {
                vidaMaximus.MuerteJugador += ActivarMenu;
            }
            else
            {
                vidaAria = player.GetComponent<Vida_Aria>();

                if(vidaAria != null)
                {
                    vidaAria.MuerteJugador += ActivarMenu;
                }
                else
                {
                    Debug.LogError("El objeto con la etiqueta 'Player' no tiene el componente del personaje seleccionado.");
                }
            }
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player'.");
        }
    }

    public void ActivarMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial()
    {
        puntaje.ResetearPuntosTemporales();
        SceneManager.LoadScene("Menu_Personajes");
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
