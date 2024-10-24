using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lsPlayer : MonoBehaviour
{

    public mapHolders currentPoint;
    
    public float moveSpeed = 10f;

    private bool levelLoading;

    public lsManager manager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f)
        {

            if(Input.GetAxisRaw("Horizontal") > 0.5f)
            {
                if(currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }
            if(Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if(currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }

            if(Input.GetAxisRaw("Vertical") > 0.5f)
            {
                if(currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }
            if(Input.GetAxisRaw("Vertical") < -.5f)
            {
                if(currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if(currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                lsUiManager.instance.ShowInfo(currentPoint);

                if(Input.GetButtonDown("Fire1"))
                {
                    levelLoading = true;

                    manager.LoadLevel();
                }
            }

        }
    }

    public void SetNextPoint(mapHolders nextPoint)
    {
        currentPoint = nextPoint;
        lsUiManager.instance.HideInfo();
    }
}
