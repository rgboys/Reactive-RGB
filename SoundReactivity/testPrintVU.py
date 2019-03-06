import pyaudio
import numpy as np
from graphics import *

SIMULATED_SQUARES = 10
TIME_IN_SECS = 10
# CHANGE AT YOUR OWN RISK 
CHUNK = 2**11
RATE = 44100
# CHANGE AT YOUR OWN RISK 

win = GraphWin("Window", SIMULATED_SQUARES * 100,200)
win.setBackground("Black")

rectangles = []
colors = []
print(rectangles)
for i in range(SIMULATED_SQUARES):
    rect = Rectangle(Point(10 + (i*75),100),Point(150 + (i*75),140))
    rect.setOutline(color_rgb(50,50,100))
    color = color_rgb(250-(i*20),25,250-(i*20))
    rect.setFill("Black")
    rectangles.append(rect)
    colors.append(color)
    

for rect in rectangles:
    rect.draw(win)

p=pyaudio.PyAudio()
stream=p.open(format=pyaudio.paInt16,channels=1,rate=RATE,input=True,
              frames_per_buffer=CHUNK)


for i in range(int(TIME_IN_SECS*44100/1024)): #go for a few seconds
    # data = np.fromstring(stream.read(CHUNK),dtype=np.int16)
    data = np.frombuffer(stream.read(CHUNK, exception_on_overflow = False),dtype=np.int16)
    peak=np.average(np.abs(data))*2
    # colorBars = int(50*peak/2**10) % (1+SIMULATED_SQUARES)
    colorBars = int(50*peak/2**10)223
    bars="#"*colorBars
    # Stuff below is too slow right now 
    print("%04d %05d %s"%(i,peak,bars))
    # for i in range(SIMULATED_SQUARES): 
    #     if(colorBars > i):
    #         # rectangles[i].setFill(colors[i])
    #         print("Colorful")
    #     else:
    #         # rectangles[i].setFill("Black")
    #         print("Black no color")
stream.stop_stream()
stream.close()
p.terminate()
    
