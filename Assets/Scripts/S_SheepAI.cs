using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class S_SheepAI : MonoBehaviour
{
    public Transform PlayerPosition;
    private GameObject Player;
    [SerializeField] public NavMeshAgent SheepBrain;

    private void Awake() 
    {
        SheepBrain = GetComponent<NavMeshAgent>();

        Player = GameObject.FindWithTag("Player");
        PlayerPosition = Player.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SheepBrain.destination = PlayerPosition.position + new Vector3(0,0,0);
    }
}
