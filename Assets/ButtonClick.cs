using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonClick : MonoBehaviour
{
    public BallPreFab ballPrefab;
    public PointerPrefab pointerPrefab;
    public int chancesLeft = 5;
    static bool touch = false;
    PointerPrefab pointer = null;
    static int stage = 0; // 0 => lock phase, 1 => shoot phase
    // Start is called before the first frame update
    static Vector3 angle = new Vector3(0.0f,0.0f, 0.0f);
    static float startTime = 0 ;
    static float timeStart = startTime%60;
    void Start()
    {
        startTime = Time.time;
        timeStart = startTime % 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.press.isPressed)
        {
            if (!touch)
            {
                if (chancesLeft != 0)
                {
                    touch = true;
                    if (stage == 0)
                    {
                        angle = Quaternion.Euler(-45f, 0, 0) * Camera.main.transform.forward;
                        stage = 1;
                        startTime = Time.time;
                        timeStart = startTime % 60;
                        pointer = Instantiate<PointerPrefab>(pointerPrefab);

                    }
                    else if (stage == 1)
                    {
                        BallPreFab ball = Instantiate<BallPreFab>(ballPrefab);
                        ball.transform.localPosition = transform.position;
                        float currentTime = Time.time;
                        float timeDiff = currentTime % 60 - timeStart + 60; // adding 60 as modulus works on positive numbers
                        ball.GetComponent<Rigidbody>().AddForce(angle * (500 + 300 * (1 - Mathf.Abs(1 - timeDiff % 2))));
                        chancesLeft--;
                        stage = 0;
                        Destroy(pointer.gameObject, 1);
                    }
                }
            }
        }
        else
        {
            touch = false;
        }
        if (pointer != null && stage!=0)
        {
            pointer.transform.localPosition = new Vector3(11.0f, -4.5f + (9.0f * Mathf.PingPong(Time.time- startTime, 1)), 10f);
        }
    }
}
