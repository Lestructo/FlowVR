# FlowVR — Inducing and Recovering Flow State in Virtual Reality

FlowVR is a Virtual Reality research project that investigates whether **Flow State** can be reliably induced using immersive VR interaction, and how users recover after a **forced disruption** in focus. The project integrates **Unity VR**, **Meta Quest 3**, and **OpenBCI EEG** to measure both behavioral and neurological indicators of flow.

---

## Team

- Ben Cruickshank  
- Orsan Jawabira  
- Leslie Kelih  
- Joseph Rodriguez  

---

## Project Motivation

Flow State is a highly focused and immersive mental state associated with increased performance, creativity, and learning. This project explores:

- Whether Flow State can be **consistently induced in VR**
- How disruptions affect immersion and attention
- Whether users can **re-enter Flow after disruption**
- How EEG data correlates with user performance and experience

---

## Technology Stack

- Unity 
- XR Interaction Toolkit
- OpenXR
- Meta Quest 3
- OpenBCI EEG (16-node cap)
- C#

---

## Core Interaction

Users repeatedly bounce a **tennis ball on a racket** in a VR tennis court environment. This task was selected because it:

- Has a low learning curve
- Allows prolonged repetition
- Has a high skill ceiling
- Maps naturally to VR controller input

This repetitive motion serves as the primary method for inducing Flow State.

---

## Physics and Haptics

- Physics materials applied to sphere colliders on the tennis balls
- Continuous Dynamic collision detection to prevent clipping
- Realistic bounce dynamics
- Haptic vibration on racket-ball impact
- Sound effects with audio cooldown logic to prevent overlap

---

## Disruption System

- The first successful ball bounce starts a 120-second Flow timer
- After 120 seconds:
  - A 30-second disruption occurs
  - Tennis balls no longer collide with the racket
- Normal physics resumes after disruption
- The user’s ability to re-enter Flow is observed and measured

---

## EEG Integration

- OpenBCI 16-node EEG cap used for brain activity monitoring
- Theta wave activity is analyzed as an indicator of Flow
- EEG data is paired with performance metrics and post-session surveys

---

## Evaluation and Results

- Six participants tested with EEG integration
- Data sources:
  - Theta-wave EEG signals
  - Pre- and post-experience surveys
- EEG data quality was impacted by hardware instability
- Overall user feedback on immersion and experience was positive

---

## Known Challenges

### Unity Development
- Version control conflicts when multiple developers worked on the same scene
- Physics instability and rubberbanding during early development
- HDRP material conversion issues

### Grabbable Objects
- Initial velocity-based grabbing caused visible lag
- Solved by separating physics and visual objects using parent-child hierarchy

### EEG Hardware
- OpenBCI battery swelling
- Multiple required re-soldering repairs

---

## Current Issues

- Moving the camera with the controller causes exponential brightness increase
- Final version removes controller-based movement to avoid this issue

---

## Development To-Do

### Unity Project Setup
- Create a clean Unity project
- Import XR Interaction Toolkit and OpenXR
- Verify Quest 3 build and deployment
- Implement realistic tennis ball and racket physics
- Add a tennis ball basket in front of the user

### Interaction and Environment
- Add grabbing and releasing interactions
- Add tennis court environment
- Implement racket vibration
- Replace controller models with hand models
- Implement grip-based holding system
- Handle ball and racket loss during interaction

### Flow Measurement and Disruption
- Script controlled interruptions
- Log interruption timestamps and event types
- Measure completion time, accuracy, and idle time after disruption
- Prepare CSV exports for analysis

### User Testing
- Design pilot study
- Create short survey on immersion and stress
- Define usability metrics (comfort, interaction, motion sickness)
- Schedule and run pilot tests

## Editor Fly Around
https://youtu.be/EHX3z177bJQ

### Documentation
- Maintain dated logs of setup, issues, and solutions
- Track development progress and research findings

---


## License

This project is for academic and research purposes.
