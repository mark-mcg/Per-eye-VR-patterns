# Introduction

This project can show images (e.g. alpha channel .pngs) with different background colours per-eye for a specified number of frames. Open up the scene in the "Mark" directory and run it for an example. 

![Example pattern as defined in the Unity Editor view.](screenshots/1.png "Example of a pattern where different images with different colour backgrounds are displayed, each for a duration of 1 frame, before the pattern loops.")

# Requirements

Tested on Unity 2017.3.0 using an Oculus Rift CV1.

# Notes

## Is 90FPS / 90Hz always the case?

Ordinarily, a VR headset experience will render at the refresh rate of the headset e.g. 90FPS. However, if the system slows down (e.g. windows update kicks in), the frame rate may take a dip. When this happens, the frame may be displayed for longer than the 1/90 seconds you assumed it would be visible for. The current implementation of this is the naive one (i.e. it just counts frames and doesn't take this into account). If this matters for your project, here's a few things to consider:

1) In Update(), Time.deltaTime tells you how long it took to render the last frame. Instead of counting frames, you could count milliseconds of display time instead, and trigger transitions based on this.

2) Different VR headsets have different behaviour when this happens e.g. Oculus will use asynchronous spacewarp ( https://developer.oculus.com/documentation/pcsdk/latest/concepts/asynchronous-spacewarp/ ) whilst SteamVR will use asynchronous re-projection ( https://www.vrheads.com/what-asynchronous-reprojection-and-how-do-i-use-it ) - basically both the same thing, but if you want to disable them, you have to do so in the requisite SDK for the headset you are using. 


## How can I validate that each pattern is being rendered exactly for the number of frames specified?

I've done a quick validation, rendering at 60FPS on a monitor and using both Unity Recorder ( https://assetstore.unity.com/packages/essentials/beta-projects/recorder-94079 record each rendered frame) and the NVidia shadow play (record at 60FPS) and it looks like everything is working exactly as expected (e.g. if you display a pattern for 1 frame, it will only persist for one frame). But I'd certainly want to validate a bit more on a real VR headset, depending on how precise you have to be...
