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
    public LayerMask mask;

   

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

    public virtual void Start() {
        
    }

    public void TorchPlacement()
    {
        player = PlayerManager.instance.player;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        int WallMask = LayerMask.GetMask("Wall");
        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            float distance = Vector3.Distance(player.transform.position, hit.point);
            Debug.Log(distance);
            if (distance <= minDistance)
            {
                hasWallPosition = true;
                //Debug.Log(hit.point);
                Monobehaviour.instance.StartCoroutine(WaitTime(hit));
                HotKeyBar.instance.RefreshHotkeys();
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
        Vector3 point = hit.point - PlayerManager.instance.player.transform.position;
        // GameObject.Instantiate(TorchObject, point, TorchObject.transform.rotation);
        Debug.Log(point);
        point.z = point.z * -1;
        //GameObject.Instantiate(TorchObject, hit.point, TorchObject.transform.rotation);
        
        GameObject pc = GameObject.Instantiate(TorchObject, hit.point, Quaternion.LookRotation(hit.normal));
        pc.transform.Rotate(new Vector3(-45, 0, 0));


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

    public override int GetPrice() {
        return 10;
    }
}
