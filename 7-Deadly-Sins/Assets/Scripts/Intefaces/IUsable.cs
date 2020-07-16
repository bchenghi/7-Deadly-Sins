using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable 
{
    void Start();
    void Use();

    Sprite Image { get; set; }
}
