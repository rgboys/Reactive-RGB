# Reactive-RGB

## What is it?

A reactive backlighting solution for a display. 

## What can it do?

### Light Reactivity

This application can control the RGB lights on the back of your monitor so they display colors that match the display. 

![Example Image](./Images/example_led.png)

### Sound Reactivity

This application can also modulate your LEDs based on the currently playing audio. 

## How does it work?

By monitoring the content displayed on a monitor border, we select colors for a strip of RGB LEDs placed on the back of the display.

By reading pitch and volume data from your microphone intensity and hue of LED strip colors can be modulated. 

## Installation

### Software Requirements

- [Python3](https://www.python.org/downloads/) 
- Python Modules
  - [PyAudio Python Module](https://people.csail.mit.edu/hubert/pyaudio/)
  - [Aubio](https://github.com/aubio/aubio)
  - [mss](https://python-mss.readthedocs.io/installation.html)
  - [numpy](https://www.scipy.org/install.html)
