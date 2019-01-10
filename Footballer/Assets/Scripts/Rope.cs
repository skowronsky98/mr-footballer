using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public List<CircleCollider2D> circleCol = new List<CircleCollider2D>();
    public Rigidbody2D hook;
    public GameObject hook2;

    public GameObject linkPrefab;

    public GameObject ropeWeightPrefab;
    
    public int links = 6;

    public int links2 = 4;
    private int length;

    public bool colliders = true;

    void Start()
    {
        GenerateRope();
        Invoke("Colliders", 1f);
    }

    private void GenerateRope()
    {
        Rigidbody2D prevRB = hook;

        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = prevRB;

            joint.connectedAnchor = new Vector2(joint.connectedAnchor.x, -0.19f);

            prevRB = link.GetComponent<Rigidbody2D>();
            prevRB.mass = 1f; //0.5
            prevRB.gravityScale = 6f;

            if (colliders)
            {
                CircleCollider2D collider = link.AddComponent<CircleCollider2D>();
                collider.radius = 0.06f;// 0.08488076f
                collider.isTrigger = true;
                circleCol.Add(collider);
            }

            if (i == 3)
            {
                Rigidbody2D prevRB2 = prevRB;

                for (int j = 0; j < links2; j++)
                {
                    GameObject link2 = Instantiate(linkPrefab, transform);
                    HingeJoint2D joint2 = link2.GetComponent<HingeJoint2D>();
                    if (j == 0)
                    {
                        SpriteRenderer sprite = link2.GetComponent<SpriteRenderer>();
                        sprite.enabled = false;
                    }

                    joint2.connectedBody = prevRB2;
                    joint2.connectedAnchor = new Vector2(joint2.connectedAnchor.x, -0.19f);

                    prevRB2 = link2.GetComponent<Rigidbody2D>();

                    if (j == links2 - 1)
                    {
                        HingeJoint2D hookJoint = hook2.GetComponent<HingeJoint2D>();
                        hookJoint.connectedBody = prevRB2;

                    }
                }
            }
            //else if (i == links - 1)
            //{
            //    GameObject weight = Instantiate(ropeWeightPrefab, transform);
            //    HingeJoint2D weightJoint = weight.GetComponent<HingeJoint2D>();
            //    weightJoint.connectedBody = prevRB;
            //}


        }

    }
    public void Colliders()
    {
        if (colliders)
        {
            foreach (var item in circleCol)
            {
                item.isTrigger = false;
            }
        }
    }
}
