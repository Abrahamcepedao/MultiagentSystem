# Model design
import agentpy as ap
import networkx as nx

# Visualization
import seaborn as sns
import time
from random import randint

# Define parameter ranges
class TrafficLight(ap.Agent):
    
    def setup(self):
        self.state = 0 # Green
        self.id = 0
  
    def changeStatus(self):
        if self.state == 0:
            self.state = 1 # Yellow
        elif self.state == 1:
            self.state = 2 # Red
        else:
            self.state = 0
    
    def getStatus(self):
        return self.state

    def start(self):
        print("starting semaforo: ", self.id)
        pass