using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Script : MonoBehaviour
{

    public float speed;
    public float dano; // Daño que causará el fuego
    private Rigidbody2D Rigidbody2D;

    private Vector2 Direccion;

    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Rigidbody2D.velocity = Direccion * speed;
    }

    public void setDireccion(Vector2 direccion)
    {
        Direccion = direccion;
    }

    public void DestroyFire()
    {
        Destroy(gameObject);
    }

    public void setDano(float dato)
    {
        dano = dato;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo") || other.CompareTag("Trampa"))
        {
            Vida_Enemigo enemigo = other.GetComponent<Vida_Enemigo>();

            if (enemigo != null)
            {
                enemigo.TomarDano(dano); // Aplica el daño al enemigo
            }

            Destroy(gameObject); // Destruye el objeto de fuego después de causar daño
        }
    }


}
