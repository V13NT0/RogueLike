using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyEyeController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;

    public SpriteRenderer sr;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTarget;

    public float waitAfterAttack;
    private float attackCounter;

    void Start()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;

        } else
        {
            if(Vector3.Distance(transform.position, playerController.instance.hitPoint.position) > distanceToAttackPlayer)
            {

                attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, points[currentPoint].position) < 0.05f)
                {
                    currentPoint++;

                    if(currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }

                if(transform.position.x < points[currentPoint].position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);

                }else if(transform.position.x > points[currentPoint].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            } else
            {
                //atacando al jugador
                if(attackTarget == Vector3.zero)
                {
                    attackTarget = playerController.instance.hitPoint.position;
                }
                
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                if(Vector3.Distance(transform.position, attackTarget) <= .1f)
                {
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }

            }

        }
    }
}
