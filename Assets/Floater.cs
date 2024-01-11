using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Floater : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] float baseOffset = -1.2f;
    [SerializeField] float waterHight = -4f;
    [SerializeField] float submerge = .2f;
    private int waterMask;
    private float velocity = 0f;
    private int groundMask;
    private NavMeshHit hit;
    [SerializeField] private bool onWater;
    [SerializeField] private bool onGround;
    [Space]
    [SerializeField] private bool calculate;
    [SerializeField] float multiplier = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        waterMask = 1 << NavMesh.GetAreaFromName("Swim");
        groundMask = 1 << NavMesh.GetAreaFromName("Walkable");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print("transform.position.y " + transform.position.y  +" < waterHight " + waterHight +" = "+ (transform.position.y < waterHight));
        if (onWater)
        {
            print("<color=#a7a555>ONWATERRRRRRRRRRRRRRRRRRRRRRRRRR</color>");

            /*
            print("((" + waterHight + "-" + transform.position.y + ")/" + waterHight + ")*" + baseOffset + "\n=" +
                +((waterHight - transform.position.y) / waterHight) + "*" + baseOffset + "\n=" +
                 ((waterHight - transform.position.y) / waterHight) * baseOffset);
            */
            if (calculate)
            {
                multiplier = Mathf.Clamp01((waterHight - transform.position.y) / submerge);

                print("((" + waterHight + "-" + transform.position.y + ")/" + submerge + "\n=" +
                +(waterHight - transform.position.y) + ")/" + submerge + "\n=" +
                 multiplier);
                agent.baseOffset = Mathf.SmoothDamp(agent.baseOffset, multiplier,ref velocity, 0.3f);
                print("<color=#555a7a>~~~~~multiplier</color>" + agent.baseOffset);
                if (waterHight <= transform.position.y)
                {
                    //agent.baseOffset = ((waterHight - transform.position.y)/ waterHight) * baseOffset *-1;
                    //print("<color=#a7aa7a>~~~~~If</color>" + agent.baseOffset);
                }
                else
                {
                    //agent.baseOffset = baseOffset;
                    //print("<color=#555>~~~~~else</color>"+ agent.baseOffset);
                }
                /*if (!animator.GetBool("OnWater"))
                    animator.SetBool("OnWater", onWater);
            */
                //rdController.Gravity = 0;
                //multiplier = Mathf.Clamp01((waterHight - transform.position.y) / submerge) * displacment;
                //Vector3 force = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * multiplier, 0f);
                //rigidbody.AddForce(force, ForceMode.Acceleration);
            }
            else
            {
                //rdController.Gravity = oldGravity;
                print("-------------------------------------------");
              /*  if (animator.GetBool("OnWater"))
                    animator.SetBool("OnWater", false);
              */
            }
        }
        else
        {
            print("elseelseelseelseelseelseelseelseelse");
            if (animator.GetBool("OnWater"))
            {
                print("<color=#a7a555>elseelseelseelseelseelseelseelseelse</color>");
                //Debug.Break();
                agent.baseOffset = 0F;
                animator.SetBool("OnWater", false);
            }
        
        }
    }

    private void Update()
    {
        agent?.SamplePathPosition(1, 1000, out hit);
        
        onWater = hit.mask == waterMask;
        onGround = hit.mask == groundMask;
    }
}
