using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrilloManager : MonoBehaviour
{
    public static BrilloManager Instance;

    private Canvas brilloCanvas;
    private Image overlay;

    [Header("Configuración del brillo")]
    [Range(0f, 1f)] public float brilloActual = 1f;
    [SerializeField] private float brilloMin = 0f;
    [SerializeField] private float brilloMax = 0.7f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            CrearOverlay();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Cargar brillo guardado
        brilloActual = PlayerPrefs.GetFloat("Brillo", 1f);
        AplicarBrillo();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasignar cámara del canvas al cambiar de escena (por si la escena tiene otra cámara)
        if (brilloCanvas != null)
            brilloCanvas.worldCamera = Camera.main;
    }

    private void CrearOverlay()
    {
        // Crear un Canvas solo para el brillo
        GameObject canvasObj = new GameObject("BrilloCanvas");
        brilloCanvas = canvasObj.AddComponent<Canvas>();
        brilloCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        DontDestroyOnLoad(canvasObj);

        // Crear la imagen negra que cubre toda la pantalla
        GameObject imageObj = new GameObject("BrilloOverlay");
        imageObj.transform.SetParent(canvasObj.transform, false);
        overlay = imageObj.AddComponent<Image>();

        // Color inicial (transparente)
        overlay.color = new Color(0, 0, 0, 0f);

        // 🚫 Importante: que no bloquee clics
        overlay.raycastTarget = false;

        // Ajustar tamaño a toda la pantalla
        RectTransform rect = overlay.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    public void CambiarBrillo(float valor)
    {
        brilloActual = valor;
        AplicarBrillo();
        PlayerPrefs.SetFloat("Brillo", brilloActual);
    }

    public void AplicarBrillo()
    {
        if (overlay == null) return;

        float alpha = Mathf.Lerp(brilloMax, brilloMin, brilloActual);
        Color color = overlay.color;
        color.a = alpha;
        overlay.color = color;
    }
}
