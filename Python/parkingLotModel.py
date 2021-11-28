# Model design
import agentpy as ap
import networkx as nx
from agents.car import Car
from agents.parkingspot import ParkingSpot
from agents.trafficLight import TrafficLight
# Visualization
import socket
import seaborn as sns
import time
from random import randint

def int_to_bytes(number: int) -> bytes:
    return number.to_bytes(length=(8 + (number + (number < 0)).bit_length()) // 8, byteorder='big', signed=True)

def initializeCar(id):
    print("Initializing Id", id)
    datos = int_to_bytes(id)
    strid = str(id)
    s.send(b"1=" + str.encode(strid))


class ParkingLotModel(ap.Model):
    def setup(self):
        self.availableSpots = self.p.parkingAgents        
        self.parkedCars = 0
        self.steps = 0
        self.cars = ap.AgentList(self, self.p.n, Car)
        print("Setup: ", self.steps)
        cajones = [[-5.574306, 75.91479], [-5.574306, 78.38479], [-5.574306, 80.85479], [-5.574306, 83.36479], 
                   [-5.574306, 85.83479], [-5.574306, 88.25479], [-5.574306, 90.67479], [-5.574306, 93.14479],
                   [-5.574306, 95.74479], [-2.404306, 95.74479], [-2.404306, 93.27479], [-2.404306, 90.75479],
                   [-2.404306, 88.25479], [-2.404306, 85.85279], [-2.404306, 83.33479], [-2.404306, 80.89479],
                   [-2.404306, 78.50479], [-2.404306, 75.93479], [-2.654306, 15.72479], [-2.654306, 13.17479],
                   [-2.654306, 10.73479], [-2.654306, 8.234787], [-2.654306, 5.774787], [-2.654306, 3.384787],
                   [-2.654306, 0.8547869], [-2.654306, -1.585213], [-2.654306, -4.125213], [-5.864305, -4.125213],
                   [-5.864305, -1.655213], [-5.864305, 0.7447869], [-5.864305, 3.304787], [-5.864305, 5.654787],
                   [-5.864305, 8.254787], [-5.864305, 10.66479], [-5.864305, 13.10479], [-5.864305, 15.65479]]
        
        self.spotsAgents = ap.AgentList(self, self.p.parkingAgents, ParkingSpot)
        count = 0
        for spot in self.spotsAgents:
            spot.id = count + 1
            spot.coordinates['x'] = cajones[count][0]
            spot.coordinates['z'] = cajones[count][1]
            count += 1
            
        self.trafficLightsAgents = ap.AgentList(self, 3, TrafficLight)
        count = 1
        for trafficLight in self.trafficLightsAgents:
            trafficLight.id = count
            count += 1
        
        self.trafficLightsAgents[0].start()
        self.trafficLightsAgents[1].start()
        self.trafficLightsAgents[2].start()
    
    def step(self):
        #initialize car
        for i in range(self.p.vehicleAgents):
            car = self.cars.random()
            initializeCar(i)
            car.initialize(i)
            #print(self.steps)
            #self.cars[self.steps].initialize(self.steps)
            self.steps += 1
            if(self.steps == 100):
                self.stop()

    def update(self):
        self.record('parkedCars', self.getParkedCars())
        self.record('steps', self.steps)
    
    def increaseParkedCar(self):
        self.parkedCars += 1
    
    def decreaseParkedCar(self):
        self.parkedCars -= 1
        
    def getParkedCars(self):
        return len(self.cars.select(self.cars.isParked == True))

s=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(("127.0.0.1", 1101))
from_server = s.recv(4096)
print ("Received from server: ",from_server.decode("ascii"))

'''
data=s.send(b'Hola XYZ Message$')
s.close()
'''
#----------------------------Main--------------------------------------

parameter_ranges = {
    'vehicleAgents' : 100,
    'parkingAgents': 36,
    'steps': 100,  # Number of simulation steps
    'speed': 0.05,  # Speed of connections per step
    'n': ap.Values(100,1000)  # Number of agents
}

model = ParkingLotModel(parameter_ranges)
results = model.run()

# Create sample for different values of n
#sample = ap.Sample(parameter_ranges)

# Keep dynamic variables
#exp = ap.Experiment(ParkingLotModel, sample, iterations=25, record=True) 

# Perform 75 separate simulations (3 parameter combinations * 25 repetitions)
#results = exp.run()
results.save()
sns.set_theme()
results = ap.DataDict.load('ParkingLotModel')
sns.lineplot(
    data=results.arrange_variables(),
    x='steps',
    y='parkedCars'
)

