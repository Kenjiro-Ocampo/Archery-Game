using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class S_SheepAI : MonoBehaviour
{
    [SerializeField] public float MaxHealth;
    [SerializeField] public float Damage = 10;
    [SerializeField] public TMP_Text HealthDisplay;
    private Transform PlayerPosition;
    private GameObject Player;
    private float CurrentHealth;
    private Canvas SheepCanvas;
    [SerializeField] public NavMeshAgent SheepBrain;

    private void Awake() 
    {
        CurrentHealth = MaxHealth;
        
        //GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        SheepBrain = GetComponent<NavMeshAgent>();
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
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
        HealthDisplay.text = MaxHealth.ToString() + " / " + MaxHealth.ToString();
        if(Player != null)
        {
            SheepBrain.destination = PlayerPosition.position + new Vector3(0,0,0);
        }
    }

    void LateUpdate()
    {
        HealthDisplay.transform.LookAt(Camera.main.transform);
        HealthDisplay.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void setHealth(float value)
    {
        CurrentHealth = value;
    }

    public float getCurrentHealth()
    {
        return this.CurrentHealth;
    }

    public float getMaxHealth()
    {
        return this.MaxHealth;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        other.gameObject.GetComponent<S_Character_Combat>().setHealth(Damage);
    }
}
