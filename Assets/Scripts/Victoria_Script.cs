using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Victoria_Script : MonoBehaviour
{
    
    [SerializeField] private GameObject menuVictoria;

    private Salida_Level1 salidaNivel;

    private Puntaje_Script puntaje;



    void Start()
    {
        puntaje = FindObjectOfType<Puntaje_Script>();

        menuVictoria.SetActive(false);
        
        
        GameObject player = GameObject.FindGameObjectWithTag("Salida_1");
        if (player != null)
        {
            salidaNivel = player.GetComponent<Salida_Level1>();
            if (salidaNivel != null)
            {
                salidaNivel.VictoriaJugador += ActivarMenu;
            }
            else
            {
                Debug.LogError("El objeto con la etiqueta 'Salida_1' no tiene el componente Salida_Level1.");
            }
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Salida_1'");
        }

    }

    public void ActivarMenu(object sender, EventArgs e)
    {
        menuVictoria.SetActive(true);
    }


    public void Continuar()
    {
        puntaje.GuardarPuntosAlCompletarNivel();
        SceneManager.LoadScene("Menu_Niveles");
    }


}
