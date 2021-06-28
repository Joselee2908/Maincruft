using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    
    public GameObject model;
    public Text score;
    public float speed = 128f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log("F");
        if (col.gameObject.tag == "Player") {
            Debug.Log("okay");
            Destroy(model);
            score.text = "Score: " + (score.text[score.text.Length-1] - '0').ToString();
        }
    }
}
