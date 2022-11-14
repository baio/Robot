# Programming Problem Robots

## Dev 

Run 

`dotnet run --project Robot.App`

Tests 

`dotnet test`

## Architecture

### List of projects
+ Robot.Core - Domain abstraction and models. Contains `ICommand` and `ICommandHandler` interface definitions
+ Robot.Engine - Implements abstractions from `Robot.Core` according to task requirements, specifically implements `Command` and related `CommandHandler`  
+ Robot.Driver - Implements main domain logic for task requirements, calculates robot final state given `Grid` , initial state, list of commands and previous scents. Depends only on abstractions from `Robot.Core` and not depends on `Robot.Engine` which covers `Open for extension closed for modification SOLID principle`. In the app domain context it means we can extends set of the commands without changing other application logic.      
+ Robot.Actors - Provide actors abstractions and implementation to coordinate process as per requirements: run command logic for every actor in sequency and wait till pervious robot returns result.
+ Robot.App - Console application which uses robot actors system
+ Robot.Input - Helper library to convert string contents to domain models

### Robot Actors

+ ControllerActor - Controls execution of the agents (aka robots). Executes agents in sequence, store scents from pervious executions 

+ AgentActor - Aka robot actor, given `IRobotDriver`, initial state, list of commands and pervious scents, calculate final state and then report it back to `ControllerActor`

+ PrinterActor - Prints result of every `AgentActor` execution

### Robot Actors Flow

`ControllerActor::Start` -> `AgentActor::Execute` -> `ControllerActor::AgentReport` ->
`AgentActor::Execute (next)` -> `ControllerActor::AgentReport` -> ... the same sequence repeats for each agent 

### Why actors ?

In real word calls to the robots could take nondeterministic amount of time, so we cant wait responses from them. Basically we need infrastructure layer (most probably with queues) to send messages from controller to robots (agents) and back. Actors allow abstract this infrastructure. We can use common interfaces to define actors behavior and when necessary use any existent actor library to bootstrap  infrastructure with our abstracts.

## Assumptions

+ There is no error handling for agent executions. In real word we would have some `ControllerActor::AgentReportError` where agents could send errors when necessary. 
Also we can have timer with expected timeout to receive message from the agent.

+ `ICommandHandler::Handle` in real world should be asynchronous, so we have to change implementation of the `RobotDriver` to support async. Also `RobotDriver` should be able to handle errors from the command handler.

+ The biggest challenge to convert the app to the real word one is to provide infrastructure layer which allows to communicate with robots. It is hard to estimate time for it, but suppose we have reliable REST API which sends commands and receive commands back it won't take too much time, suppose just one week to add this feature. And then we need to bootstrap actors system and test it.   

## Task description

Introduction
Design & implement a solution using C# to solve the problem described below. The objective of the problem is to allow the candidate to demonstrate their design principles and development skills. The problem is provided with sample data to be used for testing and the candidate should be able to demonstrate their solution using the supplied data in the form of a unit test or a simple user interface.

Expected Outcome
On completion of this task, we expect to receive an architecture & design overview describing your application design of the entire solution. Specifically, we are interested in seeing:
List of assumptions that you made
An estimate of how long this would take if you were asked to build the entire solution for a customer
Source code (including any automated tests)


Problem: Martian Robots
The surface of Mars can be modelled by a rectangular grid around which robots are able to move according to instructions provided from Earth. You are to write a program that determines each sequence of robot positions and reports the final position of the robot.
A robot position consists of a grid coordinate (a pair of integers: x-coordinate followed by y-coordinate) and an orientation (N, S, E, W for north, south, east, and west).
A robot instruction is a string of the letters “L”, “R”, and “F” which represent, respectively, the instructions:
Left: the robot turns left 90 degrees and remains on the current grid point.
Right: the robot turns right 90 degrees and remains on the current grid point.
Forward: the robot moves forward one grid point in the direction of the current orientation and maintains the same orientation. The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1). There is also a possibility that additional command types maybe required in the future and provision should be made for this.
Since the grid is rectangular and bounded, a robot that moves “off” an edge of the grid is lost forever. However, lost robots leave a robot “scent” that prohibits future robots from dropping off the world at the same grid point. The scent is left at the last grid position the robot occupied before disappearing over the edge. An instruction to move “off” the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.






Input
The first line of input is the upper-right coordinates of the rectangular world, the lower-left coordinates are assumed to be 0,0.
The remaining input consists of a sequence of robot positions and instructions (two lines per robot).
A position consists of two integers specifying the initial coordinates of the robot and an orientation (N, S, E, W), all separated by white space on one line. A robot instruction is a string of the letters “L”, “R”, and “F” on one line.
Each robot is processed sequentially, i.e., finishes executing the robot instructions before the next robot begins execution.
The maximum value for any coordinate is 50. All instruction strings will be less than 100 characters in length.

Output
For each robot position/instruction in the input, the output should indicate the final grid position and orientation of the robot. If a robot falls off the edge of the grid the word “LOST” should be printed after the position and orientation.

Sample Input
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
03 W
LLFFFLFLFL

Sample Output
11 E
3 3 N LOST
2 3 S