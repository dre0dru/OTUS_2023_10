```mermaid
classDiagram
    direction LR

    Enemy --> Pool
    Enemy --> Bullets
    Enemy --> Common
    Enemy --> Components

    Character --> Bullets
    Character --> Game
    Character --> GameInput
    Character --> Common
    Character --> Components

    Bullets --> Pool
    Bullets --> Level
    Bullets --> Common
    Bullets --> Components
```