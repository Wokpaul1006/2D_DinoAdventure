using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpDinoSC : MonoBehaviour
{
    [SerializeField] AudioSource jumpSFX;
    [SerializeField] JumpManager manager;
    [HideInInspector] Rigidbody2D rb;
    [HideInInspector] SpriteRenderer apparance;
    [SerializeField] Animator dinoAnim;
    private SoundSC sound;
    private Vector3 originPos;
    public bool allowJump;
    public bool allowDoubleJump;
    public bool isGrounded;
    private float jumpSpd;
    private void Start()
    {
        originPos = transform.position;
        jumpSpd = 6f;
        allowJump = false;
        allowDoubleJump = false;

        manager = GameObject.Find("RunningManager").GetComponent<JumpManager>();

        sound = GameObject.Find("RunningManager").GetComponent<SoundSC>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (allowJump == true || Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        else if (allowDoubleJump == true) { DoubleJump(); }
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpSpd, ForceMode2D.Impulse);
        allowJump = false;
        //if (IsAllowSFX())
        //{
        //    jumpSFX.Play();
        //}
        dinoAnim.SetBool("isJump", true);
    }

    private void DoubleJump()
    {
        rb.AddForce(transform.up * jumpSpd/2, ForceMode2D.Impulse);
        allowDoubleJump = false;
        dinoAnim.SetBool("isJump", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            manager.UpdateGameState(2);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            dinoAnim.SetBool("isJump", false);
        }
    }
}
