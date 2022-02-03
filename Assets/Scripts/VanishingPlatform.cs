using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    private Color originalColor;
    private GameObject floor;
    public bool isActive = false;
    public float desiredTime = 5f;
    public float finalAlpha = 0.0f;
    public AnimationCurve speedCurve;
    private float elapsedTime;
    private MeshRenderer meshRender;
    private Material floorMat;
    private Collider platformCollider;

    void Start()
    {
        floor = transform.GetChild(0).gameObject;
        meshRender = floor.GetComponent<MeshRenderer>();
        floorMat = meshRender.material;
        originalColor = floorMat.color;

        // Switches the material (instance) render mode to transparent
        floorMat.SetOverrideTag("RenderType", "Transparent");
        floorMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        floorMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        floorMat.SetInt("_ZWrite", 0);
        floorMat.DisableKeyword("_ALPHATEST_ON");
        floorMat.EnableKeyword("_ALPHABLEND_ON");
        floorMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        floorMat.renderQueue = 3000;


        finalAlpha = Mathf.Clamp01(finalAlpha);
        //floorMat.color = new Color(originalColor.r, originalColor.g, originalColor.b, finalAlpha);

        platformCollider = transform.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Vanish();
    }

    void Vanish()
    {
        Color finalColor = new Color(originalColor.r, originalColor.g, originalColor.b, finalAlpha);
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / desiredTime;
        floorMat.color = Color.Lerp(originalColor, finalColor, speedCurve.Evaluate(percentComplete));
        
        if(percentComplete >= 0.5f)
        {
            platformCollider.enabled = false;
        }
    }
}
