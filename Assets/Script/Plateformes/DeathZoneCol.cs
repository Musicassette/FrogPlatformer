using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZoneCol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SendMessage("Mort", SendMessageOptions.DontRequireReceiver);
    }
}
