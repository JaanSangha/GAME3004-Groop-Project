using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : PlatformController
{
    private Color originalColor;
    private GameObject floor;
    public float finalAlpha = 0.0f;
    [Tooltip("The point of the textures alpha where the collider component disappears")]
    public float IntangibilityAlpha = 0.5f;
    private MeshRenderer meshRender;
    private Material floorMat;
    private Material[] materials;
    private Collider platformCollider;

    void Start()
    {
        // floor = transform.GetChild(0).gameObject;
        // meshRender = floor.GetComponent<MeshRenderer>();
        // floorMat = meshRender.material;
        // originalColor = floorMat.color;

        // floorMat.SetOverrideTag("RenderType", "Transparent");
        // floorMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        // floorMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        // floorMat.SetInt("_ZWrite", 0);
        // floorMat.DisableKeyword("_ALPHATEST_ON");
        // floorMat.EnableKeyword("_ALPHABLEND_ON");
        // floorMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        // floorMat.renderQueue = 3000;

        materials = new Material[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            floor = transform.GetChild(i).gameObject;
            meshRender = floor.GetComponent<MeshRenderer>();
            materials[i] = meshRender.material;
            originalColor = materials[i].color;
            
            // Switches the material (instance) render mode to transparent
            materials[i].SetOverrideTag("RenderType", "Transparent");
            materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[i].SetInt("_ZWrite", 0);
            materials[i].DisableKeyword("_ALPHATEST_ON");
            materials[i].EnableKeyword("_ALPHABLEND_ON");
            materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[i].renderQueue = 3000;
        }
        
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
        
        //floorMat.color = Color.Lerp(originalColor, finalColor, speedCurve.Evaluate(Mathf.PingPong(percentComplete, 1)));
        // if(floorMat.color.a < IntangibilityAlpha)
        // {
        //     platformCollider.enabled = false;
        // }
        // else if(floorMat.color.a >= IntangibilityAlpha)
        // {
        //     platformCollider.enabled = true;
        // }
        
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.Lerp(originalColor, finalColor, speedCurve.Evaluate(Mathf.PingPong(percentComplete, 1)));
            if(materials[i].color.a < IntangibilityAlpha)
            {
                platformCollider.enabled = false;
            }
            else if(materials[i].color.a >= IntangibilityAlpha)
            {
                platformCollider.enabled = true;
            }
        }
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.JUMP_LAND_BRIDGE, other.gameObject);
            
            Debug.Log("Player collision");
            isActive = true;
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isActive = false;
            other.transform.SetParent(null);
        }
    }
}
