using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : PlayerController
{
    // Start is called before the first frame update
    public override void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
