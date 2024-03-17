using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 CurrentVelocity = Vector3.zero;
    [SerializeField] private float Smoothing = 0.5f;
    [SerializeField] private GameObject Player;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 target = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref CurrentVelocity, Smoothing);
    }
}

