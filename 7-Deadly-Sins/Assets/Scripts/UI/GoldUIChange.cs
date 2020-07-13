using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUIChange : MonoBehaviour
{
    [SerializeField]
    float verticalSpeed;
    [SerializeField]
    float duration;

    float timePassedSinceTrigger = 0;
    bool displayTriggered = false;
    bool moveUp = false;

    bool prepareToDisable = false;
    
    [SerializeField]
    Text text;
    [SerializeField]
    Animator animator;

    Coroutine currentCoroutine;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp) {
            this.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        if (prepareToDisable && 
        !animator.GetCurrentAnimatorStateInfo(0).IsName("GoldCounterUpdateOpen") &&
         !animator.GetCurrentAnimatorStateInfo(0).IsName("GoldCounterUpdateClose")) {
            moveUp = false;
            Destroy(this.gameObject);
        }
        
    }

    public void DisplayGoldChange(int difference) {
        if (difference > 0) {
        text.text = "+" + difference.ToString();
        }
        else if (difference < 0) {
            text.text = difference.ToString();
        }

        if (difference != 0) {
            if (currentCoroutine != null) {
                StopCoroutine(currentCoroutine);
            }
            //this.transform.position = originalPosition;
            currentCoroutine = StartCoroutine(Display());
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Display() {
        moveUp = true;
        yield return new WaitForSeconds(duration);
        animator.SetBool("close", true);
        prepareToDisable = true;
    }
}
