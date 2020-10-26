# BeatSaberCustomAvatars.ApiSample
Sample usage of Custom Avatars' public API. Looking for Custom Avatars? [Head over here](https://github.com/nicoco007/BeatSaberCustomAvatars).

## Running this API sample
This project was built assuming you have both [Custom Avatars](https://github.com/nicoco007/BeatSaberCustomAvatars/) and [SiraUtil](https://github.com/Auros/SiraUtil) installed. You must also tell the project where your Beat Saber folder is located so it can resolve references properly. To do so, create a file called `CustomAvatar.ApiSample.csproj.user` next to `CustomAvatar.ApiSample.csproj` and put the following contents inside:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <!-- Replace this with the path to your Beat Saber installation -->
    <BeatSaberDir>C:\Program Files (x86)\Steam\steamapps\common\Beat Saber</BeatSaberDir>
  </PropertyGroup>
</Project>
```

Once that's done, building the project will automatically copy `CustomAvatar.ApiSample.dll` and `CustomAvatar.ApiSample.pdb` to the `Plugins` folder fo your Beat Saber installation.