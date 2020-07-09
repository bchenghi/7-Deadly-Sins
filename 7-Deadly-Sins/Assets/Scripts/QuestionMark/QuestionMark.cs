using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    public GameObject floatingQuestion;
    public string LocationName;
    public GameObject SpawnObject;
    private bool hasitem;
    public bool isChecked;
    public bool completed;

    private void Start()
    {
        
        GameObject Go = GameObject.Instantiate(floatingQuestion, transform.position, Quaternion.identity, transform);
        
        Debug.Log(Go.transform.position + "Object");
        Debug.Log(transform.position + "Transform");
        isChecked = false;
        if (SpawnObject != null)
        {
            hasitem = true;
        } else
        {
            hasitem = false;
        }
    }

    private void Update()
    {
        if (!completed)
        {
            if (isChecked)
            {
                if (hasitem)
                {
                    SpawnsObject();
                } else
                {
                    completed = true;
                }
            }
        }
    }

    private void SpawnsObject()
    {
        GameObject Go = GameObject.Instantiate(SpawnObject, transform.position, SpawnObject.transform.rotation);
        Go.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        completed = true;
    }

    public bool checkItem()
    {
        return hasitem;
    }
}
