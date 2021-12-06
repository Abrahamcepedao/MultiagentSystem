using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlTrafficLight : MonoBehaviour
{
    public int id = 0;
    //public Transform target;
    public int status = 0;

    bool flag = true;

    // 0 -> Verde
    // 1 -> Amarillo
    // 2 -> Rojo
    // 3 -> Verde
    // 4 -> Amarillo
    // 5 -> Rojoa

    public GameObject verde1;
    public GameObject verde2;
    public GameObject amarillo1;
    public GameObject amarillo2;
    public GameObject rojo1;
    public GameObject rojo2;

    void Start()
    {

        verde1 = gameObject.transform.GetChild(0).gameObject;
        verde2 = gameObject.transform.GetChild(3).gameObject;
        amarillo1 = gameObject.transform.GetChild(1).gameObject;
        amarillo2 = gameObject.transform.GetChild(4).gameObject;
        rojo1 = gameObject.transform.GetChild(2).gameObject;
        rojo2 = gameObject.transform.GetChild(5).gameObject;
        //StartCoroutine(change());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void begin(){
        // Debug.Log("Begining");
        StartCoroutine(change());
    }

    public IEnumerator change(){
        //Debug.Log("Change");
        if(flag){
        //    Debug.Log("Semaforo id: " + id);
            flag = false;
            if(id == 1){
                verde1.GetComponent<Light>().intensity = 6;
                amarillo1.GetComponent<Light>().intensity = 0;
                rojo1.GetComponent<Light>().intensity = 0;
                verde2.GetComponent<Light>().intensity = 6;
                amarillo2.GetComponent<Light>().intensity = 0;
                rojo2.GetComponent<Light>().intensity = 0;
                yield return new WaitForSeconds(3f);
            } else {
                status = 2; //change to red
                verde1.GetComponent<Light>().intensity = 0;
                amarillo1.GetComponent<Light>().intensity = 0;
                rojo1.GetComponent<Light>().intensity = 6;
                verde2.GetComponent<Light>().intensity = 0;
                amarillo2.GetComponent<Light>().intensity = 0;
                rojo2.GetComponent<Light>().intensity = 6;
                if(id == 2){
                    yield return new WaitForSeconds(4f);
                } else {
                    yield return new WaitForSeconds(8f);
                }

            }
        }
        if(status == 0){ // Verde

            status = 1; //change to red
            verde1.GetComponent<Light>().intensity = 0;
            amarillo1.GetComponent<Light>().intensity = 6;
            rojo1.GetComponent<Light>().intensity = 0;
            verde2.GetComponent<Light>().intensity = 0;
            amarillo2.GetComponent<Light>().intensity = 6;
            rojo2.GetComponent<Light>().intensity = 0;
            yield return new WaitForSeconds(1f);

        } else if(status == 1){ // Amarillo
            status = 2;
            verde1.GetComponent<Light>().intensity = 0;
            amarillo1.GetComponent<Light>().intensity = 0;
            rojo1.GetComponent<Light>().intensity = 6;
            verde2.GetComponent<Light>().intensity = 0;
            amarillo2.GetComponent<Light>().intensity = 0;
            rojo2.GetComponent<Light>().intensity = 6;
            yield return new WaitForSeconds(8f);
        }
        else{ // Rojo
            status = 0;
            verde1.GetComponent<Light>().intensity = 6;
            amarillo1.GetComponent<Light>().intensity = 0;
            rojo1.GetComponent<Light>().intensity = 0;
            verde2.GetComponent<Light>().intensity = 6;
            amarillo2.GetComponent<Light>().intensity = 0;
            rojo2.GetComponent<Light>().intensity = 0;
            yield return new WaitForSeconds(3f);

        }

        StartCoroutine(change());
        
    }


    public int getStatus(){
        return status;
    }
}