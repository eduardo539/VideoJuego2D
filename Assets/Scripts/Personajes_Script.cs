using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class Personajes_Script : ScriptableObject
{
    
    public GameObject personajeJugable;

    public Sprite imagen;

    public string nombre;

    public string danoAtaque;

    public string ataque;

    public string equipamiento;
    
}
