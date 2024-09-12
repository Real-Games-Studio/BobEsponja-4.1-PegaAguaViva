using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string TrackerName;
    [SerializeField] Transform Tracker;
    [SerializeField] bool MouseControlled;
    [SerializeField] GameObject transitionParticle;


    void Start()
    {
        Tracker = GameObject.Find(TrackerName).transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Tracker == null)
        {
            if (MouseControlled)
            {
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Tracker = GameObject.Find(TrackerName).transform.GetChild(2);
            }
        }
        else
        {
            transform.position = Tracker.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Jellyfish")
        {
            Jellyfish jellyfish = collision.GetComponent<Jellyfish>();
            //Destroy(collision.gameObject);
            jellyfish.Collect();
            GameManager.Instance.AddScore(jellyfish.score);
        }
        if (collision.gameObject.tag == "JellyfishStart")
        {
            //Destroy(collision.gameObject);
            collision.GetComponent<JellyfishStart>().Collect();
        }
    }
}
