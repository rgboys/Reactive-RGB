from mss import mss
from PIL import Image
import sys
import time
import mss.tools as MSSTools
import io
import numpy as np
import cv2
import pyaudio
import aubio
import numpy as np
import image_slicer as slicer


# ##############################################################
# Commandline arguments:
# arg[1] = Display preview of monitor (0), or run (1 - also runs audio | anything else will not run audio)
# arg[2] = Monitor to capture
# arg[3] = X_COORD
# arg[4] = Y_COORD
# arg[5] = Width of screen to capture
# arg[6] = Height of screen to capture
# arg[7] = Section # by index
# arg[8] = How many subsections we need to split and calculate dominant RGB for within the section index (for optimization) (sections.Count)
#
# Example execution for the top left of Monitor 1, with a 200x200 box:
# py -3.6 .\clientcapture.py 1 1 0 0 1920 200 0 20 -- Example of capturing a 1920x1080 monitor with 20 subsections (this is testing TOP)
#  py -3.6 .\clientcapture.py 1 1 1820 0 100 1080 3 15 -- Example of capturing a 1920x1080 monitor with 15 subsections (this is testing RIGHT)
# Example of section for testing monitor 1 (with open image)
# py -3.6 .\clientcapture.py 0 1
# ##############################################################


# ##############################################################
# For testing image, use	
# t = Image.open(io.BytesIO(raw_bytes))
# t.show()
# ##############################################################

#start = time.time()

if sys.argv[1] == '1':
	SIMULATED_SQUARES = 10
	TIME_IN_SECS = 10
	# CHANGE AT YOUR OWN RISK 
	CHUNK = 2**11
	RATE = 44100
	# CHANGE AT YOUR OWN RISK 

	p=pyaudio.PyAudio()
	stream=p.open(format=pyaudio.paInt16,channels=1,rate=RATE,input=True,
				frames_per_buffer=CHUNK)

def readSoundOutputAlpha(): 
	data = np.frombuffer(stream.read(CHUNK, exception_on_overflow = False),dtype=np.int16)
	peak=np.average(np.abs(data))*2
	# colorBars = int(50*peak/2**10) % (1+SIMULATED_SQUARES)
	colorBars = int(3*peak/2**6)
	# bars="#"*colorBars
	# print("%05d %s"%(peak,bars))
	if(colorBars > 255):
		colorBars = 255
	return colorBars

with mss() as sct:
	#print(sys.argv)
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
	else:
		SECTION_IND = sys.argv[7]
		SUB_SECTION_COUNT = int(sys.argv[8])

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
		isVert = True if WIDTH == 100 else False

		pixels_per_subsection = 0
		if isVert:
			pixels_per_subsection = int(HEIGHT / SUB_SECTION_COUNT)
		else:
			pixels_per_subsection = int(WIDTH / SUB_SECTION_COUNT)

		# Continuously loop until process is killed
		while True:
			image_output = 'image_in_sec_' + SECTION_IND + '_.png'.format(**monitor)
			im = sct.grab(monitor)
			raw = MSSTools.to_png(im.rgb, im.size)

			#b = Image.open(io.BytesIO(raw))
			#b.show()
			#break

			#Split image into equal sections
			img = Image.open(io.BytesIO(raw))
			sub_sections = []
			curr_x = 0
			curr_y = 0
			for i in np.arange(SUB_SECTION_COUNT):
				if isVert:
					area = (curr_x, curr_y, WIDTH, curr_y + pixels_per_subsection)
					sub_sections.append(img.crop(area))
					curr_y += pixels_per_subsection
				else:
					area = (curr_x, curr_y, curr_x + pixels_per_subsection, HEIGHT)
					sub_sections.append(img.crop(area))
					curr_x += pixels_per_subsection

			rgb = ''
			for i in np.arange(SUB_SECTION_COUNT):
				sub_section_image = sub_sections[i]
				raw = io.BytesIO()
				sub_section_image.save(raw, format='PNG')
				raw = raw.getvalue()
				nparr = np.frombuffer(raw, np.uint8)
				cimg = cv2.imdecode(nparr, cv2.IMREAD_COLOR)
				avg_per_row = np.average(cimg, axis=0)
				avg = np.average(avg_per_row, axis=0)
				rgb +=  str(int(avg[2])) + ' ' + str(int(avg[1])) + ' ' + str(int(avg[0])) + '|'
			#get rid of extranneous pipe char
			rgb = rgb[:-1]
			#print('Runtime: ' + str(time.time()-start))
			#time.sleep(1)

			if sys.argv[1] == '1':
				alph = str(readSoundOutputAlpha())
				#Print such that the read order is R G B|R G B|R G B--SECTION_IND (split by double dash, then bar, then split by space)
				#If section object labels that it's passing in a '1' for sys.argv[1], then it should expect len()
				rgb = alph + ' ' + rgb
				print(rgb + '--' + SECTION_IND)
			else:
				print(rgb + '--' + SECTION_IND)
		
			sys.stdout.flush()
stream.stop_stream()
stream.close()
p.terminate()
