using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable 
{
    void Use();

    Sprite Image { get; set; }
}
