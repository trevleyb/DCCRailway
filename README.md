# DCCRailway
Train Command and Control solution in C#

Early stages of work and happy for others to help contribute. 
Most of the work is in the NCE classes as that is what I use.

This is a system to allow the control of DCC Model Trains. I have tried to build it to be plugable so that I can add and support different systems
and boosters easily. In fact, the system will discover additional .DLLs that meet the DCCRailway.System,*.dll contruct and will use reflection 
to find what systems are available and will build a list of available systems. 

I have also started to write some code to find appropriate adapters by iterating through what the system finds. It was always a pain trying to remember
what port, baud, parity etc I had so tried to build something to find out. 

The idea is that you Instantiate a "SYSTEM", attach and "ADAPTER" and then you can ask the system to create a "COMMAND" based on the command types 
and you can then execute that command against the System and Adapter. 

For NCE PowerPro, it works. I can control a Loco through the USB/Serial interface, I can control accessories (lights) and I can read AUI values. 

But, there is no UI yet and a lot of work still to go. 

DCCRailway.System is the base project which defines the base classes and interfaces for a "SYSTEM"
Each real system inherits from the System classes. 
A System defines a controller or booster. So., NCEPowerCab for example is a System which is based on a base System. 
A System can be connected to a single Adapter such as a Serial, or Network, or USB. Each real system needs to define how it uses an adapter. 
Commands are send to an Adapter to be executed. 
Interfaces are defined for the commands that the systems support. 
Each system registers the commands that it will support. For example, NCEProwerCab supports ISetLoco Speed using NCESetLocoSpeed. 
Not all system support all commands. This is a basic dependency solution I guess. 

Next: Need to support polling of accessory status etc
Need to build a front end to support trains. 

Lots to do. 
