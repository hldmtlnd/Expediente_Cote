# Expediente_Cote

Indie UFO found-footage survival horror game.

## Estructura de Scripts

Todos los scripts están organizados en `Assets/Scripts/`:

- **Player/** — Movimiento, interacción, teléfono, inventario
- **Cameras/** — Cámaras de seguridad, teléfono, avanzada
- **Aliens/** — Alien invisible y aliens visibles
- **Events/** — Triggers y director de sucesos
- **Interactables/** — Puertas, ventanas, luces, carga
- **Battery/** — Gestión de batería
- **Audio/** — Sonidos y voces
- **UI/** — Interfaces del teléfono y HUD
- **Save/** — Guardado y estado del expediente
- **Lighting/** — Luces, día/noche, orbes
- **Combat/** — Revólver y daño
- **Hiding/** — Escondites
- **Sensors/** — Detección de cámaras y aliens
- **Final/** — Secuencia final
- **Editor/** — Automatizador de escena

## Automatizador

Usa `Assets/Scripts/Editor/SceneSetupAutomator.cs` desde el menú de Unity para configurar automáticamente la escena de prueba.
