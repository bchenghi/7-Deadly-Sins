using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private MeshRenderer renderer;
    public float maxOutlineWidth;
    public Color OutlineColor;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public void ShowOutline()
    {
        
        foreach(var mat in renderer.materials)
        {
        mat.SetFloat("_Outline", maxOutlineWidth);
        mat.SetColor("_OutlineColor", OutlineColor);
        }
        //renderer.material.SetFloat("_Outline", maxOutlineWidth);
       // renderer.material.SetColor("_OutlineColor", OutlineColor);

        
    }

    public void HideOutline()
    {
        
        foreach (var mat in renderer.materials)
        {
            mat.SetFloat("_Outline", 0f);
        }
            
    }
}
