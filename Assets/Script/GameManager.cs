using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager UIManager;
    private Player player;
    private Camera cam;
    [SerializeField] Player PlayerPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        UIManager = GetComponentInChildren<UIManager>();
    }

    void Start()
    {
        player = Instantiate(PlayerPrefab);
        UIManager.AddHealthUI(player);

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = player.transform.position - cam.transform.position;
        cam.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
    }
}
