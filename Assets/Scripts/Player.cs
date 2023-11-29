using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    private bool pulando = true;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private struct PlayerInputConstants
    {
        public const string Jump = "Jump";

    }

    void Update()

    { 
        //Input do teclado
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        this.transform.Translate(x, 0, 0);

        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);

        if (x == 0.0f)
        {
            x = CrossPlatformInputManager.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            this.transform.Translate(x, 0, 0);
        }

        if (x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        if (x != 0)
        {
            animator.SetBool("correndo", true);
        }
        else
        {
            animator.SetBool("correndo", false);

        }

if (Input.GetKeyDown(KeyCode.Space) && !pulando || isMobileButtonDown && !pulando)
        {
            pulando = true;
            animator.SetBool("pulando", true);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (transform.position.y <= -6.0)
        {
            GameController.diminuiVida();
            if (GameController.vidas <= 0)
            {
                GameController.instance.ShowGameOver();
                this.enabled = false;
                Destroy(gameObject);
            }

            transform.position = new Vector3(-6.8f, 0.02f, 0);

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            GameController.diminuiVida();
            if (GameController.vidas <= 0)
            {
                GameController.instance.ShowGameOver();
                //Debug.Log("Game Over");
                this.enabled = false;
                Destroy(gameObject);
            }

            transform.position = new Vector3(-6.8f, 0.02f, 0);
        }

        if (collision.gameObject.tag == "spike")
        {
            GameController.diminuiVida();
            if (GameController.vidas <= 0)
            {
                GameController.instance.ShowGameOver();
                this.enabled = false;
                Destroy(gameObject);
            }
            transform.position = new Vector3(-6.8f, 0.02f, 0);
        }

        if (collision.gameObject.tag == "saw")
        {
            GameController.diminuiVida();
            if (GameController.vidas <= 0)
            {
                GameController.instance.ShowGameOver();
                this.enabled = false;
                Destroy(gameObject);
            }
            transform.position = new Vector3(-6.8f, 0.02f, 0);
        }
		
		 if (collision.gameObject.tag == "fire")
        {
            GameController.diminuiVida();
            if (GameController.vidas <= 0)
            {
                GameController.instance.ShowGameOver();
                this.enabled = false;
                Destroy(gameObject);
            }
            transform.position = new Vector3(-6.8f, 0.02f, 0);
        }

        if (collision.gameObject.tag == "chao")
        {
            pulando = false;
            animator.SetBool("pulando", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "frutas")
        {
            collision.enabled = false;
            GameController.somaFruta();
            Destroy(collision.gameObject);
        }
    }
}
