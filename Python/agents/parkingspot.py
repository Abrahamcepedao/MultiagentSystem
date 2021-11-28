# Model design
import agentpy as ap
import networkx as nx

# Visualization
import seaborn as sns
import time
from random import randint

class ParkingSpot(ap.Agent):
    def setup(self):
        self.id = 0
        self.carId = 0
        self.isOccupied = False
        self.coordinates = {'x': 0, 'z': 0}