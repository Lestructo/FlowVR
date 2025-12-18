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

## Running the Experience

- Put on the Quest 3 and navigate to Apps -> Unknown Sources.
- Launch FlowVR.
- You will spawn on the tennis court with balls and the racket to your side.
- Pick up the racket using the grip button and begin bouncing the tennis ball.
- The disruption timer begins after your first successful bounce.

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

## Documentation
- Maintain dated logs of setup, issues, and solutions
- Track development progress and research findings

---

## License

This project is for academic and research purposes.
