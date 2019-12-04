using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform player;
    public float distFromPlayer;
    public GameObject[] Prefabs;

    public float spawningInterval = 5;
    private float timer;

    public bool active = false;

    public bool checkDistance = false;
    public float maxDistDestroy = 150;

    private List<GameObject> spawned = new List<GameObject>();

    /// <summary>
    /// Called by the EventTrigger in CanvasStart
    /// </summary>
    public void StartGame()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

        SpawnTimer();
    }

    private void SpawnTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Spawn();

            //Theses 2 methods are called here and not in Update to reduce impact on perfs
            CheckNullrefs();
            CheckDistance();
            timer += spawningInterval;
        }

    }



    private void CheckNullrefs()
    {
        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            if (spawned[i] == null)
                spawned.RemoveAt(i);
        }
    }

    private void CheckDistance()
    {
        if (!checkDistance)
            return;

        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(spawned[i].transform.position, player.position) > maxDistDestroy)
            {
                Destroy(spawned[i]);
                spawned.RemoveAt(i);
            }
        }

    }

    private void Spawn()
    {
        Vector3 rand = Random.insideUnitSphere;
        rand.y = 0;
        GameObject spawn = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], player.transform.position + rand.normalized * distFromPlayer, Quaternion.identity, transform);

        spawned.Add(spawn);
    }

    internal void EndGame()
    {
        active = false;
    }
}
