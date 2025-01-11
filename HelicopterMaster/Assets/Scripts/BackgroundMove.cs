/*
\n










*/

using UnityEngine;

public class BackgroundMove : MonoBehaviour
{

    private SpriteRenderer spriteRender;

    private float
       startPos = 0.0f,
       endPos = 0.0f;

    public float
        normalSpeed = 1.0f,
        slowSpeed = 1.0f;

    // Behaviour messages
    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Behaviour messages
    void Start()
    {
        SetStartPosAndEndPos();
    }

    private void SetStartPosAndEndPos()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);

        float offset = spriteRender.bounds.size.x / 2;

        endPos = worldPoint.x - offset;
        startPos = spriteRender.bounds.size.x * (transform.parent.GetComponentsInChildren<BackgroundMove>().Length);
    }

    // Behaviour messages
    void Update()
    {
        if (!GameController.Instance.GameOver)
        {
            if (GameController.Instance.StartFire)
            {
                transform.position -= new Vector3(Time.deltaTime * normalSpeed, 0.0f, 0.0f);
            }
            else
            {
                transform.position -= new Vector3(Time.deltaTime * slowSpeed, 0.0f, 0.0f);
            }

            if (transform.position.x <= endPos)
            {
                transform.position += new Vector3(startPos, 0.0f, 0.0f);
            }
        }
    }
}
