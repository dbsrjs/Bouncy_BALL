using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower = 30;
    public int itemCount;
    
    public GameManager gameManager;

    bool isJump;
    Rigidbody rd;
    AudioSource audio;

    private void Awake()
    {
        isJump = false;
        rd = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            isJump = true;
            rd.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rd.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false);
            gameManager.GetItem(itemCount);
        }

        else if (other.tag == "Point")
        {
            if (itemCount == gameManager.totalItemCount)
            {
                if (gameManager.stage == 4)
                {
                    GameManager.main.GameOver();
                    Time.timeScale = 0;
                }

                else
                {
                    SceneManager.LoadScene("Game_" + (gameManager.stage + 1).ToString());
                }
            }

            else
            {
                SceneManager.LoadScene("Game_" + gameManager.stage.ToString());
            }
        }
    }
}
