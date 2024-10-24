using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class cameraController : MonoBehaviour
{

    public static cameraController instance;

    public Transform target;

    public Transform first, second, third, fourth, five;

    public float minHeigth, maxHeigth;

    private Vector2 lastPos;

    public float smoothness;
    private Vector3 vel;
    public Vector3 offset;

    public bool stopFollow;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movimiento de la camara

        if(!stopFollow)
        {
            //transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            //transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeigth, maxHeigth), transform.position.z);
            Vector3 tar = target.position + offset;
            tar = new Vector3(tar.x, Mathf.Clamp(tar.y, minHeigth, maxHeigth), tar.z);
            transform.position = Vector3.SmoothDamp(transform.position, tar, ref vel, smoothness);


            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            five.position = five.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
            fourth.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .7f;
            third.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
            second.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .3f;
            first.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .1f;

            lastPos = transform.position;
        }
    }
}
