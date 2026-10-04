### VNyan Advanced Camera control

<img width="320" height="70" alt="image" src="https://github.com/user-attachments/assets/38e0b71c-cd58-4701-94c4-3d7f96fd1372" />

Plugin to allow disabling the main camera, or secondary spout2 cameras from nodes. "Properly" disables rendering to save GPU usage.  
One possible use is to have only a secondary spout2 camera on your stream, meaning VNyan can be safely resized and only used for its UI.  

Alpha plugin - Should work, but at your own risk :D  

### Installation
- Download AdvCam.dll from [Releases](https://github.com/LumKitty/AdvCam/releases)
- Install in the usual way, copy AdvCam.dll to VNyan\Items\Assemblies  
- Ensure 3rd party plugins are enabled: Menu -> Settings -> Misc -> Additional Settings -> Enable 3rd party plugins

### Usage
Enable or disable main camera via the plugin button  
Advanced mode allows you to specify individual layers to enable or disable. You are on your own with this one
For startup options, attach nodes below to an Application Start node  
In future I might add a disable main cam on startup option

### Triggers
```_lum_cam_disablemain``` - Turn off the main camera  
```_lum_cam_enablemain``` - Turn on the main camera  
```_lum_cam_disable``` - Turn off a spout camera (name on text1). Sender will still be active and same size, but blank  
```_lum_cam_enable``` - Turn on a spout camera (name on text1).  
