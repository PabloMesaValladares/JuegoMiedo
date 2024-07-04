using UnityEngine;

public class LightArea : MonoBehaviour
{
    private WindowController windowController;
    private bool canDetectPlayer = true; // Variable para controlar la detección

    void Start()
    {
        // Obtén la referencia al WindowController del objeto padre
        windowController = GetComponentInParent<WindowController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprueba si la ventana está abierta y la detección está activa
        if (other.CompareTag("Player") && windowController != null && windowController.IsWindowOpen() && canDetectPlayer)
        {
            Debug.Log("El jugador ha entrado en el área de luz de " + transform.parent.name);
            canDetectPlayer = false; // Desactiva la detección hasta que la ventana se cierre y vuelva a abrirse
        }
    }

    // Método público para reactivar la detección
    public void ReactivateDetection()
    {
        canDetectPlayer = true;
    }
}
