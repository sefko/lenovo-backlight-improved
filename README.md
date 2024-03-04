# Lenovo Backlight Improved

Lenovo Backlight Improved is a Windows application that gives you better control over your keyboard backlight. 

Its main features are:
* Setting inactivity timeout for your keyboard backlight, after which the backlight is automatically turned off and when an activity is introduced, the backlight is automatically turned on again.
* The backlight turns on automatically after sleep to the last set state.
* If setup right, the backlight will be turned on even after the computer has been restarted.
* Works with the default backlight controls.

# Usage

The application runs in the system tray and offers five different states of the backlight, comapred to the factory 3:

* Backlight Off
* Backlight level 1 (No timeout)
* Backlight level 2 (No timeout)
* Backlight level 1 (Timeout)
* Backlight level 2 (Timeout)

The states are cycled through using the default backlight controls and are in that order.

# Compatibility

The application depends on the 'Keyboard_Core.dll' file that comes with the 'Lenovo Vantage' application.

Currently the application was only tested on the following machines:
* Lenovo Thinkpad T590

# Future TODOs:

* As different models of Lenovo laptops might use different dll files for controlling the keyboard backlight, the support for more dll files might be added.
* Attach the different supported dll files to the executable, so that Lenovo Vantage installation is not required.
* Add option to see and change the selected state of the backlight.

# License

This project is licensed under [MIT License](LICENSE)