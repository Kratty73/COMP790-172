using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetHandler : MonoBehaviour
{
    public PlaneHandler planeHandler;

    private TextMeshProUGUI scoreui;
    private AudioSource[] audioSrc;
    
    public int score = 0;
    PlaneHandler[] splashPointers = new PlaneHandler[5];
    int currentIdx = 0;
    int lck = 0;
    int hs;
    Camera cam ;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponents<AudioSource>();
        cam = Camera.main;
        scoreui = FindObjectOfType<TextMeshProUGUI>();
        hs = cam.GetComponent<SaveLoad>().load().hs;
        SetScoreText($"Hello!!\n\nTL: 5 CS: {score} HS: {hs}");
    }

    private void SetScoreText (string scoreText)
    {
        scoreui.SetText(scoreText);
    }

    private IEnumerator gameOver()
    {
        if (lck == 0)
        {
            lck = 1;
            yield return new WaitForSeconds(3);
            if (hs >= score && score != 50)
            {
                SetScoreText($"You Loose!!!\n\nTL: 0 CS: {score} HS: {hs}");
                audioSrc[1].Play();
            }
            else
            {
                SetScoreText($"You Win!!!\n\nTL: 0 CS: {score} HS: {score}");
                cam.GetComponent<SaveLoad>().save(score);
                audioSrc[0].Play();
            }
            while (currentIdx != 0)
            {
                Destroy(splashPointers[currentIdx - 1].gameObject);
                splashPointers[currentIdx - 1] = null;
                currentIdx--;
                score = 0;
            }
            yield return new WaitForSeconds(5);
            cam.GetComponent<ButtonClick>().chancesLeft = 5;
            lck = 0;
            hs = cam.GetComponent<SaveLoad>().hs.hs;
            SetScoreText($"Hello!!\n\nTL: 5 CS: {score} HS: {hs}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.GetComponent<ButtonClick>().chancesLeft == 0)
        {
            StartCoroutine(gameOver());
        }
        else
        {
            SetScoreText($"Hello!!\n\nTL: {cam.GetComponent<ButtonClick>().chancesLeft} CS: {score} HS: {hs}");
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        {
            PlaneHandler plane = Instantiate<PlaneHandler>(planeHandler);
            splashPointers[currentIdx] = plane;
            currentIdx += 1;
            plane.transform.localPosition = new Vector3(contact.point.x, contact.point.y, 10);
            score += (int)System.Math.Round(System.Math.Abs(10 - 2* System.Math.Sqrt((contact.point.x * contact.point.x) + (contact.point.y * contact.point.y))),0);
            SetScoreText($"Hello!!\n\nTL: {cam.GetComponent<ButtonClick>().chancesLeft} CS: {score} HS: {hs}");
 
        }

    }
}
