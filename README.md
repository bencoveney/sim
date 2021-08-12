# Sim

dotnet run
dotnet run --configuration Release

dotnet tool install -g dotnet-format
dotnet format

dotnet build ./EntityComponentSystem
dotnet run -p ./EntityComponentSystem

dotnet build ./Game
dotnet run -p ./Game

## Use cases to support

A person is alive
They have possessions
They can transfer possessions

A village has a population
The population ebbs and flows over time
People are born and people die

A hero forms a community from multiple villages
Multiple communities fight for power
Communities split when they become too large

## Inspiration

Rimworld
Dwarf fortress
Middle earth

# Resources

https://github.com/davudk/OpenGL-TileMap-Demos
https://www.youtube.com/watch?v=052xutD86uI
