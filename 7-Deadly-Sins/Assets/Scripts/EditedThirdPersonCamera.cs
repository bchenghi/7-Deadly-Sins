using UnityEngine;


public class EditedThirdPersonCamera : MonoBehaviour
{
    float moveSpeed;
    SkinnedMeshRenderer targetRenderer;
    Transform target;
    float closestDistanceToPlayer;


    private void Start()
    {
        moveSpeed = GetComponentInParent<ParentCameraController>().moveSpeed;
        targetRenderer = GetComponentInParent<ParentCameraController>().targetRenderer;
        target = GetComponentInParent<ParentCameraController>().target;
        closestDistanceToPlayer = GetComponentInParent<ParentCameraController>().dstToFade;
    }

    public void ShiftTransform(Vector3 newPosition)
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }


    public void TransparencyCheck()
    {
        if (Vector3.Distance(transform.position, target.position) <= closestDistanceToPlayer)
        {
            /*foreach (Material element in targetRenderer.sharedMaterials)
            {
                Color temp = element.color;
                temp.a = Mathf.Lerp(temp.a, 0.2f, moveSpeed * Time.deltaTime);

                targetRenderer.sharedMaterial.color = temp;

            } */
            for (int i = 0; i < targetRenderer.sharedMaterials.Length; i++ )
            {
                Color temp = targetRenderer.sharedMaterials[i].color;
                temp.a = Mathf.Lerp(temp.a, 0.2f, moveSpeed * Time.deltaTime);

                targetRenderer.sharedMaterials[i].color = temp;
            }
        }
        else
        {
            /*foreach (Material element in targetRenderer.sharedMaterials)
            {
                if (element.color.a <= 0.99f)
                {
                    Color temp = element.color;
                    temp.a = Mathf.Lerp(temp.a, 1, moveSpeed * Time.deltaTime);

                    targetRenderer.sharedMaterial.color = temp;
                }
            } */

            for (int i = 0; i < targetRenderer.sharedMaterials.Length; i++)
            {
                if (targetRenderer.sharedMaterials[i].color.a <= 0.99f)
                {
                    Color temp = targetRenderer.sharedMaterials[i].color;
                    temp.a = Mathf.Lerp(temp.a, 1, moveSpeed * Time.deltaTime);

                    targetRenderer.sharedMaterials[i].color = temp;
                }
            }
        }
    }
}

  
