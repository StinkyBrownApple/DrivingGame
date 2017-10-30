using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    [SerializeField]
    float turnSpeed = 10f;

    [SerializeField]
    float rotationSpeed = 10f;

    [SerializeField]
    GameObject[] tires;

    bool lost = false;
    // Update is called once per frame
    void Update ()
    {
        Vector3 position = transform.position;
        float movement = (Input.GetAxis("Horizontal") * turnSpeed) * Time.deltaTime;
        position.x += movement;
        position.x = Mathf.Clamp(position.x, -9.5f, 9.5f);
        transform.position = position;

        transform.rotation = Quaternion.Euler(0, movement * rotationSpeed, 0);
        foreach (GameObject tire in tires)
        {
            tire.transform.rotation = Quaternion.Euler(0, movement * rotationSpeed * 5, 90);
        }

        if(Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0; //TODO: What we do when we lose
            lost = true;

        }
    }

    private void OnGUI()
    {
        if (lost)
        {
            GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 100, 200, 50), "You lost");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "Play Again"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("scene");
            }
        }
    }
}
