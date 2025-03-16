## Introduction

ii.InfinityEngine is library facilitating working with files from the Infinity Engine, particularly the Baldurs Gate Enchanced Edition and Baldurs Gate II Enchanced Edition.

## Usage

ii.InfinityEngine is available as a pre-release [nuget package](https://www.nuget.org/packages/ii.InfinityEngine/) and can be added via e.g.

`dotnet add package ii.InfinityEngine --version 0.6.0-alpha.1`

Once added you can instantiate the Game object with the chitin.key and dialog.tlk locations:

`var game = new Game(@"D:\Games\IE\bg2ee-pristine", @"D:\Games\IE\bg2ee-pristine\lang\en_US");`

Then access functionality via the game object e.g.

```
game.LoadResources([IEFileType.Cre]);
var file = game.Creatures.Last();
game.Save<CreFile>(file);
```

## Download

Compiled downloads are not available.

## Compiling

To clone and run this application, you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

```
# Clone this repository
$ git clone https://github.com/btigi/asciiz

# Go into the repository
$ cd src

# Build  the app
$ dotnet build
```

## Licencing

ii.InfinityEngine is licenced under the MIT license. Full licence details are available in license.md