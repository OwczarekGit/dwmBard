# dwmBard
Status bar daemon for dwm.

### Initially i used scripts in my dwm setup, this is meant to replace them.
dwmbard runs in background with many `Handlers` that run in parallel on separate threads, this allows for different information to be gathered in different time intervals. It is also usefull to be able to force bar to refresh, for example when using `sxhkd` for binding keys for volume change, or backlight brightness on laptops, this can be accomplished by sending `SIGUSR1` signal to the process you can do it at any time by running `kill -10 $(pgrep dwmbard)`, this will force execute `doWork()` method for each worker assuming the worker has `manualRefreshPossible` set to `true`.
