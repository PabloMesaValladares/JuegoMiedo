using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WindowController : MonoBehaviour
{
    [SerializeField] private GameObject _GameplayManager;

    public Light windowLight;
    public float openDuration;
    public int probabilityToOpen;
    private bool isOpen = false;
    private float openTimer = 0f;

    private int triggerNumber = 0;

    void Start()
    {
        windowLight.enabled = false;
        _GameplayManager = GameObject.FindGameObjectWithTag("GameplayManager");
        probabilityToOpen = _GameplayManager.GetComponent<GameplayManager>().windowsProbability;
        openDuration = _GameplayManager.GetComponent<GameplayManager>().windowsCooldown;
    }

    void Update()
    {
        if (!isOpen)
        {
            triggerNumber = Random.Range(0, probabilityToOpen);
            if (triggerNumber == 1)
            {
                OpenWindow();
            }
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

        // Reactiva la detección en el LightArea cuando la ventana se cierra
        LightArea lightArea = GetComponentInChildren<LightArea>();
        if (lightArea != null)
        {
            lightArea.ReactivateDetection();
        }
    }

    // Método público para verificar si la ventana está abierta
    public bool IsWindowOpen()
    {
        return isOpen;
    }
}