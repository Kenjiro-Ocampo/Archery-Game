using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Spawner : MonoBehaviour
{
    [SerializeField] private float WaveTime = 15f;
    [SerializeField] private float CurentTime;
    [SerializeField] public GameObject PF_Sheep;
    [SerializeField] public int WaveCount;
    [SerializeField] public List<GameObject> ChildrenGameObjects;
    private void AddDescendantsWithTag(Transform parent, string tag, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                list.Add(child.gameObject);
            }
            AddDescendantsWithTag(child, tag, list);
        }
    }

    private void Awake() 
    {
        WaveCount = 1;
        CurentTime = WaveTime;
        AddDescendantsWithTag(this.gameObject.transform, "Spawner", ChildrenGameObjects); // Add all children to a list
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (CurentTime > 0)
        {
            CurentTime -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i <= WaveCount; i++) // Get random spawner and instanticate there
            {
                Instantiate(PF_Sheep.transform, ChildrenGameObjects[(int)Random.Range(0, ChildrenGameObjects.Count)].transform);
            }
            WaveCount++;
            CurentTime = WaveTime;
        }
    }

}
