using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public float speed;
    public float depth = -0.5f;
    public GameObject PlayerModel;
    public bool MouseAim;

    public Player PlayerObject;

    private Rigidbody2D rb2d;

    private CollectItems CollItems;
    void Update()
    {
        if (!isLocalPlayer) return;

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if (CollItems != null && CollItems.currentItem != null) rb2d.velocity = movement * (speed - 0.5f);
        else rb2d.velocity = movement * speed;

        if (MouseAim)
        {
            faceMouse();
        }
        else
        {
            var rotate = Input.GetAxis("LookHorizontal") * Time.deltaTime * 150.0f;
            transform.Rotate(0, 0, rotate);
        }

        if (Input.GetButtonDown("ToggleMouse"))
        {
            MouseAim = !MouseAim;
        }
    }

    void faceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 dir = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );

        transform.up = dir;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        CollItems = GetComponent<CollectItems>();

        PlayerSpawnPoint spawnPoint;
        if (PlayerObject.Team == PlayerType.Guard)
        {
            var spawnPoints = new List<PlayerSpawnPoint>(FindObjectsOfType<PlayerSpawnPoint>()).FindAll(x => x.PlayerSpawnType == PlayerType.Guard);
            int index = Random.Range(0, spawnPoints.Count);
            spawnPoint = spawnPoints[index];
        }
        else
        {
            var spawnPoints = new List<PlayerSpawnPoint>(FindObjectsOfType<PlayerSpawnPoint>()).FindAll(x => x.PlayerSpawnType == PlayerType.Robber);
            int index = Random.Range(0, spawnPoints.Count);
            spawnPoint = spawnPoints[index];
        }

        transform.position = spawnPoint.transform.position;
        transform.rotation = spawnPoint.transform.rotation;

        transform.position = new Vector3(transform.position.x, transform.position.y, depth);

        if (!isLocalPlayer) return;
    }
}