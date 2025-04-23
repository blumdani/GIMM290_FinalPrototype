using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAttack : MonoBehaviour
{
    public Animator attackAnimator;
    public GameObject bull;
    public GameObject sword;
    private GameObject player;
    public BullHealthController bhc;

    private float slowDownFactor = 0.0f;
    private float slowDownDuration = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<CharacterController>().enabled = false;
        attackAnimator = sword.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !attackAnimator.GetBool("attacking"))
        {
            attackAnimator.SetBool("attacking", true);
            StartCoroutine(ResetAttack());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bull") && attackAnimator.GetBool("attacking"))
        {
            Debug.Log("Bull has been hit!");
            //SlowDown();
            if(bull.GetComponent<BullStateManager>().currentState == bull.GetComponent<BullStateManager>().injuredState)
            {
                StartCoroutine(EndGame());
            }
            else
            {
                bhc.BullDamage();
            }
        }
    }

    void SlowDown()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(.45f);
        attackAnimator.SetBool("attacking", false);
    }

    IEnumerator EndGame()
    {
        Debug.Log("Game should be ending");
        Destroy(bull);
        SceneManager.LoadScene("YouWon");
        yield return null; // Ensure the method yields a value
    }
}
