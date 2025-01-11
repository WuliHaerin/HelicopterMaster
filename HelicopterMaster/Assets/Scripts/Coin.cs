/*
\n










*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private Rigidbody2D rigid2D;

    private SpriteRenderer spriteRender;

    private float endPos = 0.0f;

    // Behaviour messages
    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Behaviour messages
    void Start()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);

        float offset = spriteRender.bounds.size.y / 2;
        endPos = worldPoint.y - offset;
    }

    // Behaviour messages
    void OnEnable()
    {

        float velocityUp = Random.Range(5.5f, 10.0f);

        float velocityHorizontal = Random.Range(6.5f, 9.5f);

        int dir = -1;

        rigid2D.velocity = new Vector2(velocityHorizontal * dir, velocityUp);
    }

    // Behaviour messages
    void OnDisable()
    {
        transform.position = Vector3.zero;
    }

    // Behaviour messages
    void Update()
    {
        if (transform.position.y <= endPos)
        {
            gameObject.SetActive(false);
        }
    }
}
