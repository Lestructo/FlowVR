# FlowVR
FlowVR


#To Do
Set up Unity Collaborate
Test EKG with Headset
Measure Noise with and without headset
Make sure everyone can view/edit project


Scene To-Do:
Add Lighting
Fix ball collision
Blend Textures or Redo Scene
Add Bounce Physics
Add Disruptions to scene
Strip Movement Controls
Change In-Game models to Hands
Change grip to hold
Add something to hold ball and tennis racket
Handle Ball/Racket loss
If Possible:
Replace Controller with 3d printed Handle

#Issues
Moving the camera with the controller causes the screen brightness to exponentially increase until the user cannot see anything but white.
  - Final Version will not have controller movement settings.

# Technical Development Tasks

## Unity Project Setup
- [ ] Create a clean Unity project
- [ ] Import XR Interaction Toolkit + OpenXR
- [ ] Verify build + run on Quest 3
- [ ] Configure realistic ball + racket physics *
- [ ] Implement a ball basket or something to have tennis balls in front of user

## Basic Interaction Prototype
- [ ] Add object interaction (grabbing, releasing)
- [ ] Test comfort settings (seated/standing mode)
- [ ] Try to figure out how to make the VR headset not interfere with the cap

## Environment Prototyping
- [ ] Add placeholder assets for testing
- [ ] Add tennis court background
- [ ] Racket vibration

# Flow Measurement & Tracking

## Flow Break Simulation
- [ ] Script interruptions (pop-up distraction, audio cue, sudden pause, low gravity, ball phasing)
- [ ] Ensure interruptions are logged (time + event type)

## Performance Logging
- [ ] Record completion time, accuracy, and idle time after interruption
- [ ] Prepare CSV export for later analysis

# User Testing Preparation

## Pilot Study Design
- [ ] Draft short survey on immersion, stress, and task resumption
- [ ] Define usability metrics (ease of interaction, comfort)
- [ ] Coordinate with teammates for small-scale internal testing

## Usability Session Setup
- [ ] Schedule 2–3 participants for pilot runs
- [ ] Collect notes on VR sickness, immersion, task flow

## Documentation
- [ ] Keep dated notes of Unity setup, issues, solutions, progress
