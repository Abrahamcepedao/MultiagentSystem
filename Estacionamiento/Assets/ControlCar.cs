using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlCar : MonoBehaviour
{
    float speed = 8f;
    float high = 8f;
    //float low = 5f;
    //public Transform target;
    int count = 0;
    public int parkingSpot;
    public float parkingTime;
    public int route;
    bool flag = false;
    bool flag2 = false;
    bool flag3 = false;
    bool goingOut = false;
    Vector3 prevParkingSpot = new Vector3(0,0,0);

    public int idCar;

    public int isInStreet = 2; //1 primer semaforo, 2 semaforo derecha, 3 semaforo izquierda
    public int isInStreetInit = 2;

    public int direction = 0; // 0 norte, 1 sur, 2 oeste, 3 este

    GameObject[] trafficLights;

    bool isWaiting = false;
    public int status = 0;

    // Start is called before the first frame update
 
    List<Vector3> rouEn1 = new List<Vector3> {
        new Vector3(19.83f, 6.61f, 41.79f), new Vector3(23.86f, 6.61f, 41.79f),
        new Vector3(25.48f, 6.61f, 38.98f), new Vector3(25.4f, 6.61f, 35.3f),
        new Vector3(25.4f, 6.61f, 26.11f), new Vector3(24.83f, 6.61f, 23.91f),
        new Vector3(21.56f, 6.61f, 20.35f), new Vector3(19.34f, 6.61f, 17.9f),
    };

    List<Vector3> rouSal1 = new List<Vector3> {
        new Vector3(21.8f, 6.61f, -9.91f), new Vector3(27.45f, 6.61f, -9.91f),
        new Vector3(34.56f, 6.61f, -9.91f), new Vector3(35.72f, 6.61f, -6.55f),
        new Vector3(35.72f, 6.61f, 17.42f), new Vector3(33.49f, 6.61f, 20.15f),
        new Vector3(30.43f, 6.61f, 24.8f), new Vector3(30.43f, 6.61f, 35.99f),
        new Vector3(29.43f, 6.61f, 41.58f), new Vector3(25.68f, 6.61f, 45.57f),
        new Vector3(20.05f, 6.61f, 46.59f), new Vector3(17.09f, 6.61f, 46.59f),
        new Vector3(13.84f, 6.61f, 46.59f), new Vector3(-36.11f, 6.61f, 46.59f),
    };

    List<Vector3> rouEn2 = new List<Vector3> {
        new Vector3(19.83f, 6.61f, 41.79f), new Vector3(23.86f, 6.61f, 41.79f),
        new Vector3(25.48f, 6.61f, 38.98f), new Vector3(25.4f, 6.61f, 35.3f),
        new Vector3(25.4f, 6.61f, 26.11f), new Vector3(24.83f, 6.61f, 23.91f),
        new Vector3(21.56f, 6.61f, 20.35f), new Vector3(19.21f, 6.61f, 14.5f),
        new Vector3(19.21f, 6.61f, 13f), new Vector3(19.21f, 6.61f, -6.78f),
        new Vector3(21.8f, 6.61f, -9.91f), new Vector3(27.45f, 6.61f, -9.91f),
        new Vector3(34.56f, 6.61f, -9.91f), new Vector3(35.72f, 6.61f, -6.55f),
    };

    List<Vector3> rouSal2 = new List<Vector3> {
        new Vector3(35.72f, 6.61f, 17.42f), new Vector3(33.49f, 6.61f, 20.15f),
        new Vector3(30.43f, 6.61f, 24.8f), new Vector3(30.43f, 6.61f, 35.99f),
        new Vector3(29.43f, 6.61f, 41.58f), new Vector3(25.68f, 6.61f, 45.57f),
        new Vector3(20.05f, 6.61f, 46.59f), new Vector3(17.09f, 6.61f, 46.59f),
        new Vector3(13.84f, 6.61f, 46.59f), new Vector3(-36.11f, 6.61f, 46.59f),
    };

    List<Vector3> rouEn3 = new List<Vector3> {
        new Vector3(19.83f, 6.61f, 41.79f), new Vector3(23.86f, 6.61f, 41.79f),
        new Vector3(26.23f, 6.61f, 42.51f), new Vector3(27.66f, 6.61f, 43.87f),
        new Vector3(30.32f, 6.61f, 50.93f), new Vector3(31.62f, 6.61f, 65.34f),
        new Vector3(34.5f,  6.61f, 69.35f), new Vector3(36.58f, 6.61f, 76.16f)
    };

    List<Vector3> rouSal3 = new List<Vector3> {
        new Vector3(36.58f, 6.61f, 95.78f), new Vector3(33.7f, 6.61f, 98.51f),
        new Vector3(21.11f, 6.61f, 98.51f), new Vector3(19.34f, 6.61f, 93.16f),
        new Vector3(19.83f, 6.61f, 71.94f), new Vector3(22.74f, 6.61f, 68.69f),
        new Vector3(25.21f, 6.61f, 63.02f), new Vector3(25.21f, 6.61f, 52.24f),
        new Vector3(24.11f, 6.61f, 48.12f), new Vector3(20.05f, 6.61f, 46.59f),
        new Vector3(17.09f, 6.61f, 46.59f), new Vector3(13.84f, 6.61f, 46.59f),
        new Vector3(-36.11f, 6.61f, 46.59f),
    };

     List<Vector3> rouEn4 = new List<Vector3> {
        new Vector3(19.83f, 6.61f, 41.79f), new Vector3(23.86f, 6.61f, 41.79f),
        new Vector3(26.23f, 6.61f, 42.51f), new Vector3(27.66f, 6.61f, 43.87f),
        new Vector3(30.32f, 6.61f, 50.93f), new Vector3(31.62f, 6.61f, 65.34f),
        new Vector3(34.5f,  6.61f, 69.35f), new Vector3(36.58f, 6.61f, 76.16f),
        new Vector3(36.58f, 6.61f, 95.78f), new Vector3(33.7f, 6.61f, 98.51f),
        new Vector3(21.11f, 6.61f, 98.51f), new Vector3(19.34f, 6.61f, 93.16f)
    };

    List<Vector3> rouSal4 = new List<Vector3> {
        new Vector3(19.83f, 6.61f, 71.94f), new Vector3(22.74f, 6.61f, 68.69f),
        new Vector3(25.21f, 6.61f, 63.02f), new Vector3(25.21f, 6.61f, 52.24f),
        new Vector3(24.11f, 6.61f, 48.12f), new Vector3(20.05f, 6.61f, 46.59f),
        new Vector3(17.09f, 6.61f, 46.59f), new Vector3(13.84f, 6.61f, 46.59f),
        new Vector3(-36.11f, 6.61f, 46.59f),
    };

    List<Vector3> parkingSpots = new List<Vector3> {
        /* 0-8 */
        new Vector3(26.6f, 6.61f, 15.05f), new Vector3(26.6f, 6.61f, 12.56f),
        new Vector3(26.6f, 6.61f, 10.07f), new Vector3(26.6f, 6.61f, 7.58f),
        new Vector3(26.6f, 6.61f, 5.09f), new Vector3(26.6f, 6.61f, 2.6f),
        new Vector3(26.6f, 6.61f, 0.11f), new Vector3(26.6f, 6.61f, -2.38f),
        new Vector3(26.6f, 6.61f, -4.87f),
            
        /* 10-18 */
        new Vector3(29f, 6.61f, -6f), new Vector3(29f, 6.61f, -3.5f),
        new Vector3(29f, 6.61f, -1.09f), new Vector3(29f, 6.61f, 1.37f),
        new Vector3(29f, 6.61f, 3.8f), new Vector3(29f, 6.61f, 6.3f),
        new Vector3(29f, 6.61f, 8.8f), new Vector3(29f, 6.61f, 11.4f),
        new Vector3(29f, 6.61f, 13.7f),
        
        /* 19 - 27 */
        new Vector3(29f, 6.61f, 74f), new Vector3(29f, 6.61f, 76.6f),
        new Vector3(29f, 6.61f, 78.8f), new Vector3(29f, 6.61f, 81.5f),
        new Vector3(29f, 6.61f, 83.9f), new Vector3(29f, 6.61f, 86.4f),
        new Vector3(29f, 6.61f, 88.66f), new Vector3(29f, 6.61f, 91.02f),
        new Vector3(29f, 6.61f, 93.48f),

        /* 28 - 36 */
        new Vector3(26.8f, 6.61f, 94.8f), new Vector3(26.8f, 6.61f, 92.38f),
        new Vector3(26.8f, 6.61f, 89.9f), new Vector3(26.8f, 6.61f, 87.46f),
        new Vector3(26.8f, 6.61f, 85.2f), new Vector3(26.8f, 6.61f, 82.54f),
        new Vector3(26.8f, 6.61f, 80.08f), new Vector3(26.8f, 6.61f, 77.62f),
        new Vector3(26.8f, 6.61f, 75.16f)
    };

    void Start()
    {

        byte[] SendBytes = System.Text.Encoding.Default.GetBytes("Coche con el id " + Convert.ToString(idCar) + " inicializado");
        TCPIPServerAsync.handler.Send(SendBytes); // Manda mensaje al cliente

        trafficLights = GameObject.FindGameObjectsWithTag("Semaforo");
        // trafficLight = GameObject.FindWithTag("Semaforo");

        parkingSpot--;
        //Debug.Log("Parking spot: " + parkingSpot);
        if(parkingSpot < 9){
            route = 1;
            transform.LookAt(rouEn1[0]);
        } else if(parkingSpot < 18){
            route = 2;
            transform.LookAt(rouEn2[0]);
        } else if(parkingSpot < 27){
            route = 3;
            transform.LookAt(rouEn3[0]);
        } else {
            route = 4;
            transform.LookAt(rouEn4[0]);
        }
        direction = 0;
        //Debug.Log("Route: " + route);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        
        checkLights();
        
        if(speed != 0 || isWaiting){
            hasCarInFront();
        }
        
        if(!goingOut){
            if(!flag){
                /* if (count == 2){
                    isInStreet = -1;
                } */
                if(!isWaiting){
                    if(route == 1 && count < (rouEn1.Count-1)){

                        if(getDistance(rouEn1[count])){
                            direction = getDirection(rouEn1[count], rouEn1[count+1]);
                            count++;
                            transform.LookAt(rouEn1[count]);

                        }

                    }
                    else if(route == 1) flag = true;
                    
                    if(route == 2 && count < (rouEn2.Count-1)){

                        if(getDistance(rouEn2[count])){
                            direction = getDirection(rouEn2[count], rouEn2[count + 1]);
                            count++;
                            
                            transform.LookAt(rouEn2[count]);

                        }

                    }
                    else if(route == 2) flag = true;

                    if(route == 3 && count < (rouEn3.Count-1)){

                        if(getDistance(rouEn3[count])){
                            direction = getDirection(rouEn3[count], rouEn3[count + 1]);
                            count++;
                            transform.LookAt(rouEn3[count]);
            

                        }

                    }
                    else if(route == 3) flag = true;

                    if(route == 4 && count < (rouEn4.Count-1)){

                        if(getDistance(rouEn4[count])){
                            direction = getDirection(rouEn4[count], rouEn4[count + 1]);
                            count++;
                            transform.LookAt(rouEn4[count]);

                        }

                    }
                    else if(route == 4) flag = true;

                }
                

            }
            else
            if(speed != 0){

                if(flag2){

                    if(getDistance(parkingSpots[parkingSpot])){

                        speed = 0f;
                        status = 1;
                        Rigidbody rb = GetComponent<Rigidbody>();
                        rb.constraints = RigidbodyConstraints.FreezeRotationX;
                        direction = -1;
                        byte[] SendBytes = System.Text.Encoding.Default.GetBytes("Coche con el id " + Convert.ToString(idCar) + " estacionado");
                        TCPIPServerAsync.handler.Send(SendBytes); // dar al cliente

                        StartCoroutine(SmoothBack());
                    }

                }
                else{

                    if(!flag3){

                        speed = high;

                        if(route == 1 || route == 4){
                            prevParkingSpot = parkingSpots[parkingSpot] + new Vector3(-8f, 0f, -1.2f);
                        }
                        
                        if(route == 2 || route == 3){
                            prevParkingSpot = parkingSpots[parkingSpot] + new Vector3(8f, 0f, 1.2f);
                        }

                        flag3 = true;
                        transform.LookAt(prevParkingSpot);

                    }
                    else
                    if(getDistance(prevParkingSpot)){

                        flag2 = true;
                        transform.LookAt(parkingSpots[parkingSpot]);

                    }

                }

            }
        } else {
            if(route == 1 && count < rouSal1.Count-1){
                if(getDistance(rouSal1[count])){
                    isInStreet = 1;
                    isInStreetInit = 1;
                    direction = getDirection(rouSal1[count], rouSal1[count + 1]);
                    count++;
                    transform.LookAt(rouSal1[count]);

                }
            }
            if(route == 2 && count < rouSal2.Count-1){
                if(getDistance(rouSal2[count])){
                    isInStreet = 1;
                    isInStreetInit = 1;
                    direction = getDirection(rouSal2[count], rouSal2[count + 1]);
                    count++;
                    transform.LookAt(rouSal2[count]);

                }
            }
            if(route == 3 && count < rouSal3.Count-1){
                if(getDistance(rouSal3[count])){
                    isInStreet = 3;
                    isInStreetInit = 3;
                    direction = getDirection(rouSal3[count], rouSal3[count + 1]);
                    count++;
                    transform.LookAt(rouSal3[count]);

                }
            }
            if(route == 4 && count < rouSal4.Count-1){
                if(getDistance(rouSal4[count])){
                    isInStreet = 3;
                    isInStreetInit = 3;
                    direction = getDirection(rouSal4[count], rouSal4[count + 1]);
                    count++;
                    transform.LookAt(rouSal4[count]);

                }
            }
        }

    }

    void checkLights(){

        if(isInStreetInit == 2 && getDistance(rouEn1[0])){

            if(speed == 0 && trafficLights[1].GetComponent<ControlTrafficLight>().status == 0){
                speed = high;
                isInStreetInit = -1;
                isWaiting = false;
            }
            else
            if(isInFrontLight() && trafficLights[1].GetComponent<ControlTrafficLight>().status == 1 || trafficLights[1].GetComponent<ControlTrafficLight>().status == 2){
                Debug.Log("Waiting semaforo in Street 2 - carId: " + idCar + " street: " + isInStreet);
                speed = 0f;
                isWaiting = true;
            }

        }
        else
        if(isInStreetInit == 1 && getDistance(rouSal2[3])){

            if(speed == 0 && trafficLights[0].GetComponent<ControlTrafficLight>().status == 0){
                speed = high;
                isInStreetInit = -1;
                isWaiting = false;
            }
            else
            if(isInFrontLight() && trafficLights[0].GetComponent<ControlTrafficLight>().status == 1 || trafficLights[0].GetComponent<ControlTrafficLight>().status == 2){
                Debug.Log("Waiting semaforo in Street 1 - carId: " + idCar + " street: " + isInStreet);
                speed = 0f;
                isWaiting = true;
            }

        }
        else
        if(isInStreetInit == 3 && getDistance(rouSal4[3])){

            if(speed == 0 && trafficLights[2].GetComponent<ControlTrafficLight>().status == 0){
                speed = high;
                isInStreetInit = -1;
                isWaiting = false;
            }
            else
            if(isInFrontLight() && trafficLights[2].GetComponent<ControlTrafficLight>().status == 1 || trafficLights[2].GetComponent<ControlTrafficLight>().status == 2){
                Debug.Log("Waiting semaforo in Street3 - carId: " + idCar + " street: " + isInStreet);
                speed = 0f;
                isWaiting = true;
            }

        }

    }
   
    void hasCarInFront(){
        foreach (var car in TCPIPServerAsync.cars)
        {
            Vector3 pos = new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z);
            if(getDistanceCar(pos)){
                
                Vector3 actual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                bool step = false;
                
                if(route == 1){
                    
                    if(status == 0){ // Entrando al estacionamiento
                        
                        double distentrada1 = getDistance(actual, rouEn1[count]);
                        double distentrada2 = getDistance(pos, rouEn1[count]);

                        step = distentrada2 < distentrada1;
                    }
                    else
                    if(status == 2){ // Saliendo

                        double distsalida1 = getDistance(actual, rouSal1[count]);
                        double distsalida2 = getDistance(pos, rouSal1[count]);
                        
                        step = distsalida2 < distsalida1;
                    }

                }
                else if(route == 2){
                    
                    if(status == 0){
                        
                        double distentrada3 = getDistance(actual, rouEn2[count]);
                        double distentrada4 = getDistance(pos, rouEn2[count]);
                        
                        step = distentrada4 < distentrada3;
                    }
                    else
                    if(status == 2){ // Saliendo
                    
                        double distsalida1 = getDistance(actual, rouSal2[count]);
                        double distsalida2 = getDistance(pos, rouSal2[count]);
                        
                        step = distsalida2 < distsalida1;
                        
                    }

                } else if(route == 3){

                    if(status == 0){

                        double distsalida3 = getDistance(actual, rouEn3[count]);
                        double distsalida4 = getDistance(pos, rouEn3[count]);

                        step = distsalida4 < distsalida3;
                        
                    }
                    else if(status == 2){ // Saliendo
                
                        double distsalida1 = getDistance(actual, rouSal3[count]);
                        double distsalida2 = getDistance(pos, rouSal3[count]);

                        step = distsalida2 < distsalida1;

                    }
                    
                }
                else if(route == 4){

                    if(status == 0){

                        double distentrada1 = getDistance(actual, rouEn4[count]);
                        double distentrada2 = getDistance(pos, rouEn4[count]);

                        step = distentrada2 < distentrada1;

                    }
                    else
                    if(status == 2){
                        
                        double distsalida1 = getDistance(actual, rouSal4[count]);
                        double distsalida2 = getDistance(pos, rouSal4[count]);
                        
                        step = distsalida2 < distsalida1;
                        
                    }
                    
                }

                if(step && car.GetComponent<ControlCar>().status == status){
                    if(car.GetComponent<ControlCar>().isWaiting){
                        speed = 0f;
                        isWaiting = true;
                    }
                    else
                    if(!car.GetComponent<ControlCar>().isWaiting){
                        // if(direction == 1){
                        //     speed = low;
                        //     isWaiting = false;
                        // } else {
                            // if(isInStreet == 2){
                                speed = high;
                                isWaiting = false;
                                //transform.LookAt(rouEn1[0]);
                            //}
                        // }

                    }
                    
                    return;
                }
                
            }
            
            /* if (isInDirection(car) && car.GetComponent<ControlCar>().idCar != idCar){
                if(getDistanceCar(car)){
                    // speed = 2f;
                    // Debug.Log("Direction: " + direction);
                    // Debug.Log("Id Car actual: " + idCar);
                    // Debug.Log("Id otro Car: " + car.GetComponent<ControlCar>().idCar);
                    if(car.GetComponent<ControlCar>().isWaiting && car.GetComponent<ControlCar>().isInStreetInit == isInStreet){
                        speed = 0f;
                        isWaiting = true;
                    }
                    else
                    if(!car.GetComponent<ControlCar>().isWaiting){
                        if(direction == 1){
                            speed = low;
                            isWaiting = false;
                        } else {
                            // if(isInStreet == 2){
                                speed = high;
                                isWaiting = false;
                                //transform.LookAt(rouEn1[0]);
                            //}
                        }

                    }
                    
                    return;
                    //return true;
                }
                else{
                    if(!isWaiting){
                        speed = high;
                        isWaiting = false;
                    }
                    
                }
            } */
        }

        if(!isWaiting){
            speed = high;
            isWaiting = false;
        }
        // if(isWaiting){
        //     // if(isInStreet == 2){
        //         Debug.Log("Se fue el coche de enfrente");
        //         speed = 15f;
        //         isWaiting = false;
        //         transform.LookAt(rouEn1[0]);
        //     //}

        // }
        // if(!isWaiting){

        // }

        // transform.LookAt(rouEn1[0]);
        // }
        
        // transform.LookAt(rouEn1[0]);
        return;
        //return false;
    }

    /* bool getDistanceCar(GameObject car){

        float diffX = car.transform.position.x - gameObject.transform.position.x;
        float diffZ = car.transform.position.z - gameObject.transform.position.z;

        if(direction == 0){
            if(diffZ > -6 && diffZ < 6){
                if(diffX < 7 && diffX > 0){
                    //Debug.Log("DiffX: " + diffX);
                    return true; //slow down
                }
            }
        }
        else
        if(direction == 1){

            if(diffZ > -6 && diffZ < 6){
                if(diffX > -7 && diffX < 0){
                    return true; //slow down
                }
            }

        }
        else
        if(direction == 2){

            if(diffX > -6 && diffX < 6){
                if(diffZ < 7 && diffZ > 0){
                    return true; //slow down
                }
            }

        }
        else
        if(direction == 3){

            if(diffX > -6 && diffX < 6){
                if(diffZ > -7 && diffZ < 0){
                    return true; //slow down
                }
            }

        }

        return false; // is safe

    } */

    bool isInDirection(GameObject car){
        return car.GetComponent<ControlCar>().direction == direction;
    }
    
    double getDistance(Vector3 pos, Vector3 pos2){
        double diffX = pos2.x - pos.x;
        double diffZ = pos2.z - pos.z;
        double distance = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
        return distance;
    }

    bool getDistanceCar(Vector3 pos){
        double diffX = gameObject.transform.position.x - pos.x;
        double diffZ = gameObject.transform.position.z - pos.z;
        double distance = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
        return distance < 8;
    }

    bool getDistance(Vector3 pos){
        double diffX = gameObject.transform.position.x - pos.x;
        double diffZ = gameObject.transform.position.z - pos.z;
        double distance = Math.Sqrt((diffX*diffX)+(diffZ*diffZ));
        return distance < 1.5;
    }

    bool isInFrontLight(){
        if(isInStreet == 2){
            Debug.Log("Estoy en frente del semáforo 2");
            double diffX = gameObject.transform.position.x - rouEn1[0].x;
            return diffX < 0 && diffX > -1.5;
        }
        if(isInStreet == 1){
            Debug.Log("Estoy en frente del semáforo 1");
            double diffZ = gameObject.transform.position.z - rouSal2[3].z;
            return diffZ < 0 && diffZ > -1.5;
        }
        if(isInStreet == 3){
            Debug.Log("Estoy en frente del semáforo 3");
            double diffZ = gameObject.transform.position.z - rouSal4[3].z;
            return diffZ > 0 && diffZ < 1.5;
        }
        return false;
    }

    int getDirection(Vector3 pos1, Vector3 pos2){
        float diffX = pos2.x - pos1.x; 
        float diffZ = pos2.z - pos1.z; // 17.42 - - 6.54

        if(diffX > 0){
            if(diffZ == 0){
                return 0; // norte
            } else if(diffZ < 0) {
                return 3; // este
            } else {
                return 2; //oeste
            }
        }

        if(diffX < 0){
            if(diffZ == 0){
                return 1; // sur
            } else if(diffZ < 0) {
                return 2; // oeste
            } else {
                return 3; //este
            }
        }

        if(diffZ > 0){
            return 2; // oeste
        }

        return 3; // este
    }

    IEnumerator SmoothBack(){
        yield return new WaitForSeconds (parkingTime);
        status = 2;
        if(route == 1 || route == 4){
            for(int i = 0; i < 4; i++){
                yield return new WaitForSeconds (0.1f);
                transform.position = transform.position + new Vector3(-1f, 0f,0f);
            }
            goingOut = true;
            count = 0;
            speed = 15f;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            if(route == 1){
                direction = 3; //este
                transform.LookAt(rouSal1[0]);
            } else {
                direction = 3; //este
                transform.LookAt(rouSal4[0]);
            }
            
        }

        if(route == 2 || route == 3){
            for(int i = 0; i < 4; i++){
                yield return new WaitForSeconds (0.1f);
                transform.position = transform.position + new Vector3(1f, 0f,0f);
            }
            goingOut = true;
            count = 0;
            speed = high;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            if(route == 2){
                direction = 2; //oeste
                transform.LookAt(rouSal2[0]);
            } else {
                direction = 2; //oeste
                transform.LookAt(rouSal3[0]);
            }
        }
 
     }
    
}