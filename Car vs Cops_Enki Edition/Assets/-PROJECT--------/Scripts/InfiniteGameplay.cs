using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player goes too far, replace it, and the cop and the coins near the middle
/// </summary>
public class InfiniteGameplay : MonoBehaviour
{
    public Transform plane;


    public Transform player;
    public Transform camera;
    public Transform coinSpawner;
    public Transform copSpawner;

    public float limit = 500;

    private float distance;

    /// <summary>
    /// The size of a square on the ground
    /// </summary>
    public float gridSize = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        distance = Vector3.Distance(player.position, plane.position);
        if (distance > limit)
            Reposition();

    }

    private void Reposition()
    {

        int numberOfSquaresX = (int)((player.position - plane.position).x / gridSize);
        int numberOfSquaresZ = (int)((player.position - plane.position).z / gridSize);

        Vector3 displacement = new Vector3(numberOfSquaresX, 0, numberOfSquaresZ) * gridSize * -1;

        plane.position -= displacement;
        return;

        //Previously, I wanted to re center all the entities, but the trails and the particles were not right, so I just re-rentered the plane on the player
        //But it is not infinite, the player can reach the float limit, but, it is really high

        player.position += displacement;
        camera.position += displacement;
        coinSpawner.position += displacement;
        copSpawner.position += displacement;

    }
}
