# Datástrofe
### Developed with Unity 5 (c#)

## Controls:
Videogame is designed to be controlled with an original xbox360 gamepad, using left joystick to control mouse through applications like [Enjoy2](https://github.com/fyhuang/enjoy2/) (open source) or [Joystick Mapper](http://joystickmapper.com/). Right stick is for moving map; other controls (action buttons, zoom or rotate map) can be configured in Input config dialog or input mapper (inside Unity).

## Data update intervals
- Air quality data is gathered every 1 hour.
- Accoustic contamination three times a day.
- Traffic data is gathered every 5 mins.
- Environment data (weather related) is gathered every 1 min. 
- Tweets (query: contaminacion OR contaminacion+madrid) updated every 10 minutes (first 8 releveance-ordered tweets are displayed).
- Traffic incidents info is updated with traffic data (same source).

## About builds:
- MacOS (arquitechture independant) may have a bug with shaders, freezing and crashing compilation randomly. 
- PC build seems to run without problems.
- Linux deployment not tested yet.
- Mobile deployments not tested. A hard work of models optimization is required; also shaders need a workaround since it can be high intensive to procees.

## TODO
- Baking lights.
- Implement 3D spatial sound
- Accurate day/night cycle.
- More realistic rain particle system.

For any doubt regarding compilation or code dont heasitate to contact [tonijota](http://twitter.com/tonijota) or [lucía](http://luciaseguramente.com)

Feel free to fork.



## PLAY A BIT!
### _Happy coding!_
