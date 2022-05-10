using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    public float strength = 2f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private float score = 0f;

    private Vector3 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            direction = Vector3.up * strength;
        }
        
        if (Input.GetKeyDown(KeyCode.S)) {
            direction = Vector3.down * strength;
        }

        // Apply gravity and update the position
        direction.y += 0;
        transform.position += direction * Time.deltaTime;
        Debug.Log(strength);
        strength += score * 0.0001f;        
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            score = 0;
            strength = 2f;
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            score += 1;
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

}
