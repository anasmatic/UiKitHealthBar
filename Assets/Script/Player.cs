
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] public HealthBar healthBar;
    [Space]
    [HideInInspector] public float hitPoints = 100;

    private float Time;
    [SerializeField] public int TimerMax;
    [SerializeField] public NavMeshAgent Nav;
    [SerializeField] public Vector3 Target;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 100;
        Nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Time += UnityEngine.Time.deltaTime;
        if(Time >= TimerMax)
        {
            GoToAnotherTarget();
        }
    }

    private void LateUpdate()
    {
        //UpdateUI
        healthBar.MoveAndScaleToWorldPosition(transform.position);
    }

    private void GoToAnotherTarget()
    {
        float xPos = transform.position.x + Random.Range(transform.position.x - 50, transform.position.x + 50);
        float zPos = transform.position.z + Random.Range(transform.position.z - 50, transform.position.z + 50);
        Target = new Vector3(xPos, transform.position.y, zPos);
        var val = Nav.SetDestination(Target);
        if (val)
            Time = 0;
    }
}
