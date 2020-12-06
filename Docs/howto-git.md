# Empty Platformer project - How to set player inputs?

Since the Unity 2019.4+ migration, this project uses the new *Input System*, which allow you to easily define inputs for several devices, and use an actual real input manager tool.

The common usage of this new tool is to create `Input Action Asset`s, and define player controls in it. But in order to avoid bugs for users that are not familiar with that tool, the actions are defined directly on the components that need them, which are [`Platformer Controller`](./platformer-controller.md) (for player movement and jump) and [`Shoot`](./shoot.md) (for... well... shoot action).

In these components, you'll see a *Controls* section, that contains the input settings.

![`Platformer Controller` action settings preview ](./images/input-actions-preview.png)

The original project already has defined bindings for both keyboard, mouse and gamepad.

[=> See the *Input System* manual to learn how to use this editor](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0)

---

[<= Back to summary](./README.md)