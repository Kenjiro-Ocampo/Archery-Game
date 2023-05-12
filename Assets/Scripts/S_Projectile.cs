using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum Projectile_Type
{
    Force, Ice, Fire
}

public class S_Projectile : MonoBehaviour
{
    [SerializeField] public float ProjectileSpeed = 10f;
    [SerializeField] public int ProjType;
    [SerializeField] public Material S_DMAT_Fo;
    [SerializeField] public Material S_DMAT_Fi;
    [SerializeField] public Material S_DMAT_Ic;
    private S_SheepAI SheepSRC;
    private Rigidbody Projectile_RB;
    private void Awake() 
    {
        Projectile_RB = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Projectile_RB.velocity = transform.forward * ProjectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        switch ((Projectile_Type)ProjType) // Unfinished impelementation of different proejctiles
        {
            case Projectile_Type.Force:
                SheepSRC = other.gameObject.GetComponent<S_SheepAI>();

                if(SheepSRC.getCurrentHealth() <= 0f)
                {
                    SheepSRC.gameObject.GetComponent<MeshRenderer>().material = S_DMAT_Fo;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<S_Character_Combat>().AddPoints(5);
                    Destroy(SheepSRC.GetComponent<S_SheepAI>());
                    Destroy(SheepSRC.gameObject, 5);
                }

                SheepSRC.setHealth(SheepSRC.getCurrentHealth() - 1f);
                
            break;
            case Projectile_Type.Fire:
                SheepSRC = other.gameObject.GetComponent<S_SheepAI>();

                if(SheepSRC.getCurrentHealth() <= 0f)
                {
                    SheepSRC.gameObject.GetComponent<MeshRenderer>().material = S_DMAT_Fi;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<S_Character_Combat>().AddPoints(2);
                    Destroy(SheepSRC.GetComponent<S_SheepAI>());
                    Destroy(SheepSRC.gameObject, 5);
                }

                SheepSRC.setHealth(SheepSRC.getCurrentHealth() - 2f);

            break;
            case Projectile_Type.Ice:
                SheepSRC = other.gameObject.GetComponent<S_SheepAI>();

                if(SheepSRC.getCurrentHealth() <= 0f)
                {
                    SheepSRC.gameObject.GetComponent<MeshRenderer>().material = S_DMAT_Ic;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<S_Character_Combat>().AddPoints(8);
                    Destroy(SheepSRC.GetComponent<S_SheepAI>());
                    Destroy(SheepSRC.gameObject, 5);
                }

                SheepSRC.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                SheepSRC.setHealth(SheepSRC.getCurrentHealth() - 0.5f);

            break;
            default:
                Destroy(this.gameObject);
            break;
        }
    }
}
