There are two ways of interaction implemented in this project, gaze and controller.

Gaze functionality should work with any VR devices therotically, as long as it has a headset.

Controller functionality ic currently designed to work with Oculus Rift touch controller. Because different controller may have different input mapping, and in this project we only include the Oculus Rift touch controller input mapping. (However, Oculus also follows OpenVR layout, if the other VR device follows OpenVR layout, this project should work with them as well)

The upgraded VRTK (as the writing of this README, latest version is 4.0a) will not handle the interaction with Unity UI system, therefore, the project introduces CurvedUI (as the writing of this README, latest version is 2.8) to handle those interactions. In a nutshell, Camera movement, teleport functionality, and interaction with non-Unity-UI objects are handled by VRTK (in our case is selecting Instructor), while interaction with Unity-UI is handled by CurvedUI. 