using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject targetToStart;
    [SerializeField]
    GameObject target;

    bool gameStarted = false;
    float timer = 0;
    [SerializeField]
    float roundTimer = 60;
    [SerializeField]
    float spawnTime = 3;
    float currentSpawnTimer = 100;
    [SerializeField]
    float maxSpawn = 50;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject pointText;
    [SerializeField]
    AudioSource kirby;
    bool sound = false;
    float points = 0;

    static public GameManager gm;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(this);
    }

    void FixedUpdate()
    {
        if (gameStarted)
        {
            if (!sound)
            {
                kirby.Play();
                sound = true;
            }
            timer += Time.deltaTime;
            currentSpawnTimer += Time.deltaTime;
            if (spawnTime < currentSpawnTimer)
            {
                currentSpawnTimer = 0;

                float spawnPoint1 = Random.Range(-maxSpawn, maxSpawn);
                float spawnPoint2 = Random.Range(-maxSpawn, maxSpawn);

                while (Mathf.Abs(spawnPoint1) < 2) { spawnPoint1 = Random.Range(-maxSpawn, maxSpawn); }
                while (Mathf.Abs(spawnPoint2) < 2) { spawnPoint2 = Random.Range(-maxSpawn, maxSpawn); }

                Vector3 position = new Vector3(spawnPoint1, 0.85f, spawnPoint2);
                GameObject instTarget = Instantiate(target, position, Quaternion.LookRotation(position));
                instTarget.transform.rotation = Quaternion.Euler(-90, instTarget.transform.rotation.eulerAngles.y, instTarget.transform.rotation.eulerAngles.z);
            }
        }
    }

    public void pointsAchieved(float points)
    {
        this.points += points;
        pointText.GetComponent<TextMesh>().text = "Points: " + this.points;
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        gameStarted = true;
    }
}
