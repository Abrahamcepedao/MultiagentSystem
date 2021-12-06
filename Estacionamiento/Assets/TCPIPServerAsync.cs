using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class TCPIPServerAsync : MonoBehaviour
{
    // Use this for initialization
    public GameObject Car;
    public GameObject Car1;
    public GameObject Car2;
    public GameObject Car3;
    System.Threading.Thread SocketThread;
    volatile bool keepReading = false;
    static public List<GameObject> cars = new List<GameObject>();
    public GameObject[] trafficLights;

    //int count = 0;

    public IEnumerator carSpawner(int idCar, int carType, int parkingSpot, float parkingTime)
    {
        //System.Random rnd = new System.Random();
        //int num = rnd.Next(1, 5);
        //Debug.Log("This is executed from the main thread");
        //GameObject carSpawn = new GameObject();
        //Debug.Log(idCar);
        switch (carType)
        { 
            case 1:
                GameObject carSpawn = Instantiate(Car) as GameObject;
                carSpawn.transform.position = new Vector3(-33.24f, 14.93572f, 42.17f);
                carSpawn.transform.Rotate(0f, 90f, 0f);
                carSpawn.GetComponent<ControlCar>().idCar = idCar;
                carSpawn.GetComponent<ControlCar>().parkingSpot = parkingSpot;
                carSpawn.GetComponent<ControlCar>().parkingTime = parkingTime;
                cars.Add(carSpawn);
                break;
            case 2:
                GameObject carSpawn1 = Instantiate(Car1) as GameObject;
                carSpawn1.transform.position = new Vector3(-33.24f, 14.93572f, 42.17f);
                carSpawn1.transform.Rotate(0f, 90f, 0f);
                carSpawn1.GetComponent<ControlCar>().idCar = idCar;
                carSpawn1.GetComponent<ControlCar>().parkingSpot = parkingSpot;
                carSpawn1.GetComponent<ControlCar>().parkingTime = parkingTime;
                cars.Add(carSpawn1);
                break;
            case 3:
                GameObject carSpawn2 = Instantiate(Car2) as GameObject;
                carSpawn2.transform.position = new Vector3(-33.24f, 14.93572f, 42.17f);
                carSpawn2.transform.Rotate(0f, 90f, 0f);
                carSpawn2.GetComponent<ControlCar>().idCar = idCar;
                carSpawn2.GetComponent<ControlCar>().parkingSpot = parkingSpot;
                carSpawn2.GetComponent<ControlCar>().parkingTime = parkingTime;
                cars.Add(carSpawn2);
                break;
            case 4:
                GameObject carSpawn3 = Instantiate(Car3) as GameObject;
                carSpawn3.transform.position = new Vector3(-33.24f, 14.93572f, 42.17f);
                carSpawn3.transform.Rotate(0f, 90f, 0f);
                carSpawn3.GetComponent<ControlCar>().idCar = idCar;
                carSpawn3.GetComponent<ControlCar>().parkingSpot = parkingSpot;
                carSpawn3.GetComponent<ControlCar>().parkingTime = parkingTime;
                cars.Add(carSpawn3);
                break;
        }

        yield return null;
    }

    /* public IEnumerator initializeLight(int idSemaforo){
        
        trafficLights = GameObject.FindGameObjectsWithTag("Semaforo");
        Debug.Log("A punto de inicializar el semáforo id: " + idSemaforo);
        Debug.Log("Count semáforos: " + trafficLights.Length);


        for(int i = 0; i < 3; i++){

            //GameObject trafficLight = trafficLights[i];
            Debug.Log("Current id: " + trafficLights[i].GetComponent<ControlTrafficLight>().id + " -- " + i);
            if(trafficLights[i].GetComponent<ControlTrafficLight>().id == 0){
                trafficLights[i].GetComponent<ControlTrafficLight>().id = idSemaforo;
                //trafficLight.GetComponent<ControlTrafficLight>().begin();
                Debug.Log("Semáforo inicializado con el id " + idSemaforo);
                // StartCoroutine(trafficLight.GetComponent<ControlTrafficLight>().change());
                break;

            }
            
        }
        
        yield return null;
    
    } */
    public IEnumerator initializeLight(){
        
        trafficLights = GameObject.FindGameObjectsWithTag("Semaforo");
        //Debug.Log("Count semáforos: " + trafficLights.Length);


        for(int i = 0; i < 3; i++){
            //Debug.Log("A punto de inicializar el semáforo id: " + i);
            trafficLights[i].GetComponent<ControlTrafficLight>().id = i+1;
            StartCoroutine(trafficLights[i].GetComponent<ControlTrafficLight>().change());
            //GameObject trafficLight = trafficLights[i];
            /* Debug.Log("Current id: " + trafficLights[i].GetComponent<ControlTrafficLight>().id + " -- " + i);
            if(trafficLights[i].GetComponent<ControlTrafficLight>().id == 0){
                trafficLights[i].GetComponent<ControlTrafficLight>().id = idSemaforo;
                //trafficLight.GetComponent<ControlTrafficLight>().begin();
                Debug.Log("Semáforo inicializado con el id " + idSemaforo);
                // StartCoroutine(trafficLight.GetComponent<ControlTrafficLight>().change());
                break;

            } */
            
        }
        
        yield return null;
    
    }

    void Start()
    {
        Application.runInBackground = true;
        startServer();
    }

    void startServer()
    {
        SocketThread = new System.Threading.Thread(networkCode);
        SocketThread.IsBackground = true;
        SocketThread.Start();
    }



    private string getIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
            }

        }
        return localIP;
    }


    Socket listener;
    public static Socket handler;

    void networkCode()
    {
        string data;

        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // host running the application.
        //Create EndPoint
        IPAddress IPAdr = IPAddress.Parse("127.0.0.1"); // Dirección IP
        IPEndPoint localEndPoint = new IPEndPoint(IPAdr, 1101);

        // Create a TCP/IP socket.
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and 
        // listen for incoming connections.

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.
            while (true)
            {
                keepReading = true;

                // Program is suspended while waiting for an incoming connection.
                Debug.Log("Waiting for Connection");     //It works

                handler = listener.Accept();
                Debug.Log("Client Connected");     //It doesn't work
                data = null;

                byte[] SendBytes = System.Text.Encoding.Default.GetBytes("I will send key");
                handler.Send(SendBytes); // dar al cliente

                // An incoming connection needs to be processed.
                while (keepReading)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    if (bytesRec <= 0)
                    {
                        keepReading = false;
                        handler.Disconnect(true);
                        break;
                    }
                    
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    char index = data[0];

                    //Debug.Log("Received from Server: " + data);

                    if (index == '1') // Inicializa coches
                    {
                        int pos = data.IndexOf('=', 0);
                        int idCar1 = (int)Char.GetNumericValue(data[pos + 1]);
                        int idCar2 = (int)Char.GetNumericValue(data[pos + 2]);
                        int idCar = (idCar1 * 10) + idCar2;
                        //Debug.Log("Llegó el id " + idCar);
                        pos = data.IndexOf('=', 3);
                        int carType = (int)Char.GetNumericValue(data[pos + 1]);
                        //Debug.Log("Car Type " + carType);
                        pos = data.IndexOf('=', pos+1);
                        int parkingSpot1 = (int)Char.GetNumericValue(data[pos+1]);
                        int parkingSpot2 = (int)Char.GetNumericValue(data[pos+2]);
                        int parkingSpot = (parkingSpot1 * 10) + parkingSpot2;

                        pos = data.IndexOf('=', pos+1);
                        float parkingTime = (float)Char.GetNumericValue(data[pos+1]);

                        UnityMainThreadDispatcher.Instance().Enqueue(carSpawner(idCar, carType, parkingSpot, parkingTime));

                    }
                    else
                    if (index == '3'){ // Inicializa semáforos
                    
                        /* int pos = data.IndexOf('=', 0);
                        int idSemaforo = (int)Char.GetNumericValue(data[pos + 1]); */
                        
                        /* for(int i = 1; i <= 3; i++){
                            Debug.Log("Llegó el semáforo con id " + i);
                            
                        } */
                        UnityMainThreadDispatcher.Instance().Enqueue(initializeLight());

                    }

                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(1);
                }

                System.Threading.Thread.Sleep(1);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void stopServer()
    {
        keepReading = false;

        //stop thread
        if (SocketThread != null)
        {
            SocketThread.Abort();
        }

        if (handler != null && handler.Connected)
        {
            handler.Disconnect(false);
            Debug.Log("Disconnected!");
        }
    }

    void OnDisable()
    {
        stopServer();
    }

}