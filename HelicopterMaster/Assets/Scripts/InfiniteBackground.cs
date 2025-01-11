/*
\n










*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InfiniteBackground : BackgroundMove
{

    private float posX = 0.0f;

    [Space(10)]
    public new MeshRenderer renderer;

    [Space(10)]
    public string sortingLayerName;
    public int sortingOrder;

    // Behaviour messages
    void Start()
    {
        renderer.sortingLayerName = sortingLayerName;
        renderer.sortingOrder = sortingOrder;
    }

    // Behaviour messages
    void Update()
    {
        if (!GameController.Instance.GameOver)
        {
            if (Time.timeScale != 0.0f)
            {
                if (GameController.Instance.StartFire)
                {
                    posX += normalSpeed;
                }
                else
                {
                    posX += slowSpeed;
                }

                if (posX > 1.0f)
                {
                    posX -= 1.0f;
                }

                renderer.material.mainTextureOffset = new Vector2(posX, 0.0f);
            }
        }
    }
}
