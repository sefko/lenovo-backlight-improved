# Lenovo Backlight Improved

Lenovo Backlight Improved is a Windows application that gives you better control over your keyboard backlight. 

Its main features are:
* Having two more backlight states that offer inactivity backlight after which the backlight is automatically turned off and when an activity is introduced, the backlight is automatically turned on again.
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

Currently the application depends on the 'Keyboard_Core.dll' file that comes with the 'Lenovo Vantage' application.

As of now, the application was only tested on the following machines:
* Lenovo Thinkpad T590

# Future TODOs:

* As different models of Lenovo laptops might use different dll files for controlling the keyboard backlight, the support for more dll files might be added.
* Attach the different supported dll files to the executable, so that Lenovo Vantage installation is not required.

# License

This project is licensed under [MIT License](LICENSE)
