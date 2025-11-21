using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;

    private float limiteX;
    private float limiteY;
    private float mitadAncho;
    private float mitadAlto;
    private Animator animator;
    [SerializeField] private float hudAbajo;
    [SerializeField] private float hudArriba;

    [Header("Configuración de Control")]
    [SerializeField] private bool usarControlesTactiles = true;
    [SerializeField] private bool dispararAlTocar = true;

    [Header("Joystick Virtual")]
    [SerializeField] private float radioJoystick = 100f;
    [SerializeField] private float deadZone = 0.1f;
    [SerializeField] private Color colorBase = new Color(1f, 1f, 1f, 0.3f);
    [SerializeField] private Color colorStick = new Color(1f, 1f, 1f, 0.6f);
    
    // Componentes del joystick
    private GameObject joystickCanvas;
    private GameObject joystickBase;
    private GameObject joystickStick;
    private RectTransform baseRect;
    private RectTransform stickRect;
    private Image baseImage;
    private Image stickImage;
    
    private bool tocandoPantalla = false;
    private Vector2 inputVector;
    private int touchID = -1;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        mitadAncho = sr.bounds.extents.x;
        mitadAlto = sr.bounds.extents.y;
        animator = GetComponent<Animator>();
        
        CrearJoystickVirtual();
    }

    void CrearJoystickVirtual()
    {
        // Crear Canvas para el joystick
        joystickCanvas = new GameObject("JoystickCanvas");
        Canvas canvas = joystickCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        
        CanvasScaler scaler = joystickCanvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        joystickCanvas.AddComponent<GraphicRaycaster>();

        // Crear base del joystick (círculo exterior)
        joystickBase = new GameObject("JoystickBase");
        joystickBase.transform.SetParent(joystickCanvas.transform);
        baseRect = joystickBase.AddComponent<RectTransform>();
        baseRect.sizeDelta = new Vector2(radioJoystick * 2, radioJoystick * 2);
        
        baseImage = joystickBase.AddComponent<Image>();
        baseImage.sprite = CrearSpriteCírculo();
        baseImage.color = colorBase;
        baseImage.raycastTarget = false;

        // Crear stick del joystick (círculo interior)
        joystickStick = new GameObject("JoystickStick");
        joystickStick.transform.SetParent(joystickBase.transform);
        stickRect = joystickStick.AddComponent<RectTransform>();
        stickRect.sizeDelta = new Vector2(radioJoystick * 0.6f, radioJoystick * 0.6f);
        stickRect.anchoredPosition = Vector2.zero;
        
        stickImage = joystickStick.AddComponent<Image>();
        stickImage.sprite = CrearSpriteCírculo();
        stickImage.color = colorStick;
        stickImage.raycastTarget = false;

        // Ocultar el joystick al inicio
        joystickCanvas.SetActive(false);
    }

    Sprite CrearSpriteCírculo()
    {
        // Crear textura circular
        int tamaño = 128;
        Texture2D textura = new Texture2D(tamaño, tamaño);
        Color[] pixeles = new Color[tamaño * tamaño];
        
        Vector2 centro = new Vector2(tamaño / 2f, tamaño / 2f);
        float radio = tamaño / 2f;
        
        for (int y = 0; y < tamaño; y++)
        {
            for (int x = 0; x < tamaño; x++)
            {
                Vector2 pos = new Vector2(x, y);
                float distancia = Vector2.Distance(pos, centro);
                
                if (distancia <= radio)
                {
                    // Suavizar bordes
                    float alpha = 1f;
                    if (distancia > radio - 5)
                    {
                        alpha = (radio - distancia) / 5f;
                    }
                    pixeles[y * tamaño + x] = new Color(1f, 1f, 1f, alpha);
                }
                else
                {
                    pixeles[y * tamaño + x] = Color.clear;
                }
            }
        }
        
        textura.SetPixels(pixeles);
        textura.Apply();
        
        return Sprite.Create(textura, new Rect(0, 0, tamaño, tamaño), new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        if (usarControlesTactiles)
        {
            ProcesarControlesTactiles();
            
            if (tocandoPantalla)
            {
                MoverConJoystick();
            }
        }
        else
        {
            MovimientoTeclado();
        }
    }

    void ProcesarControlesTactiles()
    {
        // Si no hay toques y había uno activo
        if (Input.touchCount == 0 && touchID != -1)
        {
            OcultarJoystick();
            return;
        }

        // Procesar toques
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Nuevo toque
            if (touch.phase == TouchPhase.Began && touchID == -1)
            {
                // Verificar que no esté en zona de HUD
                if (!ToqueEnZonaValida(touch))
                    continue;

                touchID = touch.fingerId;
                MostrarJoystick(touch.position);
            }

            // Toque activo
            if (touch.fingerId == touchID)
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    ActualizarJoystick(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    OcultarJoystick();
                }
            }
        }
    }

    void MostrarJoystick(Vector2 posicionPantalla)
    {
        joystickCanvas.SetActive(true);
        baseRect.position = posicionPantalla;
        stickRect.anchoredPosition = Vector2.zero;
        
        tocandoPantalla = true;
        inputVector = Vector2.zero;
        
        // Animación suave de aparición
        StopAllCoroutines();
        StartCoroutine(AnimarAparicion());
    }

    IEnumerator AnimarAparicion()
    {
        float duracion = 0.15f;
        float tiempo = 0f;
        
        Vector3 escalaInicial = Vector3.zero;
        Vector3 escalaFinal = Vector3.one;
        
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;
            
            // Ease out
            progreso = 1f - Mathf.Pow(1f - progreso, 3f);
            
            joystickBase.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, progreso);
            yield return null;
        }
        
        joystickBase.transform.localScale = escalaFinal;
    }

    void ActualizarJoystick(Vector2 posicionToque)
    {
        // Calcular dirección desde el centro del joystick
        Vector2 direccion = posicionToque - (Vector2)baseRect.position;
        
        // Limitar al radio del joystick
        float distancia = direccion.magnitude;
        if (distancia > radioJoystick)
        {
            direccion = direccion.normalized * radioJoystick;
        }
        
        // Actualizar posición del stick
        stickRect.anchoredPosition = direccion;
        
        // Calcular input normalizado
        inputVector = direccion / radioJoystick;
        
        // Aplicar dead zone
        if (inputVector.magnitude < deadZone)
        {
            inputVector = Vector2.zero;
        }
    }

    void OcultarJoystick()
    {
        touchID = -1;
        tocandoPantalla = false;
        inputVector = Vector2.zero;
        joystickCanvas.SetActive(false);
        ResetearAnimaciones();
    }

    void MoverConJoystick()
    {
        if (inputVector.magnitude < deadZone)
        {
            ResetearAnimaciones();
            return;
        }
        
        // Mover el jugador
        Vector3 movimiento = new Vector3(inputVector.x, inputVector.y, 0f);
        transform.position += movimiento * velocidad * Time.deltaTime;
        
        // Actualizar animaciones
        ActualizarAnimaciones(movimiento.normalized);
        
        // Aplicar límites
        AplicarLimites();
    }

    void MovimientoTeclado()
    {
        float movX = 0f;
        float movY = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movX = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movX = 1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movY = 1f;
            animator.SetBool("Up", true);
        }
        else
        {
            animator.SetBool("Up", false);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movY = -1f;
            animator.SetBool("Down", true);
        }
        else
        {
            animator.SetBool("Down", false);
        }

        Vector3 movimiento = new Vector3(movX, movY, 0f).normalized;
        transform.position += movimiento * velocidad * Time.deltaTime;

        AplicarLimites();
    }

    void ActualizarAnimaciones(Vector3 direccion)
    {
        if (Mathf.Abs(direccion.y) > 0.1f)
        {
            if (direccion.y > 0)
            {
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
            }
            else if (direccion.y < 0)
            {
                animator.SetBool("Down", true);
                animator.SetBool("Up", false);
            }
        }
        else
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }
    }

    void ResetearAnimaciones()
    {
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
    }

    bool ToqueEnZonaValida(Touch touch)
    {
        Vector3 posicionMundo = Camera.main.ScreenToWorldPoint(touch.position);
        Camera cam = Camera.main;
        float limiteYCam = cam.orthographicSize;
        
        // Zona válida: mitad izquierda de la pantalla y no en HUD
        bool enMitadIzquierda = touch.position.x < Screen.width * 0.5f;
        bool fueraDeHUD = posicionMundo.y > (-limiteYCam + 1.5f) && 
                          posicionMundo.y < (limiteYCam - 1.5f);
        
        return enMitadIzquierda && fueraDeHUD;
    }

    void AplicarLimites()
    {
        Camera cam = Camera.main;
        limiteY = cam.orthographicSize;
        limiteX = limiteY * cam.aspect;

        float x = Mathf.Clamp(transform.position.x, -limiteX + mitadAncho, limiteX - mitadAncho);
        float y = Mathf.Clamp(transform.position.y, -limiteY + mitadAlto + hudAbajo, limiteY - mitadAlto - hudArriba);
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public bool EstaTocandoPantalla()
    {
        return tocandoPantalla;
    }

    public Vector2 GetInputVector()
    {
        return inputVector;
    }

    void OnDestroy()
    {
        if (joystickCanvas != null)
        {
            Destroy(joystickCanvas);
        }
    }
}