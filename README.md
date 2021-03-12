# Windows 98 Launcher ![GitHub last commit](https://img.shields.io/github/last-commit/SpeedStriker243/Windows98Launcher)
An open source GUI front-end for QEMU that is intended for installing and running Windows 98 inside of a virtual machine. Currently unfinished. Bundled with QEMU and written in .NET C#. Yeah, that means it won't work in macOS or Linux without any [compatibility layers](https://www.winehq.org/). On macOS 10.15 Catalina and above [it won't even work at all](https://forum.winehq.org/viewtopic.php?f=9&t=32590). Sorry.

![Windows 98 Launcher](https://github.com/That1M8Head/Windows98Launcher/raw/start/preview.png)

# Important stuff
## OS files
You need to provide your own Windows 98 ISO for use with this project, however a boot disk is already supplied. This ISO file must be named `windows98.iso`. For legal reasons, I can't include one in this project.

### Other OSes
It is possible to boot other operating systems using *their* ISO files, but it is not recommended, as this project was created with Windows 98 in mind. 
If the OS in question supports i386 (Intel x86 32-bit) processors, it'll probably work.

## Hard disk image
If you already have a QCOW2 image for Windows 98 (or any other x86 OS), you can run it using this project. The image must be named `win98.qcow2`, however if you do not have an image, the program will use `qemu-img` to create one.

### Exploring and editing the file
[7-Zip](https://www.7-zip.org/) supports opening, browsing and extracting QCOW2 files, but it cannot edit them.
There is a [guide](https://gist.github.com/shamil/62935d9b456a6f9877b5) for mounting QCOW2 images, but it seems to work for a Linux system with NBD support only -- if on Windows, Cygwin or WSL might work. I don't know, I haven't tried it.
If you just want to copy a few files to the VM, use [this utility](https://www.trustfm.net/software/utilities/Folder2Iso.php) to compress a folder to an ISO disk image and use `Boot Options` in the launcher to start up the machine with that ISO file.

## QEMU
[QEMU](https://www.qemu.org/) is open source software licensed under the [GNU General Public License V2](https://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html) created by [Fabrice Bellard](https://bellard.org/), and is not affiliated with this project or me whatsoever.

# Frequently asked questions (that were not actually asked)
## Why would you make this?
For fun.

## What can it do?
Whatever you can do with a Windows 98 VM.

## Does it have internet?
Unless you somehow had a flipping modem from the 90's, then no, I don't think it'd have internet. (Disclaimer: even if you *did* have a modem I don't think it'd work)

## Why would I use this when I can just use VirtualBox or VMware?
No one said you had to, this was just an experiment. Plus it's more fun to play around with a VM that you could absolutely screw up if you're not careful.

## Can it run Doom?
Abso-fricking-lutely. [I even made a custom QCOW2 image for it.](https://drive.google.com/file/d/1FI5B9kikLCxFtn4Qac-uii5dc6SqVmh8/view?usp=sharing)

## What happened to the original commit history?
I kinda fricked everything up and did a `git push -f origin master` (wow, was that a mistake) but thankfully the old code with the original commit history was forked by [dports](https://github.com/dports/), so I was able to reupload it to my own account! All the old code now resides at [Windows98Launcher-classic](https://github.com/SpeedStriker243/Windows98Launcher-classic).
