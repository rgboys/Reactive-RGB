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
# arg[1] = Display preview of monitor, or run (0, 1)
# arg[2] = Monitor to capture
# arg[3] = X_COORD
# arg[4] = Y_COORD
# arg[5] = Width of screen to capture
# arg[6] = Height of screen to capture
#
# Example execution for the top left of Monitor 1, with a 200x200 box:
# py .\clientcapture.py 0 0 200 200 1
# ##############################################################


# ##############################################################
# For testing image, use	
# t = Image.open(io.BytesIO(raw_bytes))
# t.show()
# ##############################################################

#start = time.time()
with mss() as sct:
	print(sys.argv)
	if sys.argv[1] == '0':
		MONITOR_NUMBER = int(sys.argv[2])
		mon = sct.monitors[MONITOR_NUMBER]
		monitor = {
			"top": mon["top"],
			"left": mon["left"],
			"width": mon["width"],
			"height": mon["height"],
			"mon": MONITOR_NUMBER
		}

		im = sct.grab(monitor)
		raw = MSSTools.to_png(im.rgb, im.size)
		b = Image.open(io.BytesIO(raw))
		b.show()
	elif sys.argv[1] == '1':
		X_COORD = int(sys.argv[3])
		Y_COORD = int(sys.argv[4])
		WIDTH = int(sys.argv[5])
		HEIGHT = int(sys.argv[6])
		MONITOR_NUMBER = int(sys.argv[2])
		mon = sct.monitors[MONITOR_NUMBER]

		# Capture a bbox using percent values
		monitor = {
			"top": mon["top"] + Y_COORD,
			"left": mon["left"] + X_COORD,
			"width": WIDTH,
			"height": HEIGHT,
			"mon": MONITOR_NUMBER
		}
		
		# Continuously loop until process is killed
		while True:
			im = sct.grab(monitor)

			raw = MSSTools.to_png(im.rgb, im.size)
			nparr = np.frombuffer(raw, np.uint8)
			cimg = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
			avg_per_row = np.average(cimg, axis=0)
			avg = np.average(avg_per_row, axis=0)	
			#print('Runtime: ' + str(time.time()-start))
			print('Average RGB: ' + str(avg))