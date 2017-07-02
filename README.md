# Cone4Unity

## What is this?

This is a Unity script that add a Cone shape to the 3D Object list in Unity.

## Why do we need this?

To draw a Cone in Unity. I'm actually quite surprise that Unity don't have that shape, so I decide to spend my time to create this and share to everyone.

## How to use?

1. Download and copy these files and folders to Assets folder in your project.
2. Now you can create a cone by choose *Create* > *3D Object* > *Cone*.
3. Change *Height*, *Top_radius*, *Bottom_radius*, *Sections* as your wish and choose *Rebuild*.

## Explain the parameters!

1. *Height*: The height of the Cone.
2. *Top_radius*, *Bottom_radius*: The radius of the Top and the Bottom circle. If one of the radius is zero, the Cone will be a full circular cone, otherwise it will be a truncated circular cone.
3. *Sections*: How many part the circle should be divided to, the bigger value means the smoother cone. But if you want an piramid, use 4 as the value for this parameter.
4. *Rebuild*: Actually you need to click this button to update your Cone.

## FAQ

### I found a bug!

Well, I'm new to Unity, so please be mercy and make a pull request. Thank you!

### Some feature are missing...

I wrote this base on my own need, so if you need more than this, you could write and make a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
