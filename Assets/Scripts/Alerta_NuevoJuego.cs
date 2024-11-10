using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Alerta_NuevoJuego : MonoBehaviour
{
    [SerializeField] private GameObject alertaNewJuego;

    private Puntaje_Script puntaje;

    private Menu_Principal menuPrincipal;

    void Start()
    {
        puntaje = FindObjectOfType<Puntaje_Script>();
        menuPrincipal = GetComponent<Menu_Principal>();
        alertaNewJuego.SetActive(false);

        if(menuPrincipal != null)
        {
            menuPrincipal.alertaNewJuego += ActivarMenu;
        }
        else
        {
            Debug.Log("La referencia a Menu principal no se encuentra");
        }
        
    }

    public void ActivarMenu(object sender, EventArgs e)
    {
        // Asegúrate de que alertaNewJuego esté asignado en el inspector
        if (alertaNewJuego != null)
        {
            alertaNewJuego.SetActive(true);
        }
        else
        {
            Debug.LogError("El objeto 'alertaNewJuego' no está asignado.");
        }
    }

    public void Aceptar()
    {
        puntaje.ReiniciarPuntaje();
        SceneManager.LoadScene("Menu_Personajes");
    }

    public void Cancelar()
    {
        //SceneManager.LoadScene("Menu_Principal");
        alertaNewJuego.SetActive(false); // Cierra solo la ventana de alerta
    }
    
}
