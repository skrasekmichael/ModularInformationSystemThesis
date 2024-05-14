# Developing Modular Information System<br> with Domain-Driven Design

This is master's thesis at [**BUT FIT**](https://www.fit.vut.cz/.en), and it consists of 3 applications:
* [Clean Architecture](Sources/CleanArchitecture/README.md) Backend (https://github.com/skrasekmichael/CleanArchitecture)
* [Modular Monolith](Sources/ModularMonolith/README.md) Backend (https://github.com/skrasekmichael/ModularMonolith)
* [Frontend](Sources/Frontend/README.md)
(https://github.com/skrasekmichael/ModularInformationSystemThesis)

### Abstract
This thesis deals with monolithic architectures and **Domain-Driven Design** (DDD) and its combination in the development of modular information systems. It provides comprehensive overview of Domain-Driven Design principles and various monolithic architectures, including **Clean Architecture** and **Modular Monolith** architecture. It then demonstrates the use of these patterns and architectures on a demonstration application. The thesis offers insights into the development of information systems using popular approaches such as CQRS, Clean Architecture, Domain-Driven Design, Modular Monolith architecture, and more.
(see [thesis](thesis.pdf))

### Demonstration Application
Team and event management application (*"TeamUp"*), primarily focused on managing small sports teams.

In the application, the user can be part of a team as a regular team member, team coordinator or a team owner. In order to invite new users to the team or remove them, a member must be at least a team coordinator. There can be only one team owner, and a user can become one by creating a team or by being the target of ownership change. The team coordinator (as well as the team owner) creates team events, to which all team members respond whether they can attend or not. Only the team owner can assign roles. 
