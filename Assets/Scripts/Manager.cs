using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject prefab;
    public float spawnDelay = 0.1f;
    public int spawnAmount = 1;
    float timer = 0;
    public Transform parent;

    LoaderHeadless loader; 
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnDelay;
        loader = FindObjectOfType<LoaderHeadless>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject go = Instantiate(prefab, parent);
                go.transform.position += new Vector3(Random.Range(-15, 15), 0, 0);
                loader.ballAmount++;
            }
            timer = spawnDelay;
        }
    }
}
