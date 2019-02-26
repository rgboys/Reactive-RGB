from mss import mss
from PIL import Image
import sys
import time
import mss.tools as MSSTools
import io
import numpy as np
import cv2

# ##############################################################
# Commandline arguments:
# arg[1] = X_COORD
# arg[2] = Y_COORD
# arg[3] = Width of screen to capture
# arg[4] = Height of screen to capture
# arg[5] = Monitor to capture
#
# Example execution for the top left of Monitor 1, with a 200x200 box:
# py .\clientcapture.py 0 0 200 200 1
# ##############################################################


# ##############################################################
# For testing image, use	
# t = Image.open(io.BytesIO(raw_bytes))
# t.show()
# ##############################################################

start = time.time()
with mss() as sct:
	
	X_COORD = int(sys.argv[1])
	Y_COORD = int(sys.argv[2])
	WIDTH = int(sys.argv[3])
	HEIGHT = int(sys.argv[3])
	MONITOR_NUMBER = int(sys.argv[5])
	mon = sct.monitors[MONITOR_NUMBER]

	# Capture a bbox using percent values
	monitor = {
        "top": mon["top"] + Y_COORD,  # 100px from the top
        "left": mon["left"] + X_COORD,  # 100px from the left
        "width": WIDTH,
        "height": HEIGHT,
        "mon": MONITOR_NUMBER
    }
	
	im = sct.grab(monitor)

	raw = MSSTools.to_png(im.rgb, im.size)
	nparr = np.frombuffer(raw, np.uint8)
	cimg = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
	avg_per_row = np.average(cimg, axis=0)
	avg = np.average(avg_per_row, axis=0)	
	print('Runtime: ' + str(time.time()-start))
	print('Average RGB: ' + str(avg))


	b = Image.open(io.BytesIO(raw))
	b.show()