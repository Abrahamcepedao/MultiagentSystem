# Model design
import agentpy as ap
import networkx as nx

# Visualization
import seaborn as sns
import time
from random import randint

class Car(ap.Agent): 
  
    def setup(self):
        # Parameters
        self.velocity = 10
        self.distance = 0
        self.timeParked = 0
        self.isInStreetTF = 0
        self.isParked = False
        self.assignedParkingSpot = -1
        self.assignedParkingSpotCoor = -1
        self.coordinates = {'x': -39.58, 'y': 14.71, 'z': 43.06}
        self.looking = 0
        self.direction = randint(0, 4)
        self.id = 0
        self.routeEntrance1 = [[19.49, 43.06], [23.7, 37.4], [23.7, 21.9], [20.4, 16.5]]
        self.routeEntrance2 = [[20.4, -7.9], [34.6, -7.9], [34.6, 7.1]]
        self.routeLeaving1 = [[20.4, -7.9], [34.6, -7.9], [34.6, 7.1], [33.2, 19.6], [29.2, 21.1], [29.2, 34.9], [17.9, 45.7]]
        self.routeLeaving2 = [[33.2, 19.6], [29.2, 21.1], [29.2, 34.9], [17.9, 45.7]]
        self.routeEntrance3 = [[28.6, 51.9], [28.6, 67.2], [34.2, 68.8]]
        self.routeEntrance4 = [[34.2, 96.6], [20.1, 96.6]]
        self.routeLeaving3 = [[34.2, 96.6], [20.1, 96.6], [20.1, 69.1], [23.1, 69.1], [23.1, 52.6], [18.2, 47.9]]
        self.routeLeaving4 = [[20.1, 69.1], [23.1, 69.1], [23.1, 52.6], [18.2, 47.9], [-43.5, 48]]
        
  
    def initialize(self, n):
        
        self.id = n
        self.isInStreetTF = 1 # semafaro 1
        self.looking = 1
        self.timeParked = randint(0,11)
        print("Car initialized: ", self.id)
        #asignar cajon
        parkingSpots = self.model.spotsAgents.select(self.model.spotsAgents.isOccupied == False)
        if (len(parkingSpots) == 0):
            
            #sacar un auto random
            parkedCars = self.model.cars.select(self.model.cars.isParked == True)
            print("len : ", self.model.parkedCars)
            if(self.model.parkedCars != 0):
                car = parkedCars.random()
                
                carId = -1
                for i in car.id:
                    carId = i
                
                while carId == self.id:
                    car = parkedCars.random()
                    carId = -1
                    for i in car.id:
                        carId = i
                
                print("Removing car: ", i, " --- : ", car.assignedParkingSpot) 
                
                #item3 = ap.AttrIter(idd)
                idd = 0
                for it in car.assignedParkingSpot:
                    if(isinstance(it, int)):
                        print(it)
                        idd = it
                    else:
                        for it2 in it:
                            print(it2)
                            idd = it2
                        
                parkingSpots2 = self.model.spotsAgents.select(self.model.spotsAgents.id == idd)
                print("parkingSpots2[0]: ", parkingSpots2[0])
                parkingSpot2 = parkingSpots2[0]
                
                self.assignedParkingSpot = ap.AttrIter([parkingSpot2.id])
                self.assignedParkingSpotCoor = parkingSpot2.coordinates
                print("Assigned parking spot: ", self.assignedParkingSpot, " : ", parkingSpot2.id)
                car.assignedParkingSpot = -1
                car.assignedParkingSpotCoor = []
                car.isParked = False
                self.model.decreaseParkedCar()
                #car.goOut()
        else:
            #parkedCars = self.model.cars.select(self.model.cars.isParked == True)
            print("len : ", self.model.parkedCars)
            parkingSpot = parkingSpots.random()
            parkingSpot.isOccupied = True
            print("parkingSpot id: ", parkingSpot.id)
            self.assignedParkingSpot = parkingSpot.id
            self.assignedParkingSpotCoor = parkingSpot.coordinates
            print("Assigned parking spot: ", self.assignedParkingSpot)
        
        self.move()
        
    def move(self):
        
        #llegar a primer semaforo
        self.looking = 1

        #pasar primer semaforo
        if(self.isInStreetTF == 1 and self.model.trafficLightsAgents.getStatus() != 0):
            self.velocity = 0
        else:
            self.velocity = 10
        
        #ir a lugar de estacionamiento
        self.getTo(self.assignedParkingSpotCoor)
            
        #estacionarse
        self.isParked = True
        self.model.increaseParkedCar()
        print("is true")
        
        time.sleep(0.01)
        #esperar tiempo de estacionamiento
        for i in range(self.timeParked):
            #time.sleep(1)
            pass
        
        #salir del lugar de estacionamiento
        '''self.isParked = False
        self.looking = 0
        parkingSpots = self.model.spotsAgents.select(self.model.spotsAgents.id == self.assignedParkingSpot)
        parkingSpot = parkingSpots.random()
        parkingSpot.isOccupied = False
        self.assignedParkingSpot = -1'''
        
        #ir a segundo semaforo
        if (self.assignedParkingSpot < 19):
            self.isInStreetTF = 2
            #self.getTo(coordenadas del semáforo 2)
        else:
            self.isInStreetTF = 3
            #self.getTo(coordenadas del semáforo 3)


        #salir del juego
        #self.goOut()
        
    def getTo(self, coordinates):
        #calcular ruta para llegar a las coordenadas proporcionadas
        pass

    def goOut(self):
        #calcular ruta para salir
        
        self.isParked = False
        self.looking = 0
        '''parkingSpots = self.model.spotsAgents.select(self.model.spotsAgents.id == self.assignedParkingSpot)
        parkingSpot = parkingSpots.random()
        parkingSpot.isOccupied = False'''
        self.assignedParkingSpot = -1
        pass
    
    