using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class S_Character_Combat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HUD_Lose;
    [SerializeField] private TextMeshProUGUI HUD_Health;
    [SerializeField] private TextMeshProUGUI HUD_Points;
    [SerializeField] private TextMeshProUGUI HUD_Force;
    [SerializeField] private TextMeshProUGUI HUD_Ice;
    [SerializeField] private TextMeshProUGUI HUD_Fire;
    [SerializeField] private Transform SpawnBulletPos;
    [SerializeField] private Transform PF_Force_Projectile;
    [SerializeField] private Transform PF_Ice_Projectile;
    [SerializeField] private Transform PF_Fire_Projectile;
    
    [SerializeField] LayerMask ProjectileColliderMask;
    private RaycastHit RayHit;
    private Transform CurrentProjectile;
    [SerializeField] public float MaxHealth;
    private float CurrentHealth;
    private float Points;
    private bool bLose;
    // Start is called before the first frame update
    void Start()
    {
        bLose = false;

        Points = 0f;
        CurrentHealth = MaxHealth;
        CurrentProjectile = PF_Force_Projectile;
        HUD_Force.color = Color.yellow;
        HUD_Ice.color = Color.white;
        HUD_Fire.color = Color.white;

        HUD_Force.text = "Force";
        HUD_Ice.text = "Ice";
        HUD_Fire.text = "Fire";
    }
    private void Awake() {
        HUD_Lose.text = "You Lose! \n Press 'r' to restart";
        HUD_Lose.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (bLose)
        {
            CurrentHealth = 0;
            Time.timeScale = 0;
        }
        HUD_Health.text = "Health: " + CurrentHealth.ToString() + " : " + MaxHealth.ToString();
        HUD_Points.text = "Points: " + Points.ToString();
        Vector3 MousePos = Vector3.zero;
        Vector2 ScreenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray RayFromCenterScreen = Camera.main.ScreenPointToRay(ScreenCenter);
        if(Physics.Raycast(RayFromCenterScreen, out RaycastHit RayHit, 999f, ProjectileColliderMask))
        {
            MousePos = RayHit.point;
        }

        //Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Dev Play Ground");
        }

        // Shooting Balls
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 AimDirec = (MousePos - SpawnBulletPos.position).normalized;
            Transform ProjInstance = Instantiate(CurrentProjectile, SpawnBulletPos.position, Quaternion.LookRotation(AimDirec, Vector3.up));
            Destroy(ProjInstance.gameObject, 10);
        }

        // Projectile Selection
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HUD_Force.color = Color.yellow;
            HUD_Ice.color = Color.white;
            HUD_Fire.color = Color.white;
            CurrentProjectile = PF_Force_Projectile;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HUD_Force.color = Color.white;
            HUD_Ice.color = Color.white;
            HUD_Fire.color = Color.red;
            CurrentProjectile = PF_Fire_Projectile;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HUD_Force.color = Color.white;
            HUD_Ice.color = Color.blue;
            HUD_Fire.color = Color.white;
            CurrentProjectile = PF_Ice_Projectile;
        }
    }

    public void AddPoints(int val)
    {
        this.Points += val;
    }

    public void setHealth(float val)
    {
        CurrentHealth -= val;

        if (CurrentHealth <= 0)
        {
            bLose = true;
            HUD_Lose.gameObject.SetActive(true);
        }
    }
}
