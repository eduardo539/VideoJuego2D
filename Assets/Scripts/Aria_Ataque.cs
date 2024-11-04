using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aria_Ataque : MonoBehaviour
{
    public GameObject Fire_Aria;
    [SerializeField] private float danoGolpe;

    public void DisparoMagia()
    {
        Vector3 direccion = transform.localScale.x == 1.0f ? Vector2.right : Vector2.left;
        GameObject Fire = Instantiate(Fire_Aria, transform.position + direccion * 0.1f, Quaternion.identity);

        // Asignamos la dirección y el daño al fuego
        Fire.GetComponent<Fire_Script>().setDireccion(direccion);
        //Fire.GetComponent<Fire_Script>().dano = danoGolpe;
        Fire.GetComponent<Fire_Script>().setDano(danoGolpe);

    }
}










/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aria_Ataque : MonoBehaviour
{
    public GameObject Fire_Aria;
    
    [SerializeField] private Transform controlador_Ataque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpe;

    public void DisparoMagia()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controlador_Ataque.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if(colisionador.CompareTag("Enemigo") || colisionador.CompareTag("Trampa"))
            {
                Vector3 direccion;
                if(transform.localScale.x == 1.0f) direccion = Vector2.right;
                else direccion = Vector2.left;

                GameObject Fire = Instantiate(Fire_Aria, transform.position + direccion * 0.1f , Quaternion.identity);

                // Establecemos la dirección y el daño para el fuego
                Fire.GetComponent<Fire_Script>().setDireccion(direccion);
                Fire.GetComponent<Fire_Script>().dano = danoGolpe;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controlador_Ataque.position, radioGolpe);
    }
}
*/


//colisionador.transform.GetComponent<Vida_Enemigo>().TomarDano(danoGolpe);