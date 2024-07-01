using UnityEngine;

public class LightArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha entrado en el área de luz de " + transform.parent.name);
        }
    }
}
