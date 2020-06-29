using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Torch", menuName = "Inventory/Consumable/Torch")]
public class Torch : Consumables, IUsable
{
    private bool hasWallPosition;
    public GameObject TorchObject;
    public float minDistance;
    Inventory inventory;
    GameObject player;

   

    public override void Use()
    {
        base.Use();
        inventory = Inventory.instance;
        TorchPlacement();
        if (hasWallPosition)
        {
            RemoveFromInventory();
            hasWallPosition = false;
        }
    }

    public void TorchPlacement()
    {
        player = PlayerManager.instance.player;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        int WallMask = LayerMask.GetMask("Wall");
        if (Physics.Raycast(ray, out hit, 100, WallMask))
        {
            float distance = Vector3.Distance(player.transform.position, hit.point);
            Debug.Log(distance);
            if (distance <= minDistance)
            {
                hasWallPosition = true;
                Debug.Log(hit.point);
                Monobehaviour.instance.StartCoroutine(WaitTime(hit));
                //Place torch at hitpoint
                
            }

        }
    }


    IEnumerator WaitTime(RaycastHit hit)
    {
        Transform[] ts = PlayerManager.instance.player.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        {
            Debug.Log(t.gameObject.name);
            if (t.gameObject.name == "PlayerTorch")
            {
                t.gameObject.SetActive(true);
                break;
            }

        }
        yield return new WaitForSeconds(0.5f);

        foreach (Transform t in ts)
        {
            Debug.Log(t.gameObject.name);
            if (t.gameObject.name == "PlayerTorch")
            {
                t.gameObject.SetActive(false);
                break;
            }
            

        }
        GameObject.Instantiate(TorchObject, hit.point, TorchObject.transform.rotation);
    }

    

           
            

        
    

    
    public Sprite Image
    {
        get
        {
            return icon;
        }
        set
        {
            return;
        }



    }
}
