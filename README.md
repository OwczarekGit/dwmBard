# dwmBard
Status bar daemon for dwm.

### Dependencies
- dotnet 5.0
- font-awesome 5
- `bash wc sed pamixer pactl playerctl pgrep curl echo dunst networkmanager inotify-tools`

Failing to satisfy all the dependancies does not mean that it won't work, but some handler might not function correctly.

### Initially i used scripts in my dwm setup, this is meant to replace them.
dwmbard runs in background with many `Handlers` that run in parallel on separate threads, this allows for different information to be gathered in different time intervals. It is also usefull to be able to force bar to refresh, for example when using `sxhkd` for binding keys for volume change, or backlight brightness on laptops, this can be accomplished by sending `SIGUSR1` signal to the process you can do it at any time by running `kill -10 $(pgrep dwmbard)`, this will force execute `doWork()` method for each worker assuming the worker has `manualRefreshPossible` set to `true`.

### Autostart
dwmbard can also be used as a replacement for autostart program (ex. instead of starting all programs in .xinitrc).
It creates the `autostart` file in `~/.config/dwmBard/` that you can put autostart entries and whether they should be relaunched in case they get closed. The syntax is: **command; should it be relauched(true/false); (optionally)the name of the program in top**. Note that they are separated by colons, and there is no need to put colon at the end of the entry. So for example to launch google chrome and keep it running if it gets closed you would write entry like that:
- `google-chrome-stable; true; chrome`-Its called **chrome** in top.
- For starting pcmanfm but without relaunching it would look like that: `pcmanfm; false`-third parameter is not necessary couse in top it has the same name (pcmanfm).
