using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeBlink : MonoBehaviour
{
    private BoxCollider2D myCollider;
    [SerializeField] private bool isSolid;
    private float timer = 0;
    [SerializeField] private float timeSolid;
    [SerializeField] private float timeNotSolid;
    private SpriteRenderer m_SpriteRenderer;
    Color32 couleurOrigine;
    [SerializeField] Color32 couleurNotSolid;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        couleurOrigine = m_SpriteRenderer.color;

        if (!isSolid)
        {
            myCollider.enabled = false;
            m_SpriteRenderer.color = couleurNotSolid;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isSolid)
        {
            if (timer >= timeSolid)
            {
                isSolid = false;
                timer = 0;
                myCollider.enabled = false;
                m_SpriteRenderer.color = couleurNotSolid;
            }
        }
        else
        {
            if (timer >= timeNotSolid)
            {
                isSolid = true;
                timer = 0;
                myCollider.enabled = true;
                m_SpriteRenderer.color = couleurOrigine;
            }
        }
    }
}
