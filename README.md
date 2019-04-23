# Reactive-RGB

## What is it?

A piece of software that makes color LEDs on the back of your display reactive to what you are doing.

The software is reactive to two things 
- Display
- Audio

## What will it do?

### Display Reactivity

This application can control the RGB lights on the back of your monitor so they display colors that match the display. 

<img src="./Images/example_led.png" alt="drawing" width="400"/>

### Sound Reactivity

This application can also modulate your LEDs based on the currently playing audio. 

## How does it work?

### Display Reactivity 

By monitoring the content displayed on a monitor border, we select colors for a strip of RGB LEDs placed on the back of the display.

<img src="./Images/display_reactivity.png" alt="drawing" width="500"/>


### Sound Reactivity 

By reading pitch and volume data from your microphone intensity and hue of LED strip colors can be modulated. 


<img src="./Images/sound_reactivity.png" alt="drawing" width="500"/>


### Hardware Configuration

<img src="./Images/hardware_conf.png" alt="drawing" width="700"/>

### Important Note Before Trying it Out Yourself

Although the software is capable of extracting the data needed for LEDs, the software for the Arduino has not been completed. 
Therefore, instead this repository bundles a simulation of the LEDs. 

## Installation

### Software Requirements

- [Python3](https://www.python.org/downloads/) 
- Python Modules
  - [PyAudio Python Module](https://people.csail.mit.edu/hubert/pyaudio/)
  - [Aubio](https://github.com/aubio/aubio)
  - [mss](https://python-mss.readthedocs.io/installation.html)
  - [numpy](https://www.scipy.org/install.html)
