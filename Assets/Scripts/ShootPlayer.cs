using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootPlayer : MonoBehaviour
{
    [Header("Fire Points")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;

    [Header("Bullets")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bullet2;
    
    [Header("Power Up")]
    [SerializeField] private GameObject powerUpActivo;
    
    [Header("Shoot Settings")]
    [SerializeField] private float tiempoEntreAtaques;
    private float tiempoSiguienteAtaque;
    private bool puedeDisparar;
    private bool itemCreado;
    private bool disparoDoble = false;

    [Header("Mobile Controls")]
    [SerializeField] private bool useMobileControls = true;
    [SerializeField] private float posicionVertical = 300f;
    
    // Referencias del HUD creadas automáticamente
    private GameObject shootButtonObj;
    private Button shootButton;
    private Image shootButtonBackground;
    private Image shootButtonIcon;
    private Image cooldownFill;
    private Image buttonGlow;

    void Start()
    {
        puedeDisparar = true;
        itemCreado = false;
        powerUpActivo = GameObject.Find("CanvasPowerUpActivo");
        
        // Detectar si es móvil automáticamente
        #if UNITY_ANDROID || UNITY_IOS
            useMobileControls = true;
        #endif

        if (useMobileControls)
        {
            CrearHUDDisparo();
        }
    }

    void CrearHUDDisparo()
    {
        // Buscar o crear el Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        // Crear objeto principal del botón - TAMAÑO AUMENTADO
        shootButtonObj = new GameObject("ShootButton");
        shootButtonObj.transform.SetParent(canvas.transform, false);
        
        RectTransform buttonRect = shootButtonObj.AddComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(200, 200); // Aumentado de 150 a 200
        buttonRect.anchorMin = new Vector2(1, 0);
        buttonRect.anchorMax = new Vector2(1, 0);
        buttonRect.pivot = new Vector2(1, 0);
        buttonRect.anchoredPosition = new Vector2(-30, posicionVertical);

        // Añadir componente Button
        shootButton = shootButtonObj.AddComponent<Button>();
        shootButton.onClick.AddListener(ShootMobile);

        // === BACKGROUND (Círculo principal) ===
        GameObject backgroundObj = new GameObject("Background");
        backgroundObj.transform.SetParent(shootButtonObj.transform, false);
        shootButtonBackground = backgroundObj.AddComponent<Image>();
        
        RectTransform bgRect = backgroundObj.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.sizeDelta = Vector2.zero;
        bgRect.anchoredPosition = Vector2.zero;
        
        // Crear sprite circular perfecto
        shootButtonBackground.sprite = CrearSpriteCircularPerfecto(512);
        shootButtonBackground.color = new Color(0.15f, 0.15f, 0.15f, 0.95f);
        shootButtonBackground.raycastTarget = true;
        shootButton.targetGraphic = shootButtonBackground;

        // === GLOW (Brillo exterior) ===
        GameObject glowObj = new GameObject("Glow");
        glowObj.transform.SetParent(shootButtonObj.transform, false);
        buttonGlow = glowObj.AddComponent<Image>();
        
        RectTransform glowRect = glowObj.GetComponent<RectTransform>();
        glowRect.anchorMin = Vector2.zero;
        glowRect.anchorMax = Vector2.one;
        glowRect.sizeDelta = new Vector2(30, 30);
        glowRect.anchoredPosition = Vector2.zero;
        glowRect.SetAsFirstSibling();
        
        buttonGlow.sprite = CrearSpriteCircularBlur(512);
        buttonGlow.color = new Color(1f, 0.3f, 0f, 0.4f);
        buttonGlow.raycastTarget = false;

        // === COOLDOWN FILL (Indicador radial) ===
        GameObject cooldownObj = new GameObject("CooldownFill");
        cooldownObj.transform.SetParent(shootButtonObj.transform, false);
        cooldownFill = cooldownObj.AddComponent<Image>();
        
        RectTransform cooldownRect = cooldownObj.GetComponent<RectTransform>();
        cooldownRect.anchorMin = Vector2.zero;
        cooldownRect.anchorMax = Vector2.one;
        cooldownRect.sizeDelta = Vector2.zero;
        cooldownRect.anchoredPosition = Vector2.zero;
        
        cooldownFill.sprite = CrearSpriteCircularPerfecto(512);
        cooldownFill.type = Image.Type.Filled;
        cooldownFill.fillMethod = Image.FillMethod.Radial360;
        cooldownFill.fillOrigin = (int)Image.Origin360.Top;
        cooldownFill.fillClockwise = true;
        cooldownFill.fillAmount = 0;
        cooldownFill.color = new Color(1f, 0.3f, 0f, 0.6f);
        cooldownFill.raycastTarget = false;

        // === ICON (Mira de arma con anillos) ===
        GameObject iconObj = new GameObject("Icon");
        iconObj.transform.SetParent(shootButtonObj.transform, false);
        shootButtonIcon = iconObj.AddComponent<Image>();
        
        RectTransform iconRect = iconObj.GetComponent<RectTransform>();
        iconRect.anchorMin = Vector2.zero;
        iconRect.anchorMax = Vector2.one;
        iconRect.sizeDelta = new Vector2(-40, -40);
        iconRect.anchoredPosition = Vector2.zero;
        
        // Crear sprite de mira profesional con anillos
        shootButtonIcon.sprite = CrearSpriteMiraDeArma();
        shootButtonIcon.color = new Color(1f, 0.3f, 0f, 0.9f); // Color naranja/rojo
        shootButtonIcon.raycastTarget = false;

        Debug.Log("HUD de disparo creado exitosamente - Tamaño: 200x200");
    }

    Sprite CrearSpriteCircularPerfecto(int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        Color[] pixels = new Color[resolution * resolution];
        
        Vector2 center = new Vector2(resolution / 2f, resolution / 2f);
        float radius = resolution / 2f - 2f; // Pequeño margen para círculo perfecto
        
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 pos = new Vector2(x + 0.5f, y + 0.5f); // Centrado de píxel
                float distance = Vector2.Distance(pos, center);
                
                if (distance <= radius)
                {
                    // Anti-aliasing en el borde
                    float alpha = 1f;
                    if (distance > radius - 2f)
                    {
                        alpha = 1f - (distance - (radius - 2f)) / 2f;
                    }
                    pixels[y * resolution + x] = new Color(1, 1, 1, alpha);
                }
                else
                {
                    pixels[y * resolution + x] = Color.clear;
                }
            }
        }
        
        texture.SetPixels(pixels);
        texture.Apply();
        
        return Sprite.Create(texture, new Rect(0, 0, resolution, resolution), new Vector2(0.5f, 0.5f), 100f);
    }

    Sprite CrearSpriteCircularBlur(int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        Color[] pixels = new Color[resolution * resolution];
        
        Vector2 center = new Vector2(resolution / 2f, resolution / 2f);
        float radius = resolution / 2f;
        
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
                float distance = Vector2.Distance(pos, center);
                
                float alpha = 1f - Mathf.Clamp01(distance / radius);
                alpha = Mathf.Pow(alpha, 2.5f); // Blur más suave y definido
                
                pixels[y * resolution + x] = new Color(1, 1, 1, alpha);
            }
        }
        
        texture.SetPixels(pixels);
        texture.Apply();
        
        return Sprite.Create(texture, new Rect(0, 0, resolution, resolution), new Vector2(0.5f, 0.5f), 100f);
    }

    Sprite CrearSpriteMiraDeArma()
    {
        int resolution = 512;
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        Color[] pixels = new Color[resolution * resolution];
        
        // Inicializar todo transparente
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }
        
        Vector2 center = new Vector2(resolution / 2f, resolution / 2f);
        
        // Función auxiliar para dibujar círculo
        System.Action<float, float, Color> DibujarAnillo = (radio, grosor, color) =>
        {
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
                    float distance = Vector2.Distance(pos, center);
                    
                    if (distance >= radio - grosor / 2f && distance <= radio + grosor / 2f)
                    {
                        // Anti-aliasing
                        float distanciaDesdeLinea = Mathf.Abs(distance - radio);
                        float alpha = 1f - Mathf.Clamp01((distanciaDesdeLinea - grosor / 2f + 1f) / 2f);
                        Color pixelColor = color;
                        pixelColor.a *= alpha;
                        
                        // Mezclar con el color existente
                        Color existing = pixels[y * resolution + x];
                        pixels[y * resolution + x] = Color.Lerp(existing, pixelColor, pixelColor.a);
                    }
                }
            }
        };
        
        // Función para dibujar línea horizontal o vertical
        System.Action<bool, float, float, float, Color> DibujarLinea = (esHorizontal, posicion, inicio, fin, color) =>
        {
            for (int i = (int)inicio; i < (int)fin; i++)
            {
                for (int grosor = -2; grosor <= 2; grosor++)
                {
                    int x = esHorizontal ? i : (int)posicion + grosor;
                    int y = esHorizontal ? (int)posicion + grosor : i;
                    
                    if (x >= 0 && x < resolution && y >= 0 && y < resolution)
                    {
                        float alpha = 1f - Mathf.Abs(grosor) / 3f;
                        Color pixelColor = color;
                        pixelColor.a *= alpha;
                        
                        Color existing = pixels[y * resolution + x];
                        pixels[y * resolution + x] = Color.Lerp(existing, pixelColor, pixelColor.a);
                    }
                }
            }
        };
        
        // === DIBUJAR MIRA ===
        
        // Anillo exterior grueso
        DibujarAnillo(240f, 6f, Color.white);
        
        // Anillo medio
        DibujarAnillo(180f, 4f, Color.white);
        
        // Anillo interior
        DibujarAnillo(120f, 4f, Color.white);
        
        // Punto central (pequeño círculo sólido)
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
                float distance = Vector2.Distance(pos, center);
                
                if (distance <= 8f)
                {
                    float alpha = 1f;
                    if (distance > 6f)
                    {
                        alpha = 1f - (distance - 6f) / 2f;
                    }
                    pixels[y * resolution + x] = new Color(1, 1, 1, alpha);
                }
            }
        }
        
        // Cruz de mira (líneas que salen del centro)
        float lineaInicio = 15f;
        float lineaFin = 80f;
        
        // Línea superior
        DibujarLinea(false, center.x, center.y + lineaInicio, center.y + lineaFin, Color.white);
        
        // Línea inferior
        DibujarLinea(false, center.x, center.y - lineaFin, center.y - lineaInicio, Color.white);
        
        // Línea izquierda
        DibujarLinea(true, center.y, center.x - lineaFin, center.x - lineaInicio, Color.white);
        
        // Línea derecha
        DibujarLinea(true, center.y, center.x + lineaInicio, center.x + lineaFin, Color.white);
        
        // Marcas adicionales en los anillos (4 puntos cardinales en el anillo medio)
        System.Action<float, float> DibujarMarca = (angulo, radio) =>
        {
            float rad = angulo * Mathf.Deg2Rad;
            float x = center.x + Mathf.Cos(rad) * radio;
            float y = center.y + Mathf.Sin(rad) * radio;
            
            for (int dy = -3; dy <= 3; dy++)
            {
                for (int dx = -3; dx <= 3; dx++)
                {
                    int px = (int)(x + dx);
                    int py = (int)(y + dy);
                    
                    if (px >= 0 && px < resolution && py >= 0 && py < resolution)
                    {
                        float dist = Mathf.Sqrt(dx * dx + dy * dy);
                        if (dist <= 3f)
                        {
                            float alpha = 1f - dist / 4f;
                            Color pixelColor = new Color(1, 1, 1, alpha);
                            Color existing = pixels[py * resolution + px];
                            pixels[py * resolution + px] = Color.Lerp(existing, pixelColor, alpha);
                        }
                    }
                }
            }
        };
        
        // Marcas en los puntos cardinales del anillo medio
        DibujarMarca(0f, 180f);    // Derecha
        DibujarMarca(90f, 180f);   // Arriba
        DibujarMarca(180f, 180f);  // Izquierda
        DibujarMarca(270f, 180f);  // Abajo
        
        texture.SetPixels(pixels);
        texture.Apply();
        
        return Sprite.Create(texture, new Rect(0, 0, resolution, resolution), new Vector2(0.5f, 0.5f), 100f);
    }

    void Update()
    {
        // Cooldown timer
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
            ActualizarCooldownVisual();
        }

        // Efecto de brillo pulsante
        if (useMobileControls && buttonGlow != null)
        {
            float pulso = Mathf.PingPong(Time.time * 2f, 1f);
            buttonGlow.color = new Color(1f, 0.3f, 0f, 0.2f + pulso * 0.3f);
        }

        // Controles PC
        if (!useMobileControls)
        {
            ShootPC();
        }
    }

    void ShootPC()
    {
        if (!disparoDoble)
        {
            if (Input.GetButtonDown("Jump") && tiempoSiguienteAtaque <= 0 && puedeDisparar)
            {
                DisparoSimple();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && tiempoSiguienteAtaque <= 0 && puedeDisparar)
            {
                DisparoDobleFunc();
            }
        }
    }

    void ShootMobile()
    {
        if (tiempoSiguienteAtaque <= 0 && puedeDisparar)
        {
            if (!disparoDoble)
            {
                DisparoSimple();
            }
            else
            {
                DisparoDobleFunc();
            }
        }
    }

    void DisparoSimple()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.shoot);
        tiempoSiguienteAtaque = tiempoEntreAtaques;
    }

    void DisparoDobleFunc()
    {
        Instantiate(bullet, firePoint1.position, firePoint1.rotation);
        Instantiate(bullet2, firePoint2.position, firePoint2.rotation);
        AudioManager.Instance.PlayAudio(AudioManager.Instance.shoot);
        tiempoSiguienteAtaque = tiempoEntreAtaques;
    }

    void ActualizarCooldownVisual()
    {
        if (cooldownFill != null && useMobileControls)
        {
            float porcentajeCooldown = tiempoSiguienteAtaque / tiempoEntreAtaques;
            cooldownFill.fillAmount = porcentajeCooldown;

            // Cambiar color del botón según estado
            if (shootButtonBackground != null)
            {
                if (porcentajeCooldown > 0)
                {
                    shootButtonBackground.color = new Color(0.25f, 0.25f, 0.25f, 0.95f);
                }
                else
                {
                    shootButtonBackground.color = new Color(0.15f, 0.15f, 0.15f, 0.95f);
                }
            }
        }
    }

    public void ActivarDisparoDoble(float duracion)
    {
        disparoDoble = true;
        powerUpActivo.GetComponent<CanvasPowerUp>().EmpezarCorrutina();
        CancelInvoke("DesactivarDisparoDoble");
        Invoke("DesactivarDisparoDoble", duracion);

        // Efecto visual en el botón de disparo
        if (useMobileControls && shootButtonBackground != null)
        {
            StartCoroutine(EfectoDisparoDobleBoton());
        }
    }

    IEnumerator EfectoDisparoDobleBoton()
    {
        Color colorOriginal = new Color(0.15f, 0.15f, 0.15f, 0.95f);
        Color colorPowerUp = new Color(1f, 0.5f, 0f, 1f);

        while (disparoDoble)
        {
            // Pulso de color
            float tiempo = 0;
            while (tiempo < 0.5f && disparoDoble)
            {
                tiempo += Time.deltaTime;
                float lerp = Mathf.PingPong(tiempo * 4f, 1f);
                shootButtonBackground.color = Color.Lerp(colorOriginal, colorPowerUp, lerp);
                
                if (buttonGlow != null)
                {
                    buttonGlow.color = new Color(1f, 0.5f, 0f, 0.5f + lerp * 0.5f);
                }
                
                yield return null;
            }
        }

        shootButtonBackground.color = colorOriginal;
        if (buttonGlow != null)
        {
            buttonGlow.color = new Color(1f, 0.3f, 0f, 0.4f);
        }
    }

    void DesactivarDisparoDoble()
    {
        disparoDoble = false;
        FindAnyObjectByType<ShootPlayer>().SetItemCreado(false);
    }

    public bool GetItemCreado()
    {
        return itemCreado;
    }

    public void SetItemCreado(bool item)
    {
        itemCreado = item;
    }
}