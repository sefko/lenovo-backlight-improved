# Lenovo Backlight Improved

Lenovo Backlight Improved is a Windows application that gives you better control over your keyboard backlight. 

Its main features are:
* Having two more backlight states that offer inactivity timeout after which the backlight is automatically turned off and when an activity is introduced, the backlight is automatically turned on again.
* Having the ability to change the backlight state from the system tray application.
* Having the option to configure the inactivity timeout and check interval.
* The backlight will automatically be set to the last set state after sleep.
* If added to the Windows startup programs, the backlight will be put to the last set state, even after the computer has been restarted/cycled.
* Works with the default backlight controls.

# Usage

This application runs in the system tray and it can be configured through the menu available there. 

Compared to the factory three states(first three) of the backlight, this application offers five different states of the backlight:

* Backlight Off
* Backlight level 1 (No timeout)
* Backlight level 2 (No timeout)
* Backlight level 1 (Timeout)
* Backlight level 2 (Timeout)

The states are cycled through using the default backlight controls and are in that order or can be directly selected from the system tray application..

# Compatibility

Currently the application depends on the 'Keyboard_Core.dll' file that comes with the '[Lenovo Vantage](https://apps.microsoft.com/detail/9wzdncrfj4mv)' application, so it needs to be installed in order to use this application.

The application was made in a way that it will automatically find the DLL file that controls the backlight, but in case it is not located in the default directory, it can be loaded manually.

As of now, the application was only tested on the following machines:
* Lenovo Thinkpad T590
* Lenovo Thinkpad T16

If you have information about whether or not the application works on another Lenovo laptop, feel free to create an issue.

# Future TODOs:

* As of now, no future improvements are planned, but any ideas are welcome.

# License

This project is licensed under [MIT License](LICENSE)
