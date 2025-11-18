using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEmitter2 : MonoBehaviour
{
    void Start()
    {
        var ps = GetComponent<ParticleSystem>();

        // --- MAIN ---
        var main = ps.main;
        main.startLifetime = 0.5f;            // lifespan: 1000 ms
        main.startSpeed = 3f;              // speed ~200 en Phaser (ajustado a escala Unity)
        main.startSize = 0.4f;              // scale start
        main.startRotation = new ParticleSystem.MinMaxCurve(
            Mathf.Deg2Rad * 175f,           // min angle
            Mathf.Deg2Rad * 185f);          // max angle
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        // --- EMISSION ---
        var emission = ps.emission;
        emission.rateOverTime = 50f;        // cantidad de partículas por segundo

        // --- SHAPE ---
        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 2.3f;                   // cono estrecho para simular llama
        shape.radius = 0.0001f;
        shape.rotation = new Vector3(0, 0, 180); // apunta hacia atrás

        // --- COLOR OVER LIFETIME ---
        var col = ps.colorOverLifetime;
        col.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color32(0x96, 0xE0, 0xDA, 255), 0.0f), 
                new GradientColorKey(new Color32(0x93, 0x7E, 0xF3, 255), 1.0f),
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f) // se desvanece
            }
        );
        col.color = new ParticleSystem.MinMaxGradient(grad);

        // --- SIZE OVER LIFETIME ---
        var size = ps.sizeOverLifetime;
        size.enabled = true;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, 1f);   // empieza con 100% del tamaño
        curve.AddKey(1f, 0f);   // termina en 0 (sine.out en Phaser)
        size.size = new ParticleSystem.MinMaxCurve(1f, curve);

        // --- RENDERER ---
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
    }
}
