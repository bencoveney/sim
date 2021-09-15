# Sim

dotnet run
dotnet run --configuration Release

dotnet tool install -g dotnet-format
dotnet format

dotnet build ./EntityComponentSystem
dotnet run -p ./EntityComponentSystem

dotnet build ./Game
dotnet run -p ./Game

## Bugs

- Screen doesn't scale correctly on resize
- Pan doesn't work correctly on resize

## Inspiration

- Rimworld
- Dwarf fortress
- Middle earth
- Banished

# Resources

https://github.com/davudk/OpenGL-TileMap-Demos
https://www.youtube.com/watch?v=052xutD86uI
https://learnopengl.com/Getting-started/Coordinate-Systems
https://www.gamedev.net/blogs/entry/2250186-designing-a-robust-input-handling-system-for-games/