using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class MainLogic : MonoBehaviour
{
    public TextMeshProUGUI tmp_time;
    int counter = 0;
    bool status = true;
    float timeStart;
    double time1, time2, time_a; //double für Math library
    double time3, time4, time_b;
    public TextMeshProUGUI InputField_p;
    public GameObject target1, target2, target3, target4;
    //public GameObject[] targetgroup
    public TextMeshProUGUI tmp_targetkind1;
    public TextMeshProUGUI tmp_target1rayhit;
    public TextMeshProUGUI tmp_targetkind2;
    public TextMeshProUGUI tmp_target2rayhit;
    public TextMeshProUGUI tmp_counter;
    public TextMeshProUGUI tmp_startMT;
    public TextMeshProUGUI tmp_stopMT;
    public TextMeshProUGUI tmp_MT;
    public TextMeshProUGUI tmp_targetkind3;
    public TextMeshProUGUI tmp_target3rayhit;
    public TextMeshProUGUI tmp_targetkind4;
    public TextMeshProUGUI tmp_target4rayhit;
    public TextMeshProUGUI tmp_startMT2;
    public TextMeshProUGUI tmp_stopMT2;
    public TextMeshProUGUI tmp_MT2;

    public GameObject targetTafel;
    public Vector3 rayHit_target1;

    public GameObject finalPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;

        if (status == true)
        {
            tmp_time.text = "task completion time: " + timeStart.ToString("f2") + " sec";

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))

                    //einfügen: nur inkrementieren, wenn ein GO mit Rigidbody aus targets getroffen wurde (damit man nicht inkrementiert, wenn daneben geklickt wurde)
                    //es soll nur weitergehen, wenn das target auch getroffen wurde, nicht wenn einfach nur geklickt wurde
                    //if (hit.rigidbody != null)
                    //{

                    //}

                {
                    counter++;
                    
                    tmp_counter.text = "hits: " + counter;
                    Debug.Log(target1.transform);
                    Debug.Log(target2.transform);
                    Debug.Log(target3.transform);
                    Debug.Log(target4.transform);
                }

                //round 1
                if (counter == 1)
                {
                    Debug.Log(hit.point);
                    tmp_target1rayhit.text = hit.point.ToString();
                    tmp_targetkind1.text = hit.collider.name;
                    time1 = Math.Round(timeStart, 3) * 1000; //3 Nachkommastellen, Umrechnung in ms
                    tmp_startMT.text = "start MT: " + timeStart.ToString("f2") + " sec";
                    target1.SetActive(false);
                    target2.SetActive(true);
                }

                if (counter == 2)
                {
                    Debug.Log(hit.point);
                    tmp_target2rayhit.text = hit.point.ToString();
                    tmp_targetkind2.text = hit.collider.name;
                    time2 = Math.Round(timeStart, 3) * 1000;
                    tmp_stopMT.text = "stop MT: " + timeStart.ToString("f2") + " sec";
                    time_a = time2 -= time1;
                    tmp_MT.text = time_a.ToString() + " ms";
                    target2.SetActive(false);
                    target3.SetActive(true);
                }

                //round 2
                if (counter == 3)
                {
                    Debug.Log(hit.point);
                    tmp_target3rayhit.text = hit.point.ToString();
                    tmp_targetkind3.text = hit.collider.name;
                    time3 = Math.Round(timeStart, 3) * 1000;
                    tmp_startMT2.text = "start MT2: " + timeStart.ToString("f2") + " sec";
                    target3.SetActive(false);
                    target4.SetActive(true);
                }

                if (counter == 4)
                {
                    Debug.Log(hit.point);
                    tmp_target4rayhit.text = hit.point.ToString();
                    tmp_targetkind4.text = hit.collider.name; 
                    time4 = Math.Round(timeStart, 3) * 1000;
                    tmp_stopMT2.text = "stop MT2: " + timeStart.ToString("f2") + " sec";
                    time_b = time4 -= time3;
                    tmp_MT2.text = time_b.ToString() + " ms";
                    target4.SetActive(false);
                    finalPanel.SetActive(true);
                    CreateTextFile();
                    status = false;
                }
            }
        }
    }


    public void CreateTextFile()
    {
        string path = Application.dataPath + "/Log_Task1.txt";
        Debug.Log("debug1");
        File.AppendAllText(path,
            "time: " + System.DateTime.Now.Ticks + " , "
            + "Person_ID: " + InputField_p.text + " , "
            + "\n"
            + "Target_kind: " + tmp_targetkind1.text + " , " + "Target_raycastHit: " + tmp_target1rayhit.text
            + "\n"
            + "Target_kind: " + tmp_targetkind2.text + " , " + "Target_raycastHit: " + tmp_target2rayhit.text
            + "\n"
            + "mt1/2 in ms: " + tmp_MT.text + "\n"
            + "Target_kind: " + tmp_targetkind3.text + " , " + "Target_raycastHit: " + tmp_target3rayhit.text
            + "\n"
            + "Target_kind: " + tmp_targetkind4.text + " , " + "Target_raycastHit: " + tmp_target4rayhit.text
            + "mt3/4 in ms: " + tmp_MT2.text + "\n");
        Debug.Log("debug2");
    }
}
