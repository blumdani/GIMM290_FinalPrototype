using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneController : MonoBehaviour {
    [SerializeField] GameObject fanPrefab;
    private GameObject fan;
    private float tempX;
    private float tempZ;
    public float checkRadius = 2.0f; 

    public List<GameObject> fanList = new List<GameObject>();
    public int spawnAmount = 0;
    public float counter = 0;

    void Update() {
        // if (enemy == null) {
        //     enemy = Instantiate(enemyPrefab) as GameObject;
        //     enemy.transform.position = new Vector3(0, 1, 0);
        //     enemy.transform.localScale = new Vector3(1, Random.Range(1.5f, 5.0f), 1);
        //     float angle = Random.Range(0, 360);
        //     enemy.transform.Rotate(0, angle, 0);

        //     // Assign a random color to the enemy
        // Renderer renderer = enemy.GetComponent<Renderer>();
        // renderer.material.color = new Color(Random.value, Random.value, Random.value);
        counter += Time.deltaTime;
        if(counter >= 2.5f && spawnAmount < 25)
        {
            counter = 0;
            spawnAmount++;
        }

        if(fanList.Count < spawnAmount)
        {
            Vector3 randomPosition;
            Collider[] colliders;
            float x, y, z;
            do
            {
                Debug.Log("Checking for spot...");
                // Define the range for your random position
                x = Random.Range(-210.0f, 210.0f);
                y = Random.Range(3f, 3.1f);
                z = Random.Range(240.0f, -240.0f);

                randomPosition = new Vector3(x, y, z);
                // Check if there are any objects at the random position
                colliders = Physics.OverlapSphere(randomPosition, checkRadius);
            }
            while (colliders.Length > 0 || Mathf.Abs(tempX - x) <= 4 || Mathf.Abs(tempZ - z) <= 4); 
            // Repeat until a position with no objects is found and does not overlap too closely with previous spawns.
            
            // Instantiate the prefab at the random position
            fanList.Add(Instantiate(fanPrefab, randomPosition, Quaternion.identity));
            //Save values for comparison against future spawns.
            tempX = x;
            tempZ = z;

            fan = fanList[fanList.Count - 1];
            Renderer renderer = fan.GetComponent<Renderer>();
            fan.transform.Rotate(0, Random.Range(0, 360), 0);
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
