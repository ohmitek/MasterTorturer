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
                meshRend.material.color = Color.yellow;
            }
            else
            {
                meshRend.material.color = Color.white;
            }
        }
        else
        {
            meshRend.material.color = Color.white;
        }
    }
}
