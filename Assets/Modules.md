```mermaid
classDiagram
    direction LR

    Game --> Components

    Enemy --> Pool
    Enemy --> Bullets
    Enemy --> Common
    Enemy --> Components

    Character --> Bullets
    Character --> GameInput
    Character --> Common
    Character --> Components

    Bullets --> Pool
    Bullets --> Level
    Bullets --> Common
    Bullets --> Components
```