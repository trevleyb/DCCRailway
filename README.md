# DCCRailway
Train Command and Control solution in C#

Early stages of work and happy for others to help contribute. 
Most of the work is in the NCE classes as that is what I use 

DCCRailway.System is the base project which defines the base classes and interfaces for a "SYSTEM"
Each real system inherits from the System classes. 

A System defines a controller or booster. So., NCEPowerCab for example is a System which is based on a base System. 
A System can be connected to a single Adapter such as a Serial, or Network, or USB. Each real system needs to define how it uses an adapter. 
Commands are send to an Adapter to be executed. 
Interfaces are defined for the commands that the systems support. 
Each system registers the commands that it will support. For example, NCEProwerCab supports ISetLoco Speed using NCESetLocoSpeed. 
Not all system support all commands. This is a basic dependency solution I guess. 

The system works, I can control through my tests loco speed, and accessory lights etc. 

Next: Need to support polling of accessory status etc
Need to build a front end to support trains. 

Lots to do. 
