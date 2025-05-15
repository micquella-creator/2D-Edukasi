using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 10f;

    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public TextMeshProUGUI scoreText;
    private bool jumpStarted = false;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        // Movement
        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        // Jumping (arcade style, tanpa better jump)
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            jumpStarted = true;
        }

        if (isTouchingGround)
        {
            jumpStarted = false;
            fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            Respawn();
            Time.timeScale = 0f;
            FindObjectOfType<QuizManager>().StartQuiz();
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
        else if (collision.CompareTag("Star"))
        {
            if (!collision.gameObject.activeSelf) return;

            Scoring.totalScore += 1;
            scoreText.text = " " + Scoring.totalScore;
            collision.gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = respawnPoint;
        player.velocity = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            Respawn();
            Time.timeScale = 0f;
            FindObjectOfType<QuizManager>().StartQuiz();
        }
    }
}
