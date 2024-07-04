using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower = 30;
    public int itemCount;
    

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
        if (Input.GetButtonDown("Jump") && isJump == false)//����
        {
            isJump = true;
            rd.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

        if (Input.GetKeyDown("r"))   //�����
        {
            SceneManager.LoadScene("Game_" + GameManager.instance.stage.ToString());
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
            UiManager.instance.GetItem(itemCount);
        }

        else if (other.tag == "Point")
        {
            if (itemCount == UiManager.instance.totalItemCount)
            {
                if (GameManager.instance.stage == 3) //������(3)�ܰ���� Ŭ����
                    UiManager.instance.GameOver();
                else
                    SceneManager.LoadScene("Game_" + (GameManager.instance.stage + 1).ToString());   //���� �ܰ�� �Ѿ
            }

            else
            {
                SceneManager.LoadScene("Game_" + GameManager.instance.stage.ToString()); //���� �ܰ� �����
            }
        }
    }
}