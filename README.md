# Magnetic Robot Simulation in Unity

## Overview

This project focuses on designing a soft robot within the Unity game engine and controlling its movement using an external magnetic field. While the robot's details are covered in academic papers, this readme provides insights into its implementation and design within Unity.

We create a Unity environment for simulating the robot, consisting of several main components:

1. **GI System Model**: We utilize a pre-designed rigid body model for the robot's movement in various sections. This model has been previously developed and used in another project. Here, we import and employ it. It is essential to provide details and references to the repository where you obtained this model and the tasks it performed.

![photo_2023-09-04_19-52-27](https://github.com/TroddenSpade/SoftRobot/assets/33734646/03adb9ef-2a7d-43a6-b721-9bc458bfdb28)


2. **Robot Model**: This is another fundamental component of our Unity environment. We define this model as a prefab in Unity, allowing us to instantiate it in any environment as needed. The robot comprises multiple distinct parts, constructed from two types of objects.

   - The primary components consist of around eleven segments, primarily responsible for robot functionality. These parts are initially placed within an external magnetic field and assigned a specific current direction.

   - The flexible middle section connects these primary components and allows them to move while maintaining a specified angular range. This flexibility enables the robot to take various shapes within a magnetic field based on its intensity, direction, and the robot's weight.

![photo_2023-09-04_19-43-04](https://github.com/TroddenSpade/SoftRobot/assets/33734646/14a2aca9-22df-43e4-8e0b-6211492de210)

Every part of the robot consists of a Rigidbody component and a Configurable Joint component, connected to the adjacent part, determining the range of motion.

The range of motion for each joint is limited to -5 to 5 degrees around the y-axis and -30 to 30 degrees around the x-axis.

![photo_2023-09-04_19-43-15](https://github.com/TroddenSpade/SoftRobot/assets/33734646/fb87a9af-60d6-4444-a987-3804a381d0db)

To define the magnetic orientations for each part of the eleven robot sections, we utilize a script named "Partial Magnetic Field." This script specifies an initial direction for each part and applies forces to align them with an external magnetic field.



### Partial Magnetic Field Script

The "Partial Magnetic Field" script is written in C# and defines the "Partial Magnetic Field" class within it. This class inherits from the MonoBehaviour class related to the Unity engine and contains the Start and FixedUpdate functions, in addition to the necessary variables and their initialization.

#### Within the Partial Magnetic Field class:

- We define a "phase" variable to store the magnetic field's phase difference for the respective section.
- A Scriptable Object reference stores information about the external magnetic field, field size, and other environment data, allowing shared access between the eleven robot section scripts.
- Besides the above variables, we maintain variables for storing the position and rotation of the corresponding section of the eleven primary parts and the Rigidbody associated with it.
- Additionally, we store variables related to the magnetic field's size and its activation status as private variables in this script.

#### Script Execution:

In the subsequent script sections, we need to implement functions inherited from the parent class, MonoBehaviour:

- We define the Start and FixedUpdate functions. The Start function runs only once at the simulation's start, where we store the Rigidbody associated with the part and the capsule's position and rotation data for subsequent calculations.
- In the FixedUpdate function, we perform operations that repeat in each time step, proportional to the number of frames. In each step, we apply a force opposing gravity and proportional to the chosen magnetic field's size to the respective section's Rigidbody.
- To simplify this process, the magnetic field's size is set within a range from 0 to the Earth's gravitational force value. Alternatively, we can use the actual magnetic field size and calculate the opposing gravitational force based on relevant formulas. Then, we apply this force to the Rigidbody using the AddForce function.
- In addition to opposing gravity, we need to apply torque to orient the eleven robot sections in alignment with the magnetic field's direction. To achieve this, we initially calculate the direction of the section using the initial forward vector and the assigned phase. We then find the torque vector by cross-multiplying this direction with the magnetic field vector. Finally, we apply this torque using the AddTorque function.

## Magnetic Robot Control

The "Magnetic Robot" model serves as the controller for the magnetic field in the environment. While it doesn't affect the environment's movement, it manages the magnetic field's properties.

The "Magnetic Robot" consists of two types of scripts: "ControlMagneticField" and "MagneticFieldAgent."

### ControlMagneticField Script

- In the "ControlMagneticField" script, we can manually adjust the magnetic field's direction and strength. This script defines the "ControlMagneticField" class, inheriting from MonoBehaviour. The class's variables include the Transform of the robot, which stores position and rotation data for various robot sections, and a Scriptable Object for magnetic field data.

   We utilize the Scriptable Object to record changes in the magnetic field. After updating its values, other scripts referencing it can read the new data and act accordingly. The Scriptable Object serves as a shared database, updated exclusively by the "Magnetic Robot" model and read by the eleven robot sections.

### MagneticFieldAgent Script

For autonomous movement through our algorithms or running a learning process, we use a script named "MagneticFieldAgent." This script connects to the sphere inside the Magnetic Robot and provides another option for controlling the magnetic field alongside the "ControlMagneticField" script. Unlike the latter, this script utilizes methods for autonomous movement.

This script inherits from the Agent class of the MLAgents package and requires the definition and completion of some functions from the Agent class within this script.

## Demonstration

Watch the following videos to see the Magnetic Robot in action:


## Conclusion

This Unity project simulates a soft robot's movement within an external magnetic field. It offers a detailed overview of the design and implementation of the robot's components, including the use of Rigidbody, Configurable Joint, and scripting to simulate magnetic interactions. The "Magnetic Robot" model controls the magnetic field, allowing for manual adjustments and influencing the robot's behavior within the Unity environment.
