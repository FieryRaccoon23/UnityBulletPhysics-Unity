# UnityBulletPhysics-(Unity C#)

Unity Bullet Physic allows user to access Bullet Physic Engine in Unity 2020. It is a WIP.
The Bullet Physic code can be fetched from here: https://github.com/bulletphysics/bullet3
I have generated the .lib files of projects that I need from Bullet Physic code and included it in the dll.

# Setup

To test bullet physics, in a Scene, add BulletPhysicsManager.cs to any GameObject (just one instance, hence it is a singleton). 
Add BulletShapes to objects that are supposed to simulate physic using Bullet Physic. And BulletPhysicsRigidBody to the same objects, so they simulate.

NOTE: Make sure that BulletPhysicsManager.cs runs before any other bullet physic code. To do that, in Unity, go to Edit -> Project Settings -> Script Execution Order. 
Under there, add BulletPhysicsManager first and then BulletPhysicsRigidBody.
