using UnityEngine;

public class HighlightOutline : MonoBehaviour
{
    private Material originalMaterial;
    public Material outlineMaterial;
    //private Outline outline;

    void Start()
    {
        // Store the original material of the object
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Perform the raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //If the ray hits the object we want to highlight, set its material to the outline material
            if (hit.collider.gameObject == gameObject)
            {
                GetComponent<Renderer>().material = outlineMaterial;
                //outline = GetComponent<Outline>();
                //if (outline != null) {
                //    outline.OutlineColor = outline.OutlineColor;
                //    outline.OutlineWidth = outline.OutlineWidth;
                //    outline.enabled = true;
                //}
            }
            // If the ray does not hit the object, set its material back to the original material
            else
            {
                GetComponent<Renderer>().material = originalMaterial;
            }
        }
        else
        {
            // If the ray does not hit anything, set the object's material back to the original material
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
