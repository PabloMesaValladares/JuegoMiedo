using UnityEngine;

public class WindowController : MonoBehaviour
{
    public Light windowLight;
    public float openDuration = 5f;
    public float probabilityToOpen = 0.1f;
    private bool isOpen = false;
    private float openTimer = 0f;

    void Start()
    {
        windowLight.enabled = false;
    }

    void Update()
    {
        if (!isOpen && Random.value < probabilityToOpen * Time.deltaTime)
        {
            OpenWindow();
        }

        if (isOpen)
        {
            openTimer += Time.deltaTime;
            if (openTimer >= openDuration)
            {
                CloseWindow();
            }
        }
    }

    void OpenWindow()
    {
        isOpen = true;
        openTimer = 0f;
        windowLight.enabled = true;
        Debug.Log("Ventana abierta: " + gameObject.name);
    }

    void CloseWindow()
    {
        isOpen = false;
        windowLight.enabled = false;
        Debug.Log("Ventana cerrada: " + gameObject.name);
    }
}