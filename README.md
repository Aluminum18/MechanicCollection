# Mechanic collection
A place to implement mechanics of other games

# Content
- [Xray](#xray)
- [Teleport with dissolve](teleport-with-dissolve)
- [Player view movement joystick](#player-view-movenent-joystick)
- [Recycle Audio Source](#recycle-audio-source)
- [Draggable ragdoll](#draggable-rag-doll)
- [Vary animation with blend tree](#vary-animation-with-blend-tree)
## Xray
Common mechanic used in stealth games. Props in scene partially changes their material to create Xray effect

**Techniques:**
- Mark area (pixels) that needed to be changed material with specific value (eg. 99) in Stencil buffer
- Use Shader graph to create xray effect for character (green effect). Create other simple material for environmnent objects (grey one)
- Use URP Renderer feature to switch material.
- - For character: Parts that marked by value 99 in Stencil 
## Teleport with dissolve
## Player view movement joystick
## Recycle Audio Source
## Draggable ragdoll
## Vary animation with blend tree
