using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAttack : MonoBehaviour
{
    public Animator attackAnimator;
    private BullStateManager bull;
    public GameObject sword;
    private GameObject player;
    private BullHealthController bhc;

    private float slowDownFactor = 0.0f;
    private float slowDownDuration = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<CharacterController>().enabled = false;
        attackAnimator = sword.GetComponent<Animator>();
        bhc = GameObject.FindGameObjectWithTag("Bull").GetComponent<BullHealthController>();
        bull = GameObject.FindGameObjectWithTag("Bull").GetComponent<BullStateManager>();
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
        if (collision.gameObject.CompareTag("Bull") && attackAnimator.GetBool("attacking") && bull.GetComponent<BullStateManager>().currentState == bull.GetComponent<BullStateManager>().injuredState)
        {
            Debug.Log("Bull has been hit!");
            bhc.BullDamage();

            if (bhc.GetHealth() == 0)
            {
                StartCoroutine(EndGame());
            }
            else
            {
                bull.SwitchState(bull.resettingState);
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
        yield return new WaitForSeconds(.25f);
        Destroy(bull);
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene("YouWon");
        yield return null; // Ensure the method yields a value
    }
}
