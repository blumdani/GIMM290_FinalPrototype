using System.Collections;
using TMPro;
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
    private Vector3 direction;
    private int targetsLeft = 2;
    private float distance;
    public HealthController hc;
    public CaptureData cd;
    public TMP_Text timeText;


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
            cd.SaveData("Completed Target Mode in " + timeText.text + "\n");
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
            direction = (player.transform.position - transform.position).normalized;
            this.gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
            Vector3 targetPosition = new Vector3( player.transform.position.x, this.transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if(runThroughStarted == false && distance < 80f)
        { 
            StartCoroutine(RunThroughPlayer());
        }
    }

    private void LockPlayerPosition() {
        
        playerPosition = player.transform.position;
        Vector3 targetPosition = new Vector3( player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
        playerLocked = true;
        StartCoroutine(PlayerWait());
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arena")
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 targetPosition = new Vector3( player.transform.position.x, this.transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
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
        yield return new WaitForSeconds(.25f);
        finishingRun = true;
        yield return new WaitForSeconds(3f);
        Vector3 targetPosition = new Vector3( player.transform.position.x, this.transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
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
