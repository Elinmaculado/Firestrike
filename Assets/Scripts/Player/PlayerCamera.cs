using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    public float horizontalOffset;
    public float smoothing;
    private Vector3 targetPos;
    Vector3 direction;


    private void Awake()
    {
    }

    private void Update()
    {
        direction = new Vector3(0, 0, -1);
        targetPos = player.transform.position + direction; 
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }
}
