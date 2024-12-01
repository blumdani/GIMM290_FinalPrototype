using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BullMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float speed = 200;
    private bool playerLocked = false;
    private bool finishingRun = false;
    private bool runThroughStarted = false;
    private bool cooldownOn = false;
    private bool invincibility = false;
    
    private Vector3 playerPosition;
    private int targetsLeft = 2;
    private float distance;
    public HealthController hc;


    void Start()
    {
        playerPosition = player.transform.position;
        this.gameObject.GetComponent<BullMovement>().enabled = false;
        StartCoroutine(StartBullPause());
        hc = GameObject.Find("HealthController").GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetsLeft == 0)
        {
            SceneManager.LoadScene("YouWon");
        }

        distance = Vector3.Distance(transform.position, player.transform.position);

        if(cooldownOn == false)
        {
            if(playerLocked == false) {
                LockPlayerPosition();
            }
            Charge(distance);
        }
    }

    private void Charge(float distance) {
        if(finishingRun == false)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            this.gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
            transform.LookAt(player.transform.position);
        }
        else
        {
            //this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * speed * Time.deltaTime;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if(runThroughStarted == false && distance < 40f)
        { 
            StartCoroutine(RunThroughPlayer());
        }
    }

    private void LockPlayerPosition() {
        
        playerPosition = player.transform.position;
        transform.LookAt(playerPosition);
        playerLocked = true;
        StartCoroutine(PlayerWait());
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arena")
        {
            finishingRun = false;
            StartCoroutine(Cooldown());
        }
        else if(collision.gameObject.tag == "Target")
        {
            Destroy(collision.gameObject);
            targetsLeft--;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!invincibility)
            {
                Debug.Log("Player hit");
                Debug.Log("Now invincible");
                StartCoroutine(Invincibility());
                hc.GetComponent<HealthController>().TakeDamage();
                StartCoroutine(PlayerPassThrough());
            }
        }
    }

    IEnumerator PlayerWait() {
        yield return new WaitForSeconds(3);
        playerLocked = false;
    }

    IEnumerator RunThroughPlayer() {
        Debug.Log("RUNNING THROUGH");
        runThroughStarted = true;
        finishingRun = true;
        yield return new WaitForSeconds(6f);
        transform.LookAt(playerPosition);
        finishingRun = false;
        runThroughStarted = false;
    }

    IEnumerator Cooldown() {
        cooldownOn = true;
        yield return new WaitForSeconds(3);
        cooldownOn = false;
    }

    IEnumerator PlayerPassThrough() {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(.3f);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator StartBullPause() {
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<BullMovement>().enabled = true;
    }

    IEnumerator Invincibility() {
        invincibility = true;
        yield return new WaitForSeconds(3);
        invincibility = false;
    }
}
