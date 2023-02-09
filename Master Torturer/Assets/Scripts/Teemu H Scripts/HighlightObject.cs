using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    private MeshRenderer meshRend;

    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                meshRend.sharedMaterial.color = Color.yellow;
            }
            else
            {
                meshRend.sharedMaterial.color = Color.white;
            }
        }
        else
        {
            meshRend.sharedMaterial.color = Color.white;
        }
    }
}
