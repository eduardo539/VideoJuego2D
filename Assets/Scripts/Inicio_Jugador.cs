using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio_Jugador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int IndexJugador = PlayerPrefs.GetInt("JuagdorIndex");

        // Instanciar el personaje seleccionado
        GameObject personajeInstanciado = Instantiate(GameManager.Instance.personajes[IndexJugador].personajeJugable, transform.position, Quaternion.identity);

        // Buscar la cámara y asignarle el personaje instanciado
        Camara_Script camaraScript = Camera.main.GetComponent<Camara_Script>();
        if (camaraScript != null)
        {
            camaraScript.AsignarJugador(personajeInstanciado);
        }
    }
}








/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio_Jugador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int IndexJugador = PlayerPrefs.GetInt("JuagdorIndex");

        Instantiate(GameManager.Instance.personajes[IndexJugador].personajeJugable, transform.position, Quaternion.identity);
    }

}
*/